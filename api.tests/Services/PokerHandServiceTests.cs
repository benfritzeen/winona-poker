using api.Models;
using api.Services;

namespace api.tests.Services;

public class PokerHandServiceTests
{
    [Fact]
    public void DealHand_ReturnsFiveCards()
    {
        var service = new PokerHandService();
        
        var hand = service.DealHand("Player1");
        
        Assert.Equal(5, hand.Count);
    }

    [Fact]
    public void DealHand_ReturnsUniqueCards()
    {
        var service = new PokerHandService();
        
        var hand = service.DealHand("Player1");
        
        Assert.Equal(hand.Count, hand.Distinct().Count());
    }

    [Fact]
    public void DealHand_MultiplePlayersGetDifferentCards()
    {
        var service = new PokerHandService();
        
        var hand1 = service.DealHand("Player1");
        var hand2 = service.DealHand("Player2");
        
        var allCards = hand1.Concat(hand2).ToList();
        Assert.Equal(10, allCards.Distinct().Count());
    }

    [Fact]
    public void EvaluateHands_ReturnsAllPlayers()
    {
        var service = new PokerHandService();
        service.DealHand("Player1");
        service.DealHand("Player2");
        service.DealHand("Player3");
        
        var results = service.EvaluateHands();
        
        Assert.Equal(3, results.Count);
        Assert.Contains(results, p => p.Name == "Player1");
        Assert.Contains(results, p => p.Name == "Player2");
        Assert.Contains(results, p => p.Name == "Player3");
    }

    [Fact]
    public void EvaluateHands_ExactlyOneWinnerOrTie()
    {
        var service = new PokerHandService();
        service.DealHand("Player1");
        service.DealHand("Player2");
        
        var results = service.EvaluateHands();
        
        var winners = results.Count(p => p.GameResult == GameResult.Win);
        var ties = results.Count(p => p.GameResult == GameResult.Tie);
        
        Assert.True(winners == 1 || (winners == 0 && ties >= 2));
    }
}
