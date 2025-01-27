using BlackJack.Model;
using BlackJack.Models;
using Xunit;

public class GameTests
{
    [Fact] // Test inicjalizacji gry
    public void Game_Should_Initialize_Correctly()
    {
        var game = new Game(100m);
        Assert.NotNull(game.Deck);
        Assert.NotNull(game.Player);
        Assert.NotNull(game.Dealer);
    }

    [Fact] // Test rozdania początkowych kart
    public void StartGame_Should_Deal_Two_Cards_To_Player_And_Dealer()
    {
        var game = new Game(100m);
        game.StartGame(10m);

        Assert.Equal(2, game.Player.Hands[0].GetCards().Count);
        Assert.Equal(2, game.Dealer.Hand.GetCards().Count);
    }
}
