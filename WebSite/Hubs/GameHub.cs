using System;
using System.Threading.Tasks;
using CardGame.Db.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace WebSite.Hubs
{
    public class GameHub : Hub
    {
        private readonly IRoomRepo _roomRepo;

        public GameHub(IRoomRepo roomRepo, IUserRepo userRepo)
        {
            this._roomRepo = roomRepo;
        }

        public Task JoinRoom(string roomName)
        {
            return this.Groups.AddAsync(this.Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return this.Groups.RemoveAsync(this.Context.ConnectionId, roomName);
        }
    }
}
