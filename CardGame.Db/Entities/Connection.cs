using CardGame.Common.Classes.Db;

namespace CardGame.Db.Entities
{
    public class Connection: EntityTrackable
    {
        public string ConnectionId { get; set; }

        public string UserAgent { get; set; }

        public bool Connected { get; set; }
    }
}
