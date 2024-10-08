namespace BlackJack
{
    public class Deck
    {
        private List<Card> _cards;

        public Deck()
        {
            _cards = new List<Card>();
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            int[] values = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };

            foreach (var suit in suits)
            {
                for (int i = 0; i < ranks.Length; i++)
                {
                    _cards.Add(new Card(suit, ranks[i], values[i]));
                }
            }

            Shuffle();
        }

        public void Shuffle()
        {
            Random randomize = new Random();
            _cards = _cards.OrderBy(c => randomize.Next()).ToList();
        }
        public Card DrawCard()
        {
            if (_cards.Count == 0)
                throw new InvalidOperationException("No more cards in the deck.");

            var card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }

    }
}

