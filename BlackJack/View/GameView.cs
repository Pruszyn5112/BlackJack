using System;
using System.Collections.Generic;
using BlackJack.Controllers;
using BlackJack.Models;
using Spectre.Console;

namespace BlackJack.Views
{
    public class GameView
    {
        private readonly GameController _controller;
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
                AnsiConsole.Clear();
                AnsiConsole.Markup("[bold yellow]Welcome to BlackJack![/]\n");
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Choose an option:")
                        .AddChoices("Start Game", "Exit"));

                if (choice == "Start Game")
                {
                    DisplayGame();
                }
                else if (choice == "Exit")
                {
                    AnsiConsole.Markup("[bold green]Thank you for playing![/]");
                    Environment.Exit(0);
                }
            }
        }

        public void DisplayGame()
        {
            AnsiConsole.Clear();
            _isDealerTurn = false; // Reset the dealer turn flag
            AnsiConsole.Markup($"[bold yellow]Your current balance is: {_controller.Player.Balance:C}[/]\n");

            decimal betAmount;
            do
            {
                betAmount = AnsiConsole.Ask<decimal>("Enter your bet amount:");
                if (betAmount <= 0)
                {
                    AnsiConsole.Markup("[red]Bet amount must be greater than zero. Please enter a valid amount.[/]\n");
                }
            } while (betAmount <= 0);

            _controller.StartGame(betAmount);

            // Check for insurance bet option
            if (_controller.Dealer.Hand.GetCards()[0].Value == 11)
            {
                DisplayInsuranceBetOption();
                var insuranceChoice = AnsiConsole.Ask<string>("Enter your choice (yes/no):").ToLower();
                if (insuranceChoice == "yes")
                {
                    _controller.PlaceInsuranceBet();
                }
            }

            if (_controller.IsBlackjack())
            {
                _isDealerTurn = true;
                DisplayHands();
                AnsiConsole.Markup($"[bold green]{_controller.GetWinner()}[/]\n");
                _controller.UpdateBalance();
                AnsiConsole.Markup($"[bold yellow]Your new balance is: {_controller.Player.Balance:C}[/]\n");
                DisplayGameSummary();
                return;
            }

            while (true)
            {
                DisplayHands();

                if (_controller.IsGameOver())
                {
                    break;
                }

                var options = new List<string> { "Hit", "Stay" };
                if (_controller.Player.CanSplit())
                {
                    options.Add("Split");
                }
                if (_controller.Player.CanDoubleDown())
                {
                    options.Add("Double Down");
                }

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Choose an option:")
                        .AddChoices(options));

                if (choice == "Hit")
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
                else if (choice == "Stay")
                {
                    if (!_controller.Player.MoveToNextHand())
                    {
                        _isDealerTurn = true;
                        _controller.DealerTurn();
                        break;
                    }
                }
                else if (choice == "Split" && _controller.Player.CanSplit())
                {
                    _controller.SplitHand();
                }
                else if (choice == "Double Down" && _controller.Player.CanDoubleDown())
                {
                    _controller.DoubleDown();
                    _isDealerTurn = true;
                    _controller.DealerTurn();
                    break;
                }
            }

            DisplayHands();
            AnsiConsole.Markup($"[bold green]{_controller.GetWinner()}[/]\n");
            _controller.UpdateBalance();
            AnsiConsole.Markup($"[bold yellow]Your new balance is: {_controller.Player.Balance:C}[/]\n");
            DisplayGameSummary();
        }

        public void DisplayInsuranceBetOption()
        {
            AnsiConsole.Markup("[yellow]Dealer's face-up card is an Ace. Do you want to place an insurance bet? (yes/no)[/]");
        }

        public void DisplayInsuranceBetResult(bool won)
        {
            if (won)
            {
                AnsiConsole.Markup("[green]You won the insurance bet![/]");
            }
            else
            {
                AnsiConsole.Markup("[red]You lost the insurance bet.[/]");
            }
        }

        public void DisplayMessage(string message)
        {
            AnsiConsole.Markup($"[blue]{message}[/]");
        }

        public string GetUserInput()
        {
            return AnsiConsole.Ask<string>("[yellow]Enter your choice:[/]");
        }

        public void DisplayGameSummary()
        {
          //  AnsiConsole.Markup($"[bold green]{_controller.GetWinner()}[/]\n");
          // AnsiConsole.Markup($"[bold yellow]Your final balance is: {_controller.Player.Balance:C}[/]\n");

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to do next?")
                    .AddChoices("Return to Main Menu", "Exit"));

            if (choice == "Return to Main Menu")
            {
                DisplayMainMenu();
            }
            else if (choice == "Exit")
            {
                AnsiConsole.Markup("[bold green]Thank you for playing![/]");
            }
        }

        private void DisplayHands()
        {
            AnsiConsole.Clear();
            AnsiConsole.Markup("[bold yellow]Player's hands:[/]\n");
            for (int i = 0; i < _controller.Player.Hands.Count; i++)
            {
                AnsiConsole.Markup($"[bold yellow]Hand {i + 1}:[/]\n");
                DisplayHand(_controller.Player.Hands[i]);
                AnsiConsole.Markup($"[bold yellow]Hand {i + 1} value: {_controller.Player.Hands[i].HandValue}[/]\n");
            }

            AnsiConsole.Markup("[bold yellow]Dealer's hand:[/]\n");
            DisplayDealerHand();

            if (_isDealerTurn)
            {
                AnsiConsole.Markup($"[bold yellow]Dealer's hand value: {_controller.Dealer.HandValue}[/]\n");
            }
            else
            {
                AnsiConsole.Markup($"[bold yellow]Dealer's visible card value: {_controller.Dealer.Hand.GetCards()[0].Value}[/]\n");
            }
        }

        private void DisplayHand(Hand hand)
        {
            var cards = hand.GetCards();
            var cardLines = new List<string[]>();

            foreach (var card in cards)
            {
                cardLines.Add(GetCardAsciiArtLines(card));
            }

            for (int i = 0; i < 9; i++)
            {
                foreach (var cardLine in cardLines)
                {
                    AnsiConsole.Markup(cardLine[i]);
                }
                AnsiConsole.WriteLine();
            }
        }

        private void DisplayDealerHand()
        {
            var cards = _controller.Dealer.Hand.GetCards();
            var cardLines = new List<string[]>();

            cardLines.Add(GetCardAsciiArtLines(cards[0]));
            if (_isDealerTurn)
            {
                for (int i = 1; i < cards.Count; i++)
                {
                    cardLines.Add(GetCardAsciiArtLines(cards[i]));
                }
            }
            else
            {
                cardLines.Add(GetCardAsciiArtLines(null)); // Hidden card
            }

            for (int i = 0; i < 9; i++)
            {
                foreach (var cardLine in cardLines)
                {
                    AnsiConsole.Markup(cardLine[i]);
                }
                AnsiConsole.WriteLine();
            }
        }

        private string[] GetCardAsciiArtLines(Card card)
        {
            if (card == null)
            {
                return new string[]
                {
                    "┌─────────┐",
                    "│░░░░░░░░░│",
                    "│░░░░░░░░░│",
                    "│░░░░░░░░░│",
                    "│░░░░░░░░░│",
                    "│░░░░░░░░░│",
                    "│░░░░░░░░░│",
                    "│░░░░░░░░░│",
                    "└─────────┘"
                };
            }

            string rank = card.Rank.ToString();
            string suit = card.Suit.ToString().Substring(0, 1);

            return new string[]
            {
                "┌─────────┐",
                $"│{rank,-2}       │",
                "│         │",
                "│         │",
                $"│    {suit}    │",
                "│         │",
                "│         │",
                $"│       {rank,2}│",
                "└─────────┘"
            };
        }
    }
}
