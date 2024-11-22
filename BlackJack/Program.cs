using BlackJack.Controllers;
using BlackJack.Views;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            var initialBalance = 1000m; // Initial balance for the player
            var controller = new GameController(initialBalance);
            var view = new GameView(controller);
            view.DisplayMainMenu();
        }
    }
}
