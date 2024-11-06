
namespace BlackJack
{
    public class MainMenu
    {
        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                DisplayOptions();

                string choice = Console.ReadLine()?.ToLower();

                switch (choice)
                {
                    case "1":
                        StartGame();
                        break;
                    case "2":
                        AddBalance();
                        return;
                 //   case "3":
                   //     Console.WriteLine("Wychodzenie z gry. Dziękujemy za grę!");
                    default:
                        Console.WriteLine("Nieprawidłowy wybór. Wybierz 1 lub 2.");
                        break;
                }
            }
        }

        private void DisplayOptions()
        {
            Console.WriteLine("Witamy w Blackjacku!");
            Console.WriteLine("1. Rozpocznij nową grę");
            Console.WriteLine("2. Dodaj balans");
            Console.WriteLine("3. Wyjdź");
            Console.Write("Wybierz opcję: ");
        }

        private void AddBalance()
        {

        }
        private void StartGame()
        {
            BlackjackGame game = new BlackjackGame();
            ConsoleView view = new ConsoleView();
            GameController controller = new GameController(game, view);
            controller.PlayGame();
        }
    }
}
