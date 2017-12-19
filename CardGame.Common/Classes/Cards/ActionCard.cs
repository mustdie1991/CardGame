using System;
using System.Collections.Generic;
using System.Text;
using CardGame.Common.Enums;

namespace CardGame.Common.Classes.Cards
{
    public class ActionCard: Card
    {
        public List<CardAction> CardActions { get; set; }

        public ActionCard()
        {
            this.CardActions = new List<CardAction>();
        }
    }
}
