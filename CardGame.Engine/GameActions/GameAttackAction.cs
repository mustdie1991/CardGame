using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardGame.Common.Classes.Cards;
using CardGame.Common.Classes.Players;
using CardGame.Common.Enums;
using CardGame.Common.Interfaces.GameActions;
using CardGame.Engine.Controllers;

namespace CardGame.Engine.GameActions
{
    public interface IAttackAction<T>
    {
        void Attack(T attackItem);
    }

    public interface ISimpleAction
    {
        void DoAction(params object [] items);
    }
    

    

    public class FeastAction : IAdvancedIntreactiveAction<PlayerController, ReserveCardsController, TrashController>
    {
        public void Interact(PlayerController subject, ReserveCardsController firstObject, TrashController secondObject)
        {
            
        }
    }

   
}
