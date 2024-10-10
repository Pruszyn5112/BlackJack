namespace BlackJack
{
    public class BlackjackGame
    {
        private Deck _deck;
        private Player _player;
        private Player _dealer;

        public BlackjackGame()
        {
            _deck = new Deck();
            _player = new Player();
            _dealer = new Player();
        }

        public void PlayGame()
        {
            Console.Clear();
            Console.WriteLine("Welcome to BlackJack!");

            _player.AddCard(_deck.DrawCard());
            _player.AddCard(_deck.DrawCard());
            _dealer.AddCard(_deck.DrawCard());
            _dealer.AddCard(_deck.DrawCard());

            bool playerTurn = true;

            while (playerTurn)
            {
                Console.Clear();
                Console.WriteLine("Your hand:");

                if (_player.HasBlackjack)
                {
                    Console.WriteLine("Blackjack! You win!");
                    return;
                }

                if (_player.IsBusted)
                {
                    Console.WriteLine("You busted! Dealer wins.");
                    return;
                }

                Console.WriteLine("Do you want to (H)it or (S)tand?");
                var choice = Console.ReadLine().ToLower();
                if (choice == "h")
                {
                    _player.AddCard(_deck.DrawCard());
                }
                else if (choice == "s")
                {
                    playerTurn = false;
                }
            }


        }
    }
}
