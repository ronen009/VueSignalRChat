using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace server.IOobjects {
    public class UserPartial {
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string icon { get; set; }
        public short age { get; set; }
    }

    public class UserCls : UserPartial  {

        // fake id should be passed to client
        public int id { get; set; }
        //code should be sent to mail/phone 
        public int? code { get; set; }
        public DateTime creationDate { get; set; }
        public string token { get; set; }

    }


    public class JwtTokenObject {
        public int userId { get; set; }
        public string userName { get; set; }
        public DateTime sentDate {get;set;}
    }


    public class loginCls
    {
        public string phone { get; set; }
        public int code { get; set; }
    }

    public class ReceiveCodeInput { 
        public string phoneNum { get; set; }
    }

    public class ValidateCodeOutput {
        public int userId { get; set; }
        public string userName { get; set; }
    }
}