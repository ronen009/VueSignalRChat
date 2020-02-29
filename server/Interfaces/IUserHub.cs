using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Interfaces {

    public interface IUserHub 
    {
        Task AddUserEvent(int userId);

        Task LogOutUserEvent(int userId);

        Task OnDisconnected(bool stopCalled);
    }
}
