using System;
using CardGame.Engine.Controllers;

namespace CardGame.Engine.GameActions
{
    public abstract class GameAction
    {
        public abstract void DoAction(Guid playerId, GameTableController tableController);

        public abstract void UndoAction(Guid playerId, GameTableController tableController);
    }
}
