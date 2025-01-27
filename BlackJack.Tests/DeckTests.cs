using BlackJack.Models;
using Xunit;

public class DeckTests
{
    [Fact] // Sprawdzenie, czy talia zawiera 52 karty
    public void Deck_Should_Contain_52_Cards_After_Initialization()
    {
        var deck = new Deck();
        Assert.Equal(52, deck.CardsRemaining());
    }

    [Fact] // Sprawdzenie, czy dobieranie karty zmniejsza ilość kart w talii
    public void DrawCard_Should_Decrease_Cards_Count()
    {
        var deck = new Deck();
        deck.DrawCard();
        Assert.Equal(51, deck.CardsRemaining());
    }
}
