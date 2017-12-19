using System;
using System.Collections.Generic;
using CardGame.Common.Classes.Db;

namespace CardGame.Db.Entities
{
    public class User : EntityTrackable
    {
        public string Login { get; set; }

        public virtual List<Connection> Connections { get; set; }

        public virtual List<Room> ConnectedToRooms { get; set; }
    }
}