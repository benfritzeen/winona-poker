using api.Services;

namespace api.tests.Services;

public class CardUtilitiesTests
{
    [Theory]
    [InlineData("AH", "A")]
    [InlineData("KS", "K")]
    [InlineData("10D", "10")]
    [InlineData("2C", "2")]
    [InlineData("JH", "J")]
    public void GetRank_ReturnsCorrectRank(string card, string expectedRank)
    {
        var result = CardUtilities.GetRank(card);
        Assert.Equal(expectedRank, result);
    }

    [Theory]
    [InlineData("AH", "H")]
    [InlineData("KS", "S")]
    [InlineData("10D", "D")]
    [InlineData("2C", "C")]
    public void GetSuit_ReturnsCorrectSuit(string card, string expectedSuit)
    {
        var result = CardUtilities.GetSuit(card);
        Assert.Equal(expectedSuit, result);
    }

    [Theory]
    [InlineData("AH", 14)]
    [InlineData("KS", 13)]
    [InlineData("QD", 12)]
    [InlineData("JC", 11)]
    [InlineData("10H", 10)]
    [InlineData("2S", 2)]
    public void GetRankValue_ReturnsCorrectValue(string card, int expectedValue)
    {
        var result = CardUtilities.GetRankValue(card);
        Assert.Equal(expectedValue, result);
    }

    [Fact]
    public void GetRankCounts_CountsRanksCorrectly()
    {
        var hand = new List<string> { "AH", "AS", "AD", "KC", "KH" };
        
        var result = CardUtilities.GetRankCounts(hand);
        
        Assert.Equal(3, result["A"]);
        Assert.Equal(2, result["K"]);
    }

    [Fact]
    public void GetSuitCounts_CountsSuitsCorrectly()
    {
        var hand = new List<string> { "AH", "KH", "QH", "JH", "10H" };
        
        var result = CardUtilities.GetSuitCounts(hand);
        
        Assert.Equal(5, result["H"]);
    }

    [Fact]
    public void GetSortedRankValues_ReturnsSortedDescending()
    {
        var hand = new List<string> { "2H", "AH", "KC", "5D", "10S" };
        
        var result = CardUtilities.GetSortedRankValues(hand);
        
        Assert.Equal(new List<int> { 14, 13, 10, 5, 2 }, result);
    }
}
