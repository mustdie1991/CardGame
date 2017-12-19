using System;
using System.Collections.Generic;
using System.Text;
using CardGame.Common.Interfaces.GameActions;
using CardGame.Engine.Controllers;

namespace CardGame.Engine.GameActions.InteractiveActions
{
    public class SpyAction : IInteractiveAction<PlayerController, PlayerController>
    {
        public void Intreact(PlayerController obj, PlayerController subject)
        {
            throw new NotImplementedException();
        }
    }
}
