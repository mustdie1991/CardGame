using System;
using System.Collections.Generic;
using CardGame.Common.Classes.Players;
using CardGame.Common.Classes.Tables.Common;
using CardGame.Engine.Controllers;

namespace CardGame.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new PlaygroundController();

            var vasia = new HumanPlayer {Name = "Pidor ", PlayerGuid = Guid.NewGuid()};
            var petya = new HumanPlayer {Name = "Hui ", PlayerGuid = Guid.NewGuid()};

            var table = new DefaultTable {TableGuid = Guid.NewGuid(), TableName = "Table nahui "};

            app.AddTable(table);
            app.AddPlayerToTable(table.TableGuid, vasia);
            app.AddPlayerToTable(table.TableGuid, petya);
            app.StartTableGame(table.TableGuid);

            Console.ReadKey();
        }
    }
}
