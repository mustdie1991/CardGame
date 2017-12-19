using CardGame.Common.Enums;

namespace CardGame.Common.Classes.Cards
{
    public abstract class Card
    {
        public string Name { get; set; }

        public int Cost { get; set; }

        public CardNames CardName { get; set; }

        protected const int PROVINCE_VICTORY_POINTS = 6;

        protected const int DUCHY_VICTORY_POINTS = 3;

        protected const int ESTATE_VICTORY_POINTS = 1;

        public override string ToString()
        {
            return this.Name;
        }
    }
}
