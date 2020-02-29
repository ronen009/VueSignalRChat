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
    public class MsgController : MainController
    {

        //enbale to send events another from other place then the hub itself.
        private readonly IHubContext<MsgHub, IMsgHub> hubContext;

        public MsgController(IHubContext<MsgHub, IMsgHub> msgHub)
        {
            this.hubContext = msgHub;
        }




        [HttpPost]
        [Route("SendMsg", Name = "SendMsg")]
        public async Task<ActionResult> SendMsg(chatMsgInput iMsgIobj)
        {
            try
            {
                int userId = ExractIdFromToken();
                if (userId == 0)
                {
                    return BadRequest();
                }

                SignalRChatContext DB = new SignalRChatContext();

                using (var transaction = DB.Database.BeginTransaction())
                {
                    try
                    {
                        MsgObject res = MsgFunctions.SendMsg(userId, iMsgIobj.toUserId, iMsgIobj.msg, DB);

                        transaction.Commit();

                        await hubContext.Clients.All.SentMsgEvent(userId, iMsgIobj.toUserId , iMsgIobj.msg);

                        return new JsonResult(res);
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

        [HttpGet]
        [Route("GetMsgHistory", Name = "GetMsgHistory")]
        public ActionResult GetMsgHistory(int id)
        {
            int userId = ExractIdFromToken();
            if (userId == 0)
            {
                return BadRequest();
            }

            try {
                List<MsgObject> msgList = MsgFunctions.GetChatHistory(userId, id, null);
                return new JsonResult(msgList);
            }
            catch (Exception ex) {
                //write ex to log...
                return BadRequest();
            }



        }
    }
}
