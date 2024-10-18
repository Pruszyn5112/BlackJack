namespace BlackJack
{
    public class BlackjackGame
    {
        private Deck _deck;
        private Player _player;
        private Player _dealer;

        public BlackjackGame()
        {
            ResetGame();
        }

        public void ResetGame()
        {
            _deck = new Deck();
            _player = new Player();
            _dealer = new Player();
        }

        public void DealerDrawCard()
        {
            _dealer.AddCard(_deck.DrawCard());
        }

        public void PlayerDrawCard()
        {
            _player.AddCard(_deck.DrawCard());
        }

        public bool IsPlayerBusted => _player.IsBusted;
        public bool IsDealerBusted => _dealer.IsBusted;
        public int PlayerScore => _player.Score;
        public int DealerScore => _dealer.Score;
        public bool HasPlayerBlackjack => _player.HasBlackjack;
        public bool HasDealerBlackjack => _dealer.HasBlackjack;

        public List<Card> GetDealerHand()
        {
            return _dealer.GetHand();
        }

        public List<Card> GetPlayerHand()
        {
            return _player.GetHand();
        }

        public int GetRemainingCards()
        {
            return _deck.CardsRemaining();
        }
    }
}
