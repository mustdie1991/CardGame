using CardGame.Common.Interfaces.GameActions;
using CardGame.Engine.Controllers;

namespace CardGame.Engine.GameActions.SimpleActions
{
    public class MilitiaAction : ISimpleAction<PlayerController>
    {
        private const int LeftCardsOnHand = 3;

        public void Act(PlayerController subject)
        {
            subject.RequestDropCardsUntilLeft(LeftCardsOnHand);
        }
    }
}
