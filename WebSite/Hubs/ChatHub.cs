using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebSite.Hubs
{
    public class ChatHub : Hub
    {
        public Task Send(string message)
        {
            return this.Clients.All.InvokeAsync("Send", message);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}
