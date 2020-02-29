using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace server.Interfaces
{
    public interface IMsgHub {

        Task SentMsgEvent(int fromUserId , int toUserId, string msg);


    }
}
