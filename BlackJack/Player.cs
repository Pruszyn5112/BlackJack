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
        public void AddCard(Card card)
        {
            Hand.Add(card);
            Score += card.Value;

            // Obsługa przypadku, gdy as przekracza 21 punktów
            if (Score > 21)
            {
                foreach (var handCard in Hand)
                {
                    if (handCard.Rank == "A" && Score > 21)
                    {
                        Score -= 10;
                    }
                }
            }
        }
    }

}
