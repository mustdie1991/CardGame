using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardGame.Common.Classes.Cards;
using CardGame.Common.Classes.Players;
using CardGame.Common.Enums;

namespace CardGame.Engine.Controllers
{
    public class PlayerController
    {
        public bool HasActionCardsOnHand => this._cardDeckController.HasActionCardsOnHand;

        public int CardsOnHandCount => this._cardDeckController.OnHandCards.Count;

        public int TotalTreasureValue => this._cardDeckController.TreasuresOnHand;

        public int TotalVictoryPoints => this._cardDeckController.VictoryPointsValue;

        public bool HasConvertableCoins => this._cardDeckController.OnHandCards.Any(x => x.CardName == CardNames.Silver || x.CardName == CardNames.Copper);

        public int ActionsRemaining { get; set; }

        public int BuysRemaining { get; set; }

        public int BuyCoinsCount { get; set; }

        public Guid PlayerGuid => this._player.PlayerGuid;

        private readonly Player _player;

        private readonly CardDeckController _cardDeckController;

        public PlayerController(Player player)
        {
            this._player = player;
            this._cardDeckController = new CardDeckController();
        }

        public void MoveCardsTo(List<Card> cards, CardDecks deck)
        {
            this._cardDeckController.AddCardsTo(cards, deck);
        }

        public void MoveCardTo(Card card, CardDecks deck)
        {
            this._cardDeckController.AddCardsTo(new List<Card> {card}, deck);
        }

        public List<Card> TakeNextCards(int count)
        {
            return this._cardDeckController.GetTopFrom(CardDecks.InDeck, count);
        }

        public List<Card> TakeFromHand(List<int> indices)
        {
            return this._cardDeckController.GetFromHand(indices);
        }

        public void GetNextCards()
        {
            this._cardDeckController.GetNextCards();
        }

        public void UseCard(int idx)
        {
            Console.WriteLine((this._cardDeckController.OnHandCards[idx] as ActionCard)?.Name);
        }


        // Attack actions
        public void RequestDropCardsUntilLeft(int leftCount)
        {
            while (this.CardsOnHandCount > leftCount)
            {
                Console.Clear();
                Console.WriteLine($"Drop cards on hand (LEFT {this.CardsOnHandCount - leftCount} : ");
                this.PrintCardsOnHand();
                if (!int.TryParse(Console.ReadLine(), out var id) || id <= -1) continue;

                var cards = this._cardDeckController.GetFromHand(new List<int> {id});
                this._cardDeckController.AddCardsTo(cards, CardDecks.Drop);
            }
        }

        public List<Card> RequestTopCardsFromDeck(int count)
        {
            return this._cardDeckController.GetTopFrom(CardDecks.InDeck, count);
        }

        public void PrintActionCards()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            var sb = new StringBuilder();
            var onHandCards = this._cardDeckController.OnHandCards;
            for (var i = 0; i < onHandCards.Count; i++)
            {
                if (onHandCards[i] is ActionCard)
                {
                    sb.AppendLine($"Card {onHandCards[i].CardName} Index {i}");
                }
            }

            Console.WriteLine(sb.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintTreasureCards()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            var sb = new StringBuilder();
            var onHandCards = this._cardDeckController.OnHandCards;
            for (var i = 0; i < onHandCards.Count; i++)
            {
                if (onHandCards[i] is TreasureCard)
                {
                    sb.AppendLine($"Card {onHandCards[i].CardName} Index {i}");
                }
            }

            Console.WriteLine(sb.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }


        public void PrintCardsOnHand()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            var cards = this._cardDeckController.OnHandCards;
            for (int i = 0; i < cards.Count; i++)
            {
                Console.WriteLine($"{cards[i].CardName} Idx : {i}");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public override string ToString()
        {
            return $"Player {this._player.Name} Guid: {this._player.PlayerGuid}" +
                   $"\n Cards: {string.Join(",", this._cardDeckController.OnHandCards)}" +
                   $"\nMoves: {this.ActionsRemaining}, Buys: {this.BuysRemaining}, Coins: {this.BuyCoinsCount}";
        }
    }
}
