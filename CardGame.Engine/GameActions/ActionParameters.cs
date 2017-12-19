using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Engine.GameActions
{
    public class ActionParameters
    {
        public int Count { get; set; }

        public int Cost { get; set; }

        public List<int> Ids { get; set; }
    }
}
