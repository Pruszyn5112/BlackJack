namespace BlackJack
{
    public class Player
    {
        public List<Card> Hand { get; private set; }
        public int Score { get; private set; }
        public bool HasBlackjack => Score == 21;
        public bool IsBusted => Score > 21;
        public Player()
        {
            Hand = new List<Card>();
            Score = 0;
        }
    }
}
