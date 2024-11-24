﻿using System;
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
                    break;
                }
            }
        }

        public void DisplayGame()
        {
            AnsiConsole.Clear();
            AnsiConsole.Markup($"[bold yellow]Your current balance is: {_controller.Player.Balance:C}[/]\n");
            var betAmount = AnsiConsole.Ask<decimal>("Enter your bet amount:");

            _controller.StartGame(betAmount);

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
            AnsiConsole.Markup($"[bold green]{_controller.GetWinner()}[/]\n");
            AnsiConsole.Markup($"[bold yellow]Your final balance is: {_controller.Player.Balance:C}[/]\n");

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
            foreach (var card in hand.GetCards())
            {
                AnsiConsole.Markup($"[bold yellow]{card.Rank} of {card.Suit}[/]\n");
            }
        }

        private void DisplayDealerHand()
        {
            var cards = _controller.Dealer.Hand.GetCards();
            AnsiConsole.Markup($"[bold yellow]{cards[0].Rank} of {cards[0].Suit}[/]\n");
            if (_isDealerTurn)
            {
                for (int i = 1; i < cards.Count; i++)
                {
                    AnsiConsole.Markup($"[bold yellow]{cards[i].Rank} of {cards[i].Suit}[/]\n");
                }
            }
            else
            {
                AnsiConsole.Markup("[bold yellow]Hidden Card[/]\n");
            }
        }
    }
}
