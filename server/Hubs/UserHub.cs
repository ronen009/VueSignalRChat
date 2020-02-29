using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using server.Interfaces;
using server.IOobjects;

namespace server.Hubs
{
    public class UserHub : Hub<IUserHub> {

    }
}
