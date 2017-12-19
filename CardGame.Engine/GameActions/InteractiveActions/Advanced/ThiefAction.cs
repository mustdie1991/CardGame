using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Common.Classes.Cards;
using CardGame.Common.Enums;
using CardGame.Common.Interfaces.GameActions;
using CardGame.Engine.Controllers;

namespace CardGame.Engine.GameActions.InteractiveActions.Advanced
{
    public class ThiefAction : IAdvancedIntreactiveAction<PlayerController, PlayerController, TrashController>
    {
        public const int RequestCardsCount = 2;

        public void Interact(PlayerController subject, PlayerController firstObject, TrashController secondObject)
        {
            var cards = firstObject.RequestTopCardsFromDeck(RequestCardsCount);
            firstObject.MoveCardsTo(cards.Where(x => !(x is TreasureCard)).ToList(), CardDecks.Drop);
            cards = cards.Where(x => !(x is TreasureCard)).ToList();
            Card selectedCard = null;
            if (cards.Count > 1)
            {
                Console.Clear();
                Console.WriteLine($"Select card to drop : {cards.Select((x, idx) => $"{x.CardName} idx: {idx}")}");
                while (int.TryParse(Console.ReadLine(), out var idx) && idx == -1 || idx < cards.Count - 1)
                {
                    selectedCard = cards[idx];
                    cards.RemoveAt(idx);
                    firstObject.MoveCardsTo(cards, CardDecks.Drop);
                }
            }
            else if (cards.Count == 1)
            {
                selectedCard = cards.First();
            }

            if (selectedCard == null)
            {
                return;
            }


            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Take card {selectedCard.CardName} in your deck?(y/n)");
                var answer = Console.ReadLine();
                if (answer.ToLower() == "y")
                {
                    subject.MoveCardsTo(new List<Card> {selectedCard}, CardDecks.Drop);
                    break;
                }

                if (answer.ToLower() == "n")
                {
                    secondObject.Trash(selectedCard);
                    break;
                }
            }
        }
    }
}
