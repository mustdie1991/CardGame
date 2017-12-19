using System;
using System.Collections.Generic;
using CardGame.Common.Classes.Players;
using CardGame.Common.Classes.Tables;

namespace CardGame.Engine.Controllers
{
    public class PlaygroundController
    {
        private readonly Dictionary<Guid, GameTableController> _tables;

        public PlaygroundController()
        {
            this._tables = new Dictionary<Guid, GameTableController>();
        }

        public void StartTableGame(Guid tableId)
        {
            this.GetTable(tableId).StartGame();
            Console.WriteLine(this.GetTable(tableId));
        }

        public void AddTable(Table table)
        {
            this._tables.Add(table.TableGuid, new GameTableController(table));
        }

        public void AddPlayerToTable(Guid tableId, Player player)
        {
            this.GetTable(tableId).AddPlayer(player);
        }

        public GameTableController GetTable(Guid tableId)
        {
            if (this._tables.TryGetValue(tableId, out var ctrl) && ctrl != null)
            {
                return ctrl;
            }

            throw new NullReferenceException($"Non registered table with GUID {tableId}");
        }
    }
}
