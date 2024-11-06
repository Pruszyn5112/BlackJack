namespace BlackJack
{
    public class Player
    {
        private List<Card> _hand;
        public int Balance { get; private set; }
        public int CurrentBet { get; private set; }

        public Player(int startingBalance = 100)
        {
            _hand = new List<Card>();
            Balance = startingBalance;
            CurrentBet = 0;
        }
        public void PlaceBet(int amount)
        {
            if (amount > Balance)
                throw new InvalidOperationException("Nie masz wystarczającej liczby żetonów.");

            CurrentBet = amount;
            Balance -= amount;
        }
        public void WinBet()
        {
            Balance += CurrentBet * 2;
            CurrentBet = 0;
        }

        public void TieBet()
        {
            Balance += CurrentBet;
            CurrentBet = 0;
        }
        public void BlackJackBet()
        {
            Balance += (CurrentBet * 3)/2;
        }
        public void LoseBet()
        {
            CurrentBet = 0;
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