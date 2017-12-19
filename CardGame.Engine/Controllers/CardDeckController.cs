using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardGame.Common.Classes.Cards;
using CardGame.Common.Enums;

namespace CardGame.Engine.Controllers
{
    public class CardDeckController
    {
        public bool HasActionCardsOnHand => this.OnHandCards.Any(x => x is ActionCard);

        public int TreasuresOnHand => this.OnHandCards.Where(x => x is TreasureCard).Cast<TreasureCard>().Sum(x => x.TreasureValue);

        public int VictoryPointsValue => this.OnHandCards.Where(x => x is VictoryCard).Cast<VictoryCard>().Sum(x => x.VictoryPoints) 
            + this.DropCards.VictoryPointsValue + this.InDeckCards.TreasuresValue;

        public readonly List<Card> OnHandCards; 

        private readonly Dictionary<CardDecks, CardDeck> _playerDecks;

        private CardDeck DropCards => this._playerDecks[CardDecks.Drop];

        private CardDeck InDeckCards => this._playerDecks[CardDecks.InDeck];


        public CardDeckController()
        {
            this._playerDecks = new Dictionary<CardDecks, CardDeck>
            {
                {CardDecks.Drop, new CardDeck()},
                {CardDecks.InDeck, new CardDeck()},
                {CardDecks.OnHand, new CardDeck()}
            };

            this.OnHandCards = new List<Card>();
        }

        public void AddCardsTo(List<Card> cards, CardDecks toDeck)
        {
            if (toDeck == CardDecks.OnHand)
            {
                this.OnHandCards.AddRange(cards);
            }
            else
            {
                this._playerDecks[toDeck].AddCards(cards);
            }
        }

        public List<Card> GetFromHand(List<int> indices)
        {
            var cards = new List<Card>();
            indices.ForEach(x =>
            {
                cards.Add(this.OnHandCards[x]);
                this.OnHandCards.RemoveAt(x);
            });

            return cards;
        }

        public List<Card> GetTopFrom(CardDecks deckName, int count)
        {
            return this._playerDecks[deckName].Take(count);
        }

        public void TopdeckCards(List<Card> cards)
        {
            this.InDeckCards.AddCards(cards);
        }

        public void GetNextCards()
        {
            this.DropCards.AddCards(this.OnHandCards.ToList());
            this.OnHandCards.Clear();

            // If all cards taken or no more cards in drop and deck.
            while (this.OnHandCards.Count != 5 || !this.InDeckCards.HasCardsInDeck && !this.DropCards.HasCardsInDeck)
            {
                if (this.InDeckCards.HasCardsInDeck)
                {
                    this.OnHandCards.Add(this.InDeckCards.Take(1).FirstOrDefault());
                }
                // If there are cards left, then move them to drop and shuffle
                else
                {
                    this.CreateNewDeck();
                }                
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"In deck : {this.InDeckCards}");
            stringBuilder.AppendLine($"On hand : {string.Join(",", this.OnHandCards.Select(x => x.CardName))}");
            stringBuilder.AppendLine($"In drop : {this.DropCards}");

            return stringBuilder.ToString();
        }

        private void CreateNewDeck()
        {
            this.InDeckCards.AddCards(this.DropCards.TakeAll());
            this.InDeckCards.Shuffle();
        }
    }
}
