using System;
using CardGame.Db.Entities;
using CardGame.Db.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserRepo _repo;

        public UserController(IUserRepo repo)
        {
            this._repo = repo;
        }
        
        [HttpGet("{id}")]
        public ActionResult Get(Guid roomId)
        {
            return this.Json(this._repo.GetOne(roomId));
        }

        [HttpGet]
        public ActionResult Get()
        {
            return this.Json(this._repo.GetAll());
        }

        [HttpPost]
        public ActionResult Post(User user)
        {
            return this.Ok(this._repo.Add(user));
        }

        [HttpDelete]
        public ActionResult Delete(Guid roomId)
        {
            this._repo.Remove(roomId);
            return this.Ok("Deleted!");
        }
    }
}