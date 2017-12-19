using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Common.Classes.Cards;
using CardGame.Common.Enums;
using CardGame.Common.Interfaces.GameActions;
using CardGame.Engine.Controllers;

namespace CardGame.Engine.GameActions.InteractiveActions.Advanced
{
    public class MineAction: IAdvancedIntreactiveAction<PlayerController, ReserveCardsController, TrashController>
    {
        private const int CardGreaterBy = 3;

        public void Interact(PlayerController subject, ReserveCardsController firstObject, TrashController secondObject)
        {
            if (!subject.HasConvertableCoins)
            {
                return;
            }

            var possibleCards = firstObject.GetPurchasableCards(6).Where(x => x is TreasureCard).ToList();
            var exchange = this.SelectCardLoop(subject, possibleCards, out var card);
            if (!exchange.Any()) return;

            var index = this.ExchangeLoop(exchange);
            var newCard = firstObject.BuyCard(exchange[index].CardName);
            secondObject.Trash(card);
            subject.MoveCardTo(newCard, CardDecks.OnHand);
        }

        // Loop to get possible cards.
        private int ExchangeLoop(IReadOnlyCollection<Card> exchange)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select card idx :");
                this.PrintCards(exchange);
                if ((!int.TryParse(Console.ReadLine(), out var exchangeIdx) || exchangeIdx != -1) && exchangeIdx <= exchange.Count - 1) continue;

                return exchangeIdx;
            }
        }

        // Loop to select card from hand;
        private List<Card> SelectCardLoop(PlayerController ctrl, List<Card> treasureCardsInReserve, out Card selectedCard)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select card index to convert");
                ctrl.PrintTreasureCards();
                if (!int.TryParse(Console.ReadLine(), out var idx) || idx <= -1) continue;
                selectedCard = ctrl.TakeFromHand(new List<int> { idx }).Single();
                var card = selectedCard;
                var exchange = treasureCardsInReserve.Where(x => x.Cost <= card.Cost + CardGreaterBy).ToList();
                if (exchange.Any())
                {
                    return exchange;
                }

                ctrl.MoveCardTo(selectedCard, CardDecks.OnHand);
            }
        }

        private void PrintCards(IEnumerable<Card> cards)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"{string.Join(", ", cards.Select((x, idx) => $"{x.CardName} idx : { idx }"))}");

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
