using System;
using System.Collections.Generic;

namespace server.DB
{
    public partial class Users
    {
        public Users()
        {
            MessagesMsgFromUser = new HashSet<Messages>();
            MessagesMsgToUser = new HashSet<Messages>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public short Age { get; set; }
        public DateTime CreationDate { get; set; }
        public string Icon { get; set; }

        public virtual SignInCodes SignInCodes { get; set; }
        public virtual ICollection<Messages> MessagesMsgFromUser { get; set; }
        public virtual ICollection<Messages> MessagesMsgToUser { get; set; }
    }
}
