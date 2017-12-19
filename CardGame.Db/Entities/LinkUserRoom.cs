using System;

namespace CardGame.Db.Entities
{
    public class LinkUserRoom
    {
        public Guid UserId { get; set; }

        public Guid GameTableId { get; set; }

        public User User { get; set; }

        public Room GameRoom { get; set; }
    }
}
