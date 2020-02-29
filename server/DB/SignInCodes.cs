using System;
using System.Collections.Generic;

namespace server.DB
{
    public partial class SignInCodes
    {
        public int CodeId { get; set; }
        public int UserId { get; set; }
        public int Code { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual Users User { get; set; }
    }
}
