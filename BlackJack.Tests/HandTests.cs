using BlackJack.Models;
using Xunit;

public class HandTests
{
    [Fact] // Test, czy wartość ręki jest poprawnie liczona
    public void AddCard_Should_Increase_HandValue()
    {
        var hand = new Hand();
        hand.AddCard(new Card("Hearts", "10", 10));
        hand.AddCard(new Card("Diamonds", "A", 11));

        Assert.Equal(21, hand.HandValue); // A + 10 = 21
    }

    [Fact] // Test, czy As zmienia wartość na 1, gdy przekracza 21
    public void HandValue_Should_Adjust_Ace_Value()
    {
        var hand = new Hand();
        hand.AddCard(new Card("Hearts", "A", 11));
        hand.AddCard(new Card("Diamonds", "A", 11));
        hand.AddCard(new Card("Spades", "9", 9));

        Assert.Equal(21, hand.HandValue); // A (11) + A (1) + 9 = 21
    }

    [Fact] // Test czyszczenia ręki
    public void ClearHand_Should_Reset_Cards()
    {
        var hand = new Hand();
        hand.AddCard(new Card("Hearts", "5", 5));
        hand.ClearHand();

        Assert.Empty(hand.GetCards());
    }
}
