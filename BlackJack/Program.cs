using System;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Bez tego nie wyświetlają się kolory kart (znaczki)

            MainMenu menu = new MainMenu();
            menu.ShowMenu();
        }
    }
}
