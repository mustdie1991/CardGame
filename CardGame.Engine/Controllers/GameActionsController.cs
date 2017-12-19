using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CardGame.Common.Enums;
using CardGame.Common.Interfaces.GameActions;
using CardGame.Engine.Configuration;
using CardGame.Engine.GameActions;

namespace CardGame.Engine.Controllers
{
    public class GameActionsController
    {
        private readonly GameTableController _tableCtrl;

        private readonly ReserveCardsController _reserveCtrl;

       // private readonly Stack<IGameAction> _actionsHistory;

        private readonly Dictionary<CardActions, Action<Guid, Dictionary<string, object>>> _actions;

        public GameActionsController(GameTableController tableCtrl, ReserveCardsController reserveCtrl)
        {
          //  this._actionsHistory = new Stack<IGameAction>();
            this._tableCtrl = tableCtrl;
            this._reserveCtrl = reserveCtrl;
            this._actions = new Dictionary<CardActions, Action<Guid, Dictionary<string, object>>>
            {
                { CardActions.EarnBuyCoins, this.GainBuyCoins },
                { CardActions.EarnBuys, this.GainBuys },
                { CardActions.EarnCards, this.GetFromDeck },
                { CardActions.EarnMoves, this.GainActions }
            };
        }

        public void ExecuteAction(Guid playerId, CardActions action, Dictionary<string, object> parameters)
        {
            this._actions[action](playerId, parameters);
        }

        public void UndoPreviousAction()
        {
            
        }

        private void GetFromDeck(Guid playerId, Dictionary<string, object> parameters)
        {
            var playerCtrl = this.GetPlayerController(playerId);
            var count = this.GetParamFromDictionary<int>(CardConfiguration.CountName, parameters);
            var nextCards = playerCtrl.TakeNextCards(count);
            playerCtrl.MoveCardsTo(nextCards, CardDecks.OnHand);
        }

        private void DropFromHand(Guid playerId, Dictionary<string, object> parameters)
        {
            var playerCtrl = this.GetPlayerController(playerId);
            var indices = this.GetParamFromDictionary<List<int>>(CardConfiguration.Indices, parameters);
            var cards = playerCtrl.TakeFromHand(indices);
            playerCtrl.MoveCardsTo(cards, CardDecks.Drop);
        }

        private void GainActions(Guid playerId, Dictionary<string, object> parameters)
        {
            var playerCtrl = this.GetPlayerController(playerId);
            playerCtrl.ActionsRemaining += this.GetParamFromDictionary<int>(CardConfiguration.CountName, parameters);
        }

        private void GainBuys(Guid playerId, Dictionary<string, object> parameters)
        {
            var playerCtrl = this.GetPlayerController(playerId);
            playerCtrl.BuysRemaining += this.GetParamFromDictionary<int>(CardConfiguration.CountName, parameters);
        }

        private void GainBuyCoins(Guid playerId, Dictionary<string, object> parameters)
        {
            var playerCtrl = this.GetPlayerController(playerId);
            playerCtrl.BuyCoinsCount += this.GetParamFromDictionary<int>(CardConfiguration.CostName, parameters);
        }

        private void MoveToTrash(Guid playerId, Dictionary<string, object> parameters)
        {
            var playerCtrl = this.GetPlayerController(playerId);
            var cards = playerCtrl.TakeFromHand(this.GetParamFromDictionary<List<int>>(CardConfiguration.Indices, parameters));
           // this._tableCtrl.MoveToTrash(cards);
        }
        
        private PlayerController GetPlayerController(Guid playerId)
        {
            return this._tableCtrl.GetCurrentPlayerController(playerId);
        }

        private T GetParamFromDictionary<T>(string name, Dictionary<string, object> parameters)
        {
            if (parameters.TryGetValue(name, out var parameter))
            {
                return (T) parameter;
            }

            return default(T);
        }
    }
}
