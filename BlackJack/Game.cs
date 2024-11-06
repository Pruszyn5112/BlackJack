using System.Numerics;

namespace BlackJack
{
    public class BlackjackGame
    {
        private Deck _deck;
        private Player _player;
        public Player Player => _player;
        private Player _dealer;

        public BlackjackGame(Player player)
        {
            _player = player;
            _dealer = new Player();
            _deck = new Deck();
            _deck.ShuffleDeck();
        }

        /* public void ResetGame()
        {
            _deck = new Deck();
            _player = new Player();
            _dealer = new Player();
        } */
        public void ResetGame()
        {
            _player.GetHand().Clear();
            _dealer.GetHand().Clear();
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

        public List<Card> GetDealerHand(bool revealSecondCard = false)
        {
            if (revealSecondCard)
            {
                return _dealer.GetHand(); // Zwróć pełną rękę
            }
            else
            {
                var visibleHand = new List<Card> { _dealer.GetHand()[0] };
                visibleHand.Add(new Card("Unknown", "Hidden", 0)); // Dodaj kartę zakrytą
                return visibleHand;
            }
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
