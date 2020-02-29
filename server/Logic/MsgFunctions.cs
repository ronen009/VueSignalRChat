using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using server.IOobjects;
using server.DB;

namespace server.Logic
{
    public static class MsgFunctions
    {

        public static List<MsgObject> GetChatMsgByUser(int iUserId, int iPage, int iBulk, SignalRChatContext iDb)
        {

            List<MsgObject> res = iDb.Messages.Where(x => x.MsgFromUserId == iUserId).Select(x => new MsgObject
            {
                fromUserId = x.MsgFromUserId,
                msg = x.Msg,
                sentDate = x.SendDate,
                toUserId = x.MsgToUserId,
            }).OrderByDescending(x => x.sentDate).Skip(iPage * iBulk).Take(iBulk).ToList();

            return res;
        }

        public static List<MsgObject> GetChatHistory(int iFromUserId, int iToUserId , SignalRChatContext iDb) {

            if( iDb == null) {
                iDb = new SignalRChatContext();
            }

            List<MsgObject> res = iDb.Messages.Where(x => (x.MsgFromUserId == iFromUserId && x.MsgToUserId == iToUserId) || (x.MsgFromUserId == iToUserId && x.MsgToUserId == iFromUserId)).Select( x => new MsgObject {
                msg = x.Msg,
                sentDate = x.SendDate,
                fromUserId = x.MsgFromUserId, //user id should not be returned.
                toUserId = x.MsgToUserId
            }).OrderBy(x => x.sentDate).ToList();

            return res;
        }


        public static MsgObject SendMsg(int iFromUser, int iToUser, string iMsg, SignalRChatContext iDb)
        {
                
                var msgToAdd = new Messages();
                msgToAdd.Msg = iMsg;
                msgToAdd.MsgFromUserId = iFromUser;
                msgToAdd.MsgToUserId = iToUser;
                msgToAdd.SendDate = DateTime.Now;

                iDb.Messages.Add(msgToAdd);
                iDb.SaveChanges();

                MsgObject ret = new MsgObject();
                ret.msg = msgToAdd.Msg;
                ret.fromUserId = msgToAdd.MsgFromUserId;
                ret.toUserId = msgToAdd.MsgToUserId;
                ret.sentDate = msgToAdd.SendDate;

                return ret;
        }


    }
}