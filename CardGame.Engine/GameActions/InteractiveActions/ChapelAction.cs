using System;
using System.Collections.Generic;
using CardGame.Common.Interfaces.GameActions;
using CardGame.Engine.Controllers;

namespace CardGame.Engine.GameActions.InteractiveActions
{
    public class ChapelAction : IInteractiveAction<TrashController, PlayerController>
    {
        public void Intreact(TrashController obj, PlayerController subject)
        {
            var ids = new List<int>();
            Console.Clear();
            subject.PrintCardsOnHand();
            Console.WriteLine("Select cards to drop : ");
            while (true)
            {
                var input = Console.ReadLine();
                if (input.Length == 1 && input.ToLower().StartsWith("q"))
                {
                    break;
                }

                if (int.TryParse(input, out var idx) && idx > -1 && idx <= subject.CardsOnHandCount - 1 && !ids.Contains(idx))
                {
                    ids.Add(idx);
                }
            }

            obj.Trash(subject.TakeFromHand(ids));
        }
    }
}
