using System;

namespace CardGame.Common.Classes.Db
{
    public class EntityTrackable
    {
        public Guid EntityId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
