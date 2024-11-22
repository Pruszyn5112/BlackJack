using System;
using BlackJack.Controllers;
using BlackJack.Models;

namespace BlackJack.Views
{
    public class GameView
    {
        private GameController _controller;
        private bool _isDealerTurn;

        public GameView(GameController controller)
        {
            _controller = controller;
            _isDealerTurn = false;
        }

        public void DisplayMainMenu()
        {
            while (true)
            {
                Console.WriteLine("Welcome to BlackJack!");
                Console.WriteLine("1. Start Game");
                Console.WriteLine("2. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    DisplayGame();
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Thank you for playing!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please choose (1) Start Game or (2) Exit.");
                }
            }
        }

        public void DisplayGame()
        {
            Console.WriteLine($"Your current balance is: {_controller.Player.Balance:C}");
            Console.Write("Enter your bet amount: ");
            var betAmount = decimal.Parse(Console.ReadLine());

            _controller.StartGame(betAmount);

            if (_controller.IsBlackjack())
            {
                _isDealerTurn = true;
                DisplayHands();
                Console.WriteLine(_controller.GetWinner());
                _controller.UpdateBalance();
                Console.WriteLine($"Your new balance is: {_controller.Player.Balance:C}");
                return;
            }

            while (true)
            {
                DisplayHands();

                if (_controller.IsGameOver())
                {
                    break;
                }

                if (_controller.Player.CanSplit())
                {
                    Console.WriteLine("Choose an option: (1) Hit (2) Stay (3) Split (4) Double Down");
                }
                else if (_controller.Player.CanDoubleDown())
                {
                    Console.WriteLine("Choose an option: (1) Hit (2) Stay (4) Double Down");
                }
                else
                {
                    Console.WriteLine("Choose an option: (1) Hit (2) Stay");
                }

                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    _controller.PlayerHit();
                    if (_controller.Player.IsCurrentHandFinished())
                    {
                        if (!_controller.Player.MoveToNextHand())
                        {
                            _isDealerTurn = true;
                            _controller.DealerTurn();
                            break;
                        }
                    }
                }
                else if (choice == "2")
                {
                    if (!_controller.Player.MoveToNextHand())
                    {
                        _isDealerTurn = true;
                        _controller.DealerTurn();
                        break;
                    }
                }
                else if (choice == "3" && _controller.Player.CanSplit())
                {
                    _controller.SplitHand();
                }
                else if (choice == "4" && _controller.Player.CanDoubleDown())
                {
                    _controller.DoubleDown();
                    _isDealerTurn = true;
                    _controller.DealerTurn();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please choose (1) Hit, (2) Stay, (3) Split, or (4) Double Down.");
                }
            }

            DisplayHands();
            Console.WriteLine(_controller.GetWinner());
            _controller.UpdateBalance();
            Console.WriteLine($"Your new balance is: {_controller.Player.Balance:C}");
        }

        private void DisplayHands()
        {
            Console.WriteLine("\nPlayer's hands:");
            for (int i = 0; i < _controller.Player.Hands.Count; i++)
            {
                Console.WriteLine($"Hand {i + 1}:");
                DisplayHand(_controller.Player.Hands[i]);
                Console.WriteLine($"Hand {i + 1} value: {_controller.Player.Hands[i].HandValue}");
            }

            Console.WriteLine("\nDealer's hand:");
            DisplayDealerHand();

            if (_isDealerTurn)
            {
                Console.WriteLine($"Dealer's hand value: {_controller.Dealer.HandValue}\n");
            }
            else
            {
                Console.WriteLine($"Dealer's visible card value: {_controller.Dealer.Hand.GetCards()[0].Value}\n");
            }
        }

        private void DisplayHand(Hand hand)
        {
            foreach (var card in hand.GetCards())
            {
                Console.WriteLine($"{card.Rank} of {card.Suit}");
            }
        }

        private void DisplayDealerHand()
        {
            var cards = _controller.Dealer.Hand.GetCards();
            Console.WriteLine($"{cards[0].Rank} of {cards[0].Suit}");
            if (_isDealerTurn)
            {
                for (int i = 1; i < cards.Count; i++)
                {
                    Console.WriteLine($"{cards[i].Rank} of {cards[i].Suit}");
                }
            }
            else
            {
                Console.WriteLine("Hidden Card");
            }
        }
    }
}
