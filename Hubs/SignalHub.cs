using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApi.Hubs
{
    public class SignalHub : Hub
    {
        public async Task SendPosition( string status, string user)
        {
            await Clients.Others.SendAsync("ReceivePosition",  status, user);
        }
    }
}
