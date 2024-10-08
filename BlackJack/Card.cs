namespace BlackJack
{
    public class Card
    {
        public string Suit { get; set; }
        public string Rank { get; set; }
        public int Value { get; set; }

        public Card(string suit, string rank, int value)
        {
            Suit = suit;
            Rank = rank;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }

        // Funkcja do zwrócenia symbolu koloru karty
        private string GetSuitSymbol(string suit)
        {
            switch (suit)
            {
                case "Hearts":
                    return "♥";
                case "Diamonds":
                    return "♦";
                case "Clubs":
                    return "♣";
                case "Spades":
                    return "♠";
                default:
                    return " ";
            }
        }
    }
}
