namespace BlackJack
{
    public class Card
    {
        public string Suit { get; private set; }  // Kolor (np. serce, karo)
        public string Rank { get; private set; }  // Wartość karty (np. 2, 3, J, Q)
        public int Value { get; private set; }    // Punktacja karty

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
    }
}