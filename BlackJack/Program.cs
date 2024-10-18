namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            BlackjackGame game = new BlackjackGame();
            ConsoleView view = new ConsoleView();
            GameController controller = new GameController(game, view);

            controller.PlayGame();
        }
    }

}