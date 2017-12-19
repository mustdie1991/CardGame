using CardGame.Common.Enums;
using CardGame.Common.Interfaces.GameActions;
using CardGame.Engine.Controllers;

namespace CardGame.Engine.GameActions.InteractiveActions
{
    public class WitchAction : IInteractiveAction<PlayerController, ReserveCardsController>
    {
        public void Intreact(PlayerController obj, ReserveCardsController subject)
        {
            obj.MoveCardTo(subject.BuyCard(CardNames.Curse), CardDecks.Drop);
        }
    }
}
