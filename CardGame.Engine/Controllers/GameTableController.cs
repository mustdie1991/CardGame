using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardGame.Common.Classes.Cards;
using CardGame.Common.Classes.Players;
using CardGame.Common.Classes.Tables;
using CardGame.Common.Enums;
using CardGame.Engine.Configuration;

namespace CardGame.Engine.Controllers
{
    public class GameTableController
    {
        private readonly Table _gameTable;

        private readonly List<Player> _tablePlayers;

        private readonly ReserveCardsController _reserveCardsController;

        private readonly GameActionsController _actionsController;

        private readonly Queue<PlayerController> _playerMovesQueue;

        private readonly CardConfiguration _cardConfig;

        private readonly Stack<Card> _cardsOnTable; 

        private PlayerController _currentPlayer;

        private Dictionary<CardActions, Action<PlayerController>> Actions;

        private readonly TrashController _trash;
        
        public GameTableController(Table table)
        {
            this._gameTable = table;
            this._cardConfig = new CardConfiguration();
            this._reserveCardsController = new ReserveCardsController();
            this._playerMovesQueue = new Queue<PlayerController>();
            this._cardsOnTable = new Stack<Card>();
            this._tablePlayers = new List<Player>();
            this._trash = new TrashController();
            this._actionsController = new GameActionsController(this, this._reserveCardsController);
        }

        public void StartGame()
        {
            this.Setup();

            while (!this._reserveCardsController.GameOverConditions)
            {
                this.StartMove();

                this.ActionPhase();

                this.BuyPhase(1);

                this.EndMove();
            }

            var winner = this._playerMovesQueue.Aggregate((x, y) => x.TotalVictoryPoints > y.TotalVictoryPoints ? x : y);

            Console.WriteLine($"Game over! Winner : {this._tablePlayers.First(x => x.PlayerGuid == winner.PlayerGuid).Name}");
            Console.ReadKey();
        }

        public void AddPlayer(Player player)
        {
            this._tablePlayers.Add(player);
        }

        public PlayerController GetCurrentPlayerController(Guid playerId)
        {
            return this._currentPlayer;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(this._reserveCardsController.ToString());

            return sb.ToString();
        }

        private void StartMove()
        {
            this._currentPlayer = this._playerMovesQueue.Dequeue();
            
        }

        private void ActionPhase()
        {
            this._currentPlayer.ActionsRemaining = 1;
            if (!this._currentPlayer.HasActionCardsOnHand)
            {
                return;
            }

            while (this._currentPlayer.ActionsRemaining > 0)
            {
                Console.WriteLine("Choose action card");

                this._currentPlayer.PrintActionCards();
                Console.WriteLine("Select index of card : ");
                var key = Console.ReadLine();
                if (int.TryParse(key, out var k))
                {
                    var card = this._currentPlayer.TakeFromHand(new List<int> {k}).Single();
                    this._cardsOnTable.Push(card);
                    var a = card as ActionCard;
                    foreach (var cardAction in a.CardActions)
                    {
                        this._actionsController.ExecuteAction(this._currentPlayer.PlayerGuid, cardAction.CardActionName, cardAction.Parameters);
                    }
                    
                    this._currentPlayer.ActionsRemaining--;
                    Console.ReadKey();
                }
                else if (key.ToLower().Equals("q"))
                {
                    break;
                }

                Console.Clear();
            }
        }

        private void BuyPhase(int buys)
        {
            this._currentPlayer.BuysRemaining += 1;
            this._currentPlayer.BuyCoinsCount += this._currentPlayer.TotalTreasureValue;
            var sb = new StringBuilder();
            while (this._currentPlayer.BuysRemaining > 0)
            {
                this.PrintReserveDeckInfo();
                var cardsByPrice = this._reserveCardsController.GetPurchasableCards(this._currentPlayer.BuyCoinsCount);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Move start : {this._currentPlayer}");
                Console.ForegroundColor = ConsoleColor.Gray;
                sb.AppendLine($"Possible items to buy(0 to quit) ON HAND ({this._currentPlayer.BuyCoinsCount}) : ");
                cardsByPrice.ForEach(x =>
                {
                    sb.AppendLine($"{x.Name} - Cost : {x.Cost}. Key: {(int)x.CardName}");
                });
                Console.WriteLine(sb.ToString());
                sb.Clear();
                var readKey = Console.ReadLine();
                if (int.TryParse(readKey, out var selectedId) && cardsByPrice.Any(x => x.CardName == (CardNames) selectedId))
                {
                    var cardInfo = this._reserveCardsController.BuyCard((CardNames) selectedId);

                    this._currentPlayer.MoveCardsTo(new List<Card> {cardInfo}, CardDecks.Drop);
                    this._currentPlayer.BuysRemaining--;
                    this._currentPlayer.BuyCoinsCount -= cardInfo.Cost;
                }
                else if (readKey.StartsWith('q'))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid command. Try again.");
                }

                Console.Clear();
            }
        }

        private void PrintReserveDeckInfo()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Reserve ");
            Console.WriteLine(this._reserveCardsController.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void EndMove()
        {
            Console.WriteLine($"Move end : {this._currentPlayer}");
            this._currentPlayer.ActionsRemaining = 0;
            this._currentPlayer.BuyCoinsCount = 0;
            this._currentPlayer.BuysRemaining = 0;
            this._currentPlayer.GetNextCards();
            this._playerMovesQueue.Enqueue(this._currentPlayer);
        }

        private void Setup(List<CardNames> actionCards = null)
        {
            var cards = new List<Card>();
            cards.AddRange(Enumerable.Repeat(this._cardConfig.GetCardInfo(CardNames.Estate), 3));
            cards.AddRange(Enumerable.Repeat(this._cardConfig.GetCardInfo(CardNames.Copper), 7));

            var random = new Random();
            foreach (var player in this._tablePlayers.ToList().OrderBy(x => random.Next()))
            {
                var controller = new PlayerController(player);
                controller.MoveCardsTo(cards.ToList(), CardDecks.Drop);
                controller.GetNextCards();
                this._playerMovesQueue.Enqueue(controller);
            }

            this._reserveCardsController.SetReserveCards(actionCards);
        }

    }
}
