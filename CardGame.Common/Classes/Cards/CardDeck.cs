using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Common.Classes.Cards
{
    public class CardDeck
    {
        private Stack<Card> _cards;

        public int CardCount => this._cards.Count;

        public bool HasCardsInDeck => this._cards.Any();

        public bool HasActions => this._cards.Any(x => x is ActionCard);

        public int TreasuresValue => this._cards.Where(x => x is TreasureCard).Cast<TreasureCard>().Sum(x => x.TreasureValue);

        public int VictoryPointsValue => this._cards.Where(x => x is VictoryCard).Cast<VictoryCard>().Sum(x => x.VictoryPoints);

        public CardDeck()
        {
            this._cards = new Stack<Card>();
        }

        public void Shuffle()
        {
            var rnd = new Random();
            this._cards = new Stack<Card>(this._cards.ToList().OrderBy(x => rnd.Next()).ToList());
        }

        public List<Card> Take(int count)
        {
            var collection = new List<Card>();
            for (var i = 0; i < count; i++)
            {
                collection.Add(this._cards.Pop());
            }

            return collection;
        }

        public List<Card> TakeAll()
        {
            var collection = new List<Card>();

            while (this._cards.Any())
            {
                collection.Add(this._cards.Pop());
            }

            return collection;
        }

        public List<Card> GetActionCards()
        {
            return this._cards.Where(x => x is ActionCard).ToList();
        }

        public void AddCards(List<Card> cards)
        {
            cards.ForEach(card =>
            {
                this._cards.Push(card);
            });
        }

        public void Clear()
        {
            this._cards.Clear();
        }

        public Card this[int idx] => this._cards.ToList()[idx];

        public override string ToString()
        {
            return $"Cards : {string.Join(", ", this._cards.Select(x => x.Name))}";
        }
    }
}
