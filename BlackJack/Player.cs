namespace BlackJack
{
    public class Player
    {
        private List<Card> _hand;

        public Player()
        {
            _hand = new List<Card>();
        }

        public void AddCard(Card card)
        {
            _hand.Add(card);
        }

        public int Score
        {
            get
            {
                int score = 0;
                int aceCount = 0;

                foreach (var card in _hand)
                {
                    score += card.Value;
                    if (card.Rank == "A")
                    {
                        aceCount++;
                    }
                }

                while (score > 21 && aceCount > 0)
                {
                    score -= 10;
                    aceCount--;
                }

                return score;
            }
        }

        public bool HasBlackjack
        {
            get
            {
                return _hand.Count == 2 && Score == 21;
            }
        }

        public bool IsBusted
        {
            get
            {
                return Score > 21;
            }
        }

        public List<Card> GetHand()
        {
            return _hand;
        }
    }


}