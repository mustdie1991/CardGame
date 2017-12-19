using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    public class ChatHub : Hub
    {
        public Task Send(string message)
        {
            return this.Clients.All.InvokeAsync("Send", message);
        }
    }
}
