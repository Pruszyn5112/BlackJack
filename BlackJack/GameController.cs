namespace BlackJack
{
    public class GameController
    {
        private BlackjackGame _game;
        private ConsoleView _view;
        private Player _player;
        public GameController(BlackjackGame game, ConsoleView view)
        {
            _game = game;
            _view = view;
        }

        public void PlayGame()
        {
            bool playAgain = true;

            while (playAgain)
            {
                StartRound();

                _view.DisplayMessage("Do you want to play again? (y/n)");
                string choice = Console.ReadLine()?.ToLower();
                playAgain = (choice == "y");
                if (playAgain)
                {
                    _game.ResetGame();
                }
            }

            _view.DisplayMessage("Thank you for playing!");
        }

        private void StartRound()
        {
            try
            {
                int betAmount = _view.GetBetAmount();
                _game.Player.PlaceBet(betAmount);
            }
            catch (Exception ex)
            {
                _view.DisplayMessage(ex.Message);
                return;
            }

            _view.DisplayPlayerStatus(_game.Player.Balance, _game.Player.CurrentBet);

            _game.DealerDrawCard();
            _game.DealerDrawCard();
            _game.PlayerDrawCard();
            _game.PlayerDrawCard();

            bool playerTurn = true;

            while (playerTurn)
            {
                _view.ClearScreen();
                _view.DisplayDealerHand(_game.GetDealerHand(), false); // Nie odkrywa drugiej karty
                _view.DisplayPlayerHand(_game.GetPlayerHand());

                if (_game.HasPlayerBlackjack)
                {
                    _view.DisplayMessage("Blackjack! You win!");
                    _game.Player.BlackJackBet();
                    return;
                }

                if (_game.IsPlayerBusted)
                {
                    _view.DisplayMessage("You busted! Dealer wins.");
                    _game.Player.LoseBet();
                    return;
                }

                var choice = _view.GetPlayerInput();

                if (choice == "h")
                {
                    _game.PlayerDrawCard();
                }
                else if (choice == "s")
                {
                    playerTurn = false;
                }
            }

            DealerTurn();
        }

        private void DealerTurn()
        {
            while (_game.DealerScore < 17)
            {
                _game.DealerDrawCard();
                _view.AnimateCardDraw();
            }

            DetermineWinner();
        }

        private void DetermineWinner()
        {
            _view.ClearScreen();
            var dealerHand = _game.GetDealerHand(true); // Odkrywa wszystkie karty dealera
            _view.DisplayDealerHand(dealerHand, true); // Wyświetla rękę dealera, odkrywając drugą kartę
            _view.DisplayPlayerHand(_game.GetPlayerHand()); // Wyświetla rękę gracza

            // Sprawdzenie, kto wygrał
            if (_game.IsDealerBusted)
            {
                _view.DisplayMessage("Dealer busted! You win.");
                _game.Player.WinBet();
            }
            else if (_game.DealerScore > _game.PlayerScore)
            {
                _view.DisplayMessage("Dealer wins.");
                _game.Player.LoseBet();
            }
            else if (_game.DealerScore < _game.PlayerScore)
            {
                _view.DisplayMessage("You win!");
                _game.Player.WinBet();
            }
            else
            {
                _view.DisplayMessage("It's a tie!");
                _game.Player.TieBet();
            }
            _view.DisplayPlayerStatus(_game.Player.Balance, _game.Player.CurrentBet);
        }
    }
}
