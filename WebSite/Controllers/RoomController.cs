using System;
using CardGame.Db.Entities;
using CardGame.Db.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [Produces("application/json")]
    [Route("api/room")]
    public class RoomController : Controller
    {
        private readonly IRoomRepo _roomRepo;

        public RoomController(IRoomRepo roomRepo)
        {
            this._roomRepo = roomRepo;
        }
        
        [HttpGet("{id}")]
        public ActionResult Get(Guid roomId)
        {
            return this.Json(this._roomRepo.GetOne(roomId));
        }
         
        [HttpGet]
        public ActionResult Get()
        {
            return this.Json(this._roomRepo.GetAll());
        }

        [HttpPost]
        public ActionResult Post(string roomName)
        {
            var room = new Room
            {
                RoomName = roomName,
                Created = DateTime.Now
            };

            return this.Ok(this._roomRepo.Add(room));
        }

        [HttpDelete]
        public ActionResult Delete(Guid roomId)
        {
            this._roomRepo.Remove(roomId);
            return this.Ok("Deleted!");
        }
    }
}