using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Common.Classes.Cards;
using CardGame.Common.Enums;
using CardGame.Common.Interfaces.GameActions;
using CardGame.Engine.Controllers;

namespace CardGame.Engine.GameActions.InteractiveActions
{
    public class WorkshopAction : IInteractiveAction<PlayerController, ReserveCardsController>
    {
        private const int Credit = 4;

        public void Intreact(PlayerController obj, ReserveCardsController subject)
        {
            var possibleCards = subject.GetPurchasableCards(Credit);

            Card selectedCard = null;
            while (selectedCard == null)
            {
                Console.Clear();
                Console.WriteLine("Select card :");
                this.PrintCards(possibleCards);
                if (int.TryParse(Console.ReadLine(), out var idx) && idx > -1 && idx < possibleCards.Count - 1)
                {
                    selectedCard = possibleCards[idx];
                }
            }

            obj.MoveCardTo(subject.BuyCard(selectedCard.CardName), CardDecks.Drop);
        }

        private void PrintCards(IEnumerable<Card> cards)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"{string.Join(", ", cards.Select((x, idx) => $"{x.CardName} idx : { idx }"))}");

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}