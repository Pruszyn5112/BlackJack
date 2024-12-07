using BlackJack.Models;
using BlackJack.Views;
using BlackJack.Model;

namespace BlackJack.Controllers
{
    public class GameController
    {
        private readonly Game _game;
        private readonly GameView _gameView;

        public GameController(decimal initialBalance)
        {
            _game = new Game(initialBalance);
            _gameView = new GameView(this); // Initialize GameView with the controller
        }

        public Deck Deck => _game.Deck;
        public Player Player => _game.Player;
        public Dealer Dealer => _game.Dealer;
        public decimal CurrentBet => _game.CurrentBet;

        public void StartGame(decimal betAmount)
        {
            _game.StartGame(betAmount);
        }

        public void PlayerHit()
        {
            _game.PlayerHit();
        }

        public void DealerTurn()
        {
            _game.DealerTurn();
        }

        public bool IsGameOver()
        {
            return _game.IsGameOver();
        }

        public bool IsBlackjack()
        {
            return _game.IsBlackjack();
        }

        public string GetWinner()
        {
            return _game.GetWinner();
        }

        public void UpdateBalance()
        {
            _game.UpdateBalance();
        }

        public void SplitHand()
        {
            _game.SplitHand();
        }

        public void DoubleDown()
        {
            _game.DoubleDown();
        }

        public void PlaceInsuranceBet()
        {
            _game.PlaceInsuranceBet();
        }

        public void ResolveGame()
        {
            _game.ResolveGame();
        }

        public void CheckInsurance()
        {
            _game.CheckInsurance();
        }
    }
}
