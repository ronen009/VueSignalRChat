using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using server.DB;
using server.IOobjects;

namespace server.Logic
{
    public static class SignInCodesFucntions {

        public static int GenerateCodeForUser (int iUserId, SignalRChatContext iDB) {

            SignInCodes addObj = new SignInCodes();
            var code = CreateCode();

            addObj.Code = code;
            addObj.UpdateDate = DateTime.Now;
            addObj.UserId = iUserId;

            var ret = iDB.SignInCodes.Add(addObj);
            iDB.SaveChanges();

            return addObj.Code;

        }


        public static int UpdateCodeForUserByPhone (string iPhoneNum, SignalRChatContext iDB)
        {
            try {

                if (iDB == null) {
                    iDB = new SignalRChatContext();
                }

                var objToUpdate = iDB.Users.Where(x => x.Phone == iPhoneNum).Select(x => x.SignInCodes).FirstOrDefault();

                if (objToUpdate == null) {
                    return -1;
                }

                objToUpdate.Code = CreateCode();
                objToUpdate.UpdateDate = DateTime.Now;

                iDB.SaveChanges();

                return objToUpdate.Code;
            }
            catch (Exception ex) {
                throw new Exception();
            }

        }

        public static ValidateCodeOutput ValidateCode(string iPhoneNum, int iCode)
        {

            SignalRChatContext DB = new SignalRChatContext();

            var ret = DB.SignInCodes.Where(x => x.User.Phone == iPhoneNum && x.Code == iCode).Select(x => new ValidateCodeOutput { userId = x.User.UserId , userName = x.User.UserName}).FirstOrDefault();

            return ret;
        }

        private static int CreateCode() {
            Random random = new Random();
            return random.Next(1000, 9999);
        }
    }
}