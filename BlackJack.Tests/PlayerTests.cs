using BlackJack.Models;
using Xunit;

public class PlayerTests
{
    [Fact] // Test początkowego balansu gracza
    public void Player_Should_Have_Initial_Balance()
    {
        var player = new Player("John", 100m);
        Assert.Equal(100m, player.Balance);
    }

    [Fact] // Test obstawiania zakładu
    public void PlaceBet_Should_Decrease_Balance()
    {
        var player = new Player("John", 100m);
        bool betPlaced = player.PlaceBet(20m);

        Assert.True(betPlaced);
        Assert.Equal(80m, player.Balance);
    }

    [Fact] // Test wygranej i zwiększenia balansu
    public void WinBet_Should_Increase_Balance()
    {
        var player = new Player("John", 100m);
        player.WinBet(40m);

        Assert.Equal(140m, player.Balance);
    }
}
