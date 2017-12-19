using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardGame.Common.Classes.Cards;
using CardGame.Common.Enums;
using CardGame.Engine.Configuration;

namespace CardGame.Engine.Controllers
{
    public class ReserveCardsController
    {
        public bool GameOverConditions => this.ThreePilesEmpty || this.NoProvincesLeft;

        private bool ThreePilesEmpty => this._reserveDecks.Count(x => x.CardsLeft == 0) >= 3;

        private bool NoProvincesLeft => this._reserveDecks.First(x => x.CardInfo.CardName == CardNames.Province).CardsLeft == 0;

        private readonly List<ReserveDeck> _reserveDecks;

        private readonly CardConfiguration _cardConfig;

        private const int CardsInDeck = 10;

        private const int ActionCardDecksCount = 10;

        public List<Card> GetPurchasableCards(int coins)
        {
            return this._reserveDecks.Where(x => x.CardInfo.Cost <= coins && x.CardsLeft > 0).Select(x => x.CardInfo).ToList();
        }

        public Card BuyCard(CardNames name)
        {
            var reserveDeck = this._reserveDecks.First(x => x.CardInfo.CardName == name);
            if (reserveDeck.CardsLeft <= 0)
            {
                throw new InvalidOperationException($"Not able to get card {name.ToString()} from deck. Deck is empty.");
            }

            reserveDeck.CardsLeft--;
            return reserveDeck.CardInfo;
        }

        public bool HasReserveCard(CardNames cardName)
        {
            return this._reserveDecks.SingleOrDefault(x => x.CardInfo.CardName == cardName && x.CardsLeft > 0) != null;
        }

        public ReserveCardsController()
        {
            this._reserveDecks = new List<ReserveDeck>();
            this._cardConfig = new CardConfiguration();
        }

        public void SetReserveCards(List<CardNames> cardNames = null)
        {
            this._reserveDecks.Clear();
            this.InitializeBaseCards();
            if (cardNames != null && cardNames.Any())
            {
                cardNames.ForEach(x =>
                {
                    this.AddDeckToReserve(x, CardsInDeck);
                });
            }
            else
            {
                this.CardRandomizer();
            }
        }

        private void InitializeBaseCards()
        {
            this.AddDeckToReserve(CardNames.Estate, 8);
            this.AddDeckToReserve(CardNames.Province, 8);
            this.AddDeckToReserve(CardNames.Duchy, 8);
            this.AddDeckToReserve(CardNames.Curse, 20);

            this.AddDeckToReserve(CardNames.Copper, 10);
            this.AddDeckToReserve(CardNames.Silver, 10);
            this.AddDeckToReserve(CardNames.Gold, 10);
        }

        private CardNames GetUniqueName(ICollection<CardNames> excludedItems)
        {
            while (true)
            {
                var cards = Enum.GetValues(typeof(CardNames));
                var random = new Random();
                var cardName = (CardNames) cards.GetValue(random.Next(cards.Length));
                if (this._reserveDecks.All(x => x.CardInfo.CardName != cardName)) return cardName;

                excludedItems.Add(cardName);
            }
        }

        private void CardRandomizer()
        {
            for (var i = 0; i < ActionCardDecksCount; i++)
            {
                this.AddDeckToReserve(this.GetUniqueName(new List<CardNames>()), CardsInDeck);
            }
        }

        private void AddDeckToReserve(CardNames name, int count)
        {
            this._reserveDecks.Add(new ReserveDeck { CardInfo = this._cardConfig.GetCardInfo(name), CardsLeft = count });
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var reserveDeck in this._reserveDecks)
            {
                sb.AppendLine($"{reserveDeck.CardInfo.Name}({reserveDeck.CardsLeft})");
            }

            return sb.ToString();
        }

        internal class ReserveDeck
        {
            public int CardsLeft { get; set; }

            public Card CardInfo { get; set; }
        }
    }
}
