using BlackJack.Models;

namespace BlackJack.Controllers
{
    public class GameController
    {
        public Deck Deck { get; private set; }
        public Player Player { get; private set; }
        public Dealer Dealer { get; private set; }
        public decimal CurrentBet { get; private set; }

        public GameController(decimal initialBalance)
        {
            Deck = new Deck();
            Player = new Player("Player", initialBalance);
            Dealer = new Dealer();
        }

        public void StartGame(decimal betAmount)
        {
            Player.ClearHands();
            Dealer.ClearHand();
            CurrentBet = betAmount;
            Player.PlaceBet(betAmount);

            // Deal two cards to player and dealer
            Player.AddCard(Deck.DrawCard());
            Player.AddCard(Deck.DrawCard());
            Dealer.AddCard(Deck.DrawCard());
            Dealer.AddCard(Deck.DrawCard());
        }

        public void PlayerHit()
        {
            Player.AddCard(Deck.DrawCard());
        }

        public void DealerTurn()
        {
            while (Dealer.HandValue < 17)
            {
                Dealer.AddCard(Deck.DrawCard());
            }
        }

        public bool IsGameOver()
        {
            return Player.Hands.TrueForAll(hand => hand.HandValue > 21) || Dealer.HandValue > 21 || Player.Hands.Exists(hand => hand.HandValue == 21) || Dealer.HandValue == 21;
        }

        public bool IsBlackjack()
        {
            return Player.Hands.Exists(hand => hand.HandValue == 21);
        }

        public string GetWinner()
        {
            if (Player.Hands.TrueForAll(hand => hand.HandValue > 21)) return "Dealer wins!";
            if (Dealer.HandValue > 21) return "Player wins!";
            if (Player.Hands.Exists(hand => hand.HandValue > Dealer.HandValue)) return "Player wins!";
            if (Player.Hands.Exists(hand => hand.HandValue < Dealer.HandValue)) return "Dealer wins!";
            return "It's a tie!";
        }

        public void UpdateBalance()
        {
            foreach (var hand in Player.Hands)
            {
                if (hand.HandValue > 21)
                {
                    // Player loses, no balance update needed
                }
                else if (Dealer.HandValue > 21 || hand.HandValue > Dealer.HandValue)
                {
                    if (hand.HandValue == 21 && hand.GetCards().Count == 2)
                    {
                        Player.WinBet(CurrentBet * 2.5m); // 3:2 payout for Blackjack
                    }
                    else
                    {
                        Player.WinBet(CurrentBet * 2); // 2:1 payout for regular win
                    }
                }
                else if (hand.HandValue == Dealer.HandValue)
                {
                    Player.WinBet(CurrentBet); // Return the bet for a tie
                }
            }
        }

        public void SplitHand()
        {
            if (Player.CanSplit())
            {
                Player.SplitHand();
                Player.PlaceBet(CurrentBet); // Place an equal bet on the second hand
            }
        }
    }
}
