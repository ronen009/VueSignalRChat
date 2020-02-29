using System;
using System.Collections.Generic;

namespace server.DB
{
    public partial class Messages
    {
        public long UsersChatId { get; set; }
        public int MsgFromUserId { get; set; }
        public int MsgToUserId { get; set; }
        public string Msg { get; set; }
        public DateTime SendDate { get; set; }

        public virtual Users MsgFromUser { get; set; }
        public virtual Users MsgToUser { get; set; }
    }
}
