using System;
using System.Collections.Generic;
using CardGame.Common.Classes.Db;

namespace CardGame.Db.Entities
{
    public class Room : EntityTrackable
    {
        public string RoomName { get; set; }

        public virtual List<LinkUserRoom> ConnectedUsers { get; set; } = new List<LinkUserRoom>();
    }
}
