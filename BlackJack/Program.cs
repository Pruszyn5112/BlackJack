using BlackJack.Controllers;
using BlackJack.Views;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            var initialBalance = 1000;
            var controller = new GameController(initialBalance);
            var view = new GameView(controller);
            view.DisplayMainMenu();
        }
    }
}
