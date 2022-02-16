using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndChallenge
{
    public class CryptoHub : Hub
    {
        public async Task SendAsync()
        {
            await Clients.All.SendAsync("GetData");
        }
    }
}
