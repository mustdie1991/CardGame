using System.Collections.Generic;
using CardGame.Common.Classes.Cards;

namespace CardGame.Engine.Controllers
{
    public class TrashController
    {
        private readonly List<Card> _trashedCards;

        public TrashController()
        {
            this._trashedCards = new List<Card>();
        }

        public void Trash(Card cards)
        {
            this._trashedCards.Add(cards);
        }

        public void Trash(List<Card> cards)
        {
            this._trashedCards.AddRange(cards);
        }
    }
}