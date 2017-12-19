using System.Collections.Generic;
using CardGame.Common.Enums;

namespace CardGame.Common.Classes.Cards
{
    public class CardAction
    {
        public CardActions CardActionName { get; set; }

        public Dictionary<string, object> Parameters { get; set; }
    }
}
