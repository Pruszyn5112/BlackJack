namespace BlackJack
{
    public class ConsoleView
    {
        public void ClearScreen()
        {
            Console.Clear();
        }
        public void DisplayPlayerStatus(int balance, int currentBet)
        {
            Console.WriteLine($"Twój balans: {balance} żetonów");
            Console.WriteLine($"Obecny zakład: {currentBet} żetonów");
            Console.WriteLine();
        }
        public void DisplayPlayerHand(List<Card> playerHand)
        {
            Console.WriteLine("Player's hand:");
            DrawCards(playerHand, true); 
        }
        public void DisplayDealerHand(List<Card> dealerHand, bool showSecondCard)
        {
            Console.WriteLine("Dealer's hand:");
            DrawCards(dealerHand, showSecondCard);
        }

        private void DrawCards(List<Card> cards, bool showSecondCard)
        {
            int cardOverlap = 5;
            for (int line = 0; line < 7; line++)
            {
                for (int i = 0; i < cards.Count; i++)
                {
                    if (i == 1 && !showSecondCard)
                    {
                        DrawHiddenCard(line);
                    }
                    else
                    {
                        DrawCardLine(cards[i], line);
                    }

                    if (i < cards.Count - 1)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - cardOverlap, Console.CursorTop);
                    }
                }
                Console.WriteLine();
            }
        }



        private void DrawHiddenCard(int line)
        {
            switch (line)
            {
                case 0: Console.Write("┌─────────┐"); break;
                case 1: Console.Write("│         │"); break;
                case 2: Console.Write("│         │"); break;
                case 3: Console.Write("│   ???   │"); break;
                case 4: Console.Write("│         │"); break;
                case 5: Console.Write("│         │"); break;
                case 6: Console.Write("└─────────┘"); break;
            }
        }

        private void DrawCardLine(Card card, int line)
        {
            string rank = card.Rank;
            string suit = GetSuitSymbol(card.Suit);

            switch (line)
            {
                case 0: Console.Write("┌─────────┐"); break;
                case 1: Console.Write($"│{rank,-2}       │"); break;
                case 2: Console.Write("│         │"); break;
                case 3: Console.Write($"│    {suit}    │"); break;
                case 4: Console.Write("│         │"); break;
                case 5: Console.Write($"│       {rank,2}│"); break;
                case 6: Console.Write("└─────────┘"); break;
            }
        }

        private string GetSuitSymbol(string suit)
        {
            switch (suit.ToLower())
            {
                case "hearts": return "♥";
                case "diamonds": return "♦";
                case "clubs": return "♣";
                case "spades": return "♠";
                default: return "?";
            }
        }

        public int GetBetAmount()
        {
            Console.Write("Wpisz kwotę zakładu: ");
            int bet;
            while (!int.TryParse(Console.ReadLine(), out bet) || bet <= 0)
            {
                Console.Write("Nieprawidłowa kwota. Wpisz poprawną kwotę zakładu: ");
            }
            return bet;
        }

        public void DisplayBalance(int balance)
        {
            Console.WriteLine($"Twój balans: {balance} żetonów");
        }

        public string GetPlayerInput()
        {
            Console.WriteLine("Do you want to (H)it or (S)tand?");
            return Console.ReadLine().ToLower();
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void AnimateCardDraw()
        {
            Console.WriteLine("Drawing card...");
            System.Threading.Thread.Sleep(500);
        }

        public void DisplayRemainingCards(int remainingCards)
        {
            Console.WriteLine($"\nRemaining cards in the deck: {remainingCards}");
        }
    }
}
