using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace server.IOobjects
{
    public class MsgObject {
        public int fromUserId { get; set; }
        public int toUserId { get; set; }
        public string msg { get; set; }
        public DateTime sentDate { get; set; }
    }

    public class chatMsgInput {
        public int toUserId { get; set; }
        public string msg { get; set; }
    }

    public class GetMsgHistoryInput {
        public int fromUserId { set; get; }
    }

    //create MsgOutput obj
}