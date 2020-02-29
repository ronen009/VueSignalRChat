using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Interfaces;
using server.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using server.Logic;
using server.DB;
using server.IOobjects;


namespace server.Controllers
{
    public class MainController : ControllerBase {

        public int ExractIdFromToken()
        {
            try {
                var token = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.FirstOrDefault();
                int userId = 0;
                if (JwtHandler.IsTokenValid(token, out userId) == false)
                {
                    return 0;
                }

                return userId;
            }
            catch (Exception ex) {
                return 1019;
            }
        }
    }
}
