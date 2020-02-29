using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using server.IOobjects;
using server.DB;

namespace server.Logic
{
    public static class UserFunctions
    {

        public static UserCls CreateNewUser(UserPartial iUser, SignalRChatContext iDB)
        {

            var userToAdd = new Users
            {
                Age = iUser.age,
                CreationDate = DateTime.Now,
                FirstName = iUser.firstName,
                LastName = iUser.lastName,
                Phone = iUser.phone,
                UserName = iUser.userName,
                Icon = iUser.icon
            };

            iDB.Users.Add(userToAdd);
            iDB.SaveChanges();

            UserCls ret = new UserCls
            {
                id = userToAdd.UserId,
                age = userToAdd.Age,
                firstName = iUser.firstName,
                lastName = iUser.lastName,
                phone = iUser.phone,
                userName = iUser.userName,
                icon = iUser.icon
            };

            return ret;
        }

        public static bool CheckIfUserExist(UserPartial iUser, SignalRChatContext iDB)
        {

            bool res = iDB.Users.Any(x => x.UserName == iUser.userName || x.Phone == iUser.phone);

            return res;

        }

        public static List<UserCls> GetFullUserList(SignalRChatContext iDB, int iUserId)
        {
            var retList = iDB.Users.Where(x => x.UserId != iUserId).OrderBy(x => x.FirstName).Take(1000).Select(x => new UserCls
            {
                id = x.UserId,
                age = x.Age,
                firstName = x.FirstName,
                lastName = x.LastName,
                phone = x.Phone,
                userName = x.UserName,
                icon = x.Icon
            }).ToList();

            return retList;
        }

        public static int IsUserExist(string iPhoneNum , SignalRChatContext iDB) {

            if (iDB == null) {
                iDB = new SignalRChatContext();
            }

            return iDB.Users.Where(x => x.Phone == iPhoneNum).Select(x => x.UserId).FirstOrDefault();

        }

        public static UserCls UserByPhoneAndCode(string iPhoneNum , int iCode) {

            SignalRChatContext DB = new SignalRChatContext();

            var userDetails = DB.Users.Where(x => x.Phone == iPhoneNum && x.SignInCodes.Code == iCode).Select(x => new UserCls {
                age = x.Age,
                firstName = x.FirstName,
                icon = x.Icon,
                id = x.UserId,
                lastName = x.LastName,
                phone = x.Phone,
                userName = x.UserName

            }).FirstOrDefault();

            return userDetails;

        } 

    }
}