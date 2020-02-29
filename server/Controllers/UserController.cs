using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Interfaces;
using server.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using server.Logic;
using server.DB;
using server.IOobjects;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MainController
    {

        //enbale to send events another from other place then the hub itself.
        private readonly IHubContext<UserHub, IUserHub> hubContext;

        public UserController(IHubContext<UserHub, IUserHub> userHub)
        {
            this.hubContext = userHub;
        }

        [HttpPost]
        [Route("CreateUser", Name = "CreateUser")]
        public async Task<ActionResult> CreateUser(UserPartial iUser)
        {
            try
            {
                SignalRChatContext DB = new SignalRChatContext();

                using (var transaction = DB.Database.BeginTransaction())
                {
                    try
                    {
                        bool isExist = UserFunctions.CheckIfUserExist(iUser, DB);

                        if (isExist == true) {
                            return BadRequest();
                        }

                        UserCls newUser = UserFunctions.CreateNewUser(iUser, DB);
                        newUser.code = SignInCodesFucntions.GenerateCodeForUser(newUser.id, DB);

                        transaction.Commit();

                        newUser.token = JwtHandler.CreateJwt(newUser.id, newUser.userName, DateTime.Now);
                        
                        await hubContext.Clients.All.AddUserEvent(newUser.id);

                        return new JsonResult(newUser);
                    }
                    catch (Exception exc)
                    {
                        //write exc to log...
                        transaction.Rollback();
                        return BadRequest();
                    }
                }
            }
            catch (Exception ex)
            {

                //write ex to log...
                return BadRequest();
            }
        }


        //[HttpPost]
        //[Route("LogIn", Name = "LogIn")]
        //public ActionResult LogIn(string iPhoneNum, string iCode) {

        //    int numeriCode = 0;

        //    if (int.TryParse(iCode, out numeriCode) == false) {
        //        return BadRequest();
        //    }

        //    var userDetails = UserFunctions.UserByPhoneAndCode(iPhoneNum, numeriCode);

        //    if (userDetails == null) {
        //        return BadRequest();
        //    }

        //    var token = JwtHandler.CreateJwt(userDetails.id, userDetails.userName, DateTime.Now);

        //    var isValid = JwtHandler.IsTokenValid(token);

        //    return new JsonResult(userDetails);
        //}

        [HttpGet]
        [Route("GetFullUserList", Name = "GetFullUserList")]
        public ActionResult GetFullUserList()
        {

            int userId = ExractIdFromToken();
            if (userId == 0)
            {
                return BadRequest();
            }

            //var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.FirstOrDefault();
            //int userId = 0;
            //if (JwtHandler.IsTokenValid(token , out userId) == false) {
            //    return BadRequest();
            //}

            SignalRChatContext Db = new SignalRChatContext();

            List<UserCls> userListRes = UserFunctions.GetFullUserList(Db , userId);

            return new JsonResult(userListRes);
        }


        // GET api/values
        [HttpGet("[action]")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpPost]
        [Route("ReceiveCode", Name = "ReceiveCode")]
        public ActionResult ReceiveCode(loginCls iLoginObj) //since only one param [FromBody] needed
        {
            try
            {
                int newCode = SignInCodesFucntions.UpdateCodeForUserByPhone(iLoginObj.phone, null);

                //Code should be sent to mail/phone... if user not exist, Code text box should appear, but phone mail should not be sent. Irrelevant user should not know whether user exist or not.
                return new JsonResult(newCode);
            }
            catch (Exception exc)
            {
                //write exc to log...
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("LogIn", Name = "LogIn")]
        public async Task<ActionResult> LogIn(loginCls iLoginObj) {

            var userValidData = SignInCodesFucntions.ValidateCode(iLoginObj.phone, iLoginObj.code);

            if (userValidData == null) {
                return BadRequest();
            }

            var token = JwtHandler.CreateJwt(userValidData.userId , userValidData.userName, DateTime.Now);

            UserCls res = UserFunctions.UserByPhoneAndCode(iLoginObj.phone, iLoginObj.code);
            res.token = token;
            res.id = res.id; //id should not be returned. selected user should return dummy id or encrypted one.
            res.code = null;

            await this.hubContext.Clients.All.AddUserEvent(res.id);

            return new JsonResult(res);
        }

        [HttpPost]
        [Route("LogOut" , Name = "LogOut")]
        public async Task<ActionResult> LogOut() {

            int userId = ExractIdFromToken();
            if (userId == 0) {
                return BadRequest();
            }

            await this.hubContext.Clients.All.LogOutUserEvent(userId);

            return new JsonResult(userId);

        }


        //private int ExractIdFromToken() {
        //    var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.FirstOrDefault();
        //    int userId = 0;
        //    if (JwtHandler.IsTokenValid(token, out userId) == false)
        //    {
        //        return 0;
        //    }

        //    return userId;
        //}


    }
}
