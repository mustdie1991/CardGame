using CardGame.Common.Classes.Db;

namespace CardGame.Db.Entities
{
    public class GameCard: EntityTrackable
    {
        public string CardNameEn { get; set; }

        public string CardNameRu { get; set; }
    }
}
