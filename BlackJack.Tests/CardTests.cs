using BlackJack.Models;
using Xunit;

public class CardTests
{
    [Fact] // Test sprawdzający poprawność utworzenia karty
    public void Card_Creation_Should_Set_Properties()
    {
        var card = new Card("Hearts", "A", 11);

        Assert.Equal("Hearts", card.Suit);
        Assert.Equal("A", card.Rank);
        Assert.Equal(11, card.Value);
    }
}
