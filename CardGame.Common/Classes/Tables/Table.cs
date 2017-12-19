using System;
using CardGame.Common.Classes.Cards;
using CardGame.Common.Classes.Players;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CardGame.Common.Enums;

namespace CardGame.Common.Classes.Tables
{
    public abstract class Table
    {
        public Guid TableGuid { get; set; }

        public string TableName { get; set; }
    }
}
