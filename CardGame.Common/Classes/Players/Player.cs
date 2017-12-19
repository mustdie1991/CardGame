using System;
using System.Collections.Generic;
using System.Text;
using CardGame.Common.Classes.Cards;
using CardGame.Common.Enums;
using CardGame.Common.Interfaces.Players;

namespace CardGame.Common.Classes.Players
{
    public abstract class Player
    {
        public Guid PlayerGuid { get; set; }

        public string Name { get; set; }
    }
}
