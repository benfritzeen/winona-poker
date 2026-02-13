using api.Models;
using api.Services;

namespace api.tests.Services;

public class HandEvaluatorTests
{
    [Fact]
    public void ClassifyHand_StraightFlush_ReturnsCorrectType()
    {
        var hand = new List<string> { "AH", "KH", "QH", "JH", "10H" };
        
        var result = HandEvaluator.ClassifyHand(hand);
        
        Assert.Equal("Straight Flush", result);
    }

    [Fact]
    public void ClassifyHand_FourOfAKind_ReturnsCorrectType()
    {
        var hand = new List<string> { "AH", "AS", "AD", "AC", "KH" };
        
        var result = HandEvaluator.ClassifyHand(hand);
        
        Assert.Equal("Four of a Kind", result);
    }

    [Fact]
    public void ClassifyHand_FullHouse_ReturnsCorrectType()
    {
        var hand = new List<string> { "AH", "AS", "AD", "KC", "KH" };
        
        var result = HandEvaluator.ClassifyHand(hand);
        
        Assert.Equal("Full House", result);
    }

    [Fact]
    public void ClassifyHand_Flush_ReturnsCorrectType()
    {
        var hand = new List<string> { "AH", "KH", "QH", "JH", "9H" };
        
        var result = HandEvaluator.ClassifyHand(hand);
        
        Assert.Equal("Flush", result);
    }

    [Fact]
    public void ClassifyHand_Straight_ReturnsCorrectType()
    {
        var hand = new List<string> { "AH", "KS", "QD", "JC", "10H" };
        
        var result = HandEvaluator.ClassifyHand(hand);
        
        Assert.Equal("Straight", result);
    }

    [Fact]
    public void ClassifyHand_ThreeOfAKind_ReturnsCorrectType()
    {
        var hand = new List<string> { "AH", "AS", "AD", "KC", "QH" };
        
        var result = HandEvaluator.ClassifyHand(hand);
        
        Assert.Equal("Three of a Kind", result);
    }

    [Fact]
    public void ClassifyHand_TwoPair_ReturnsCorrectType()
    {
        var hand = new List<string> { "AH", "AS", "KD", "KC", "QH" };
        
        var result = HandEvaluator.ClassifyHand(hand);
        
        Assert.Equal("Two Pair", result);
    }

    [Fact]
    public void ClassifyHand_OnePair_ReturnsCorrectType()
    {
        var hand = new List<string> { "AH", "AS", "KD", "QC", "JH" };
        
        var result = HandEvaluator.ClassifyHand(hand);
        
        Assert.Equal("One Pair", result);
    }

    [Fact]
    public void ClassifyHand_HighCard_ReturnsCorrectType()
    {
        var hand = new List<string> { "AH", "KS", "QD", "JC", "9H" };
        
        var result = HandEvaluator.ClassifyHand(hand);
        
        Assert.Equal("High Card", result);
    }

    [Fact]
    public void CompareScores_FirstHigher_ReturnsPositive()
    {
        var scoreA = new List<int> { 14, 13, 12 };
        var scoreB = new List<int> { 14, 13, 11 };
        
        var result = HandEvaluator.CompareScores(scoreA, scoreB);
        
        Assert.Equal(1, result);
    }

    [Fact]
    public void CompareScores_SecondHigher_ReturnsNegative()
    {
        var scoreA = new List<int> { 14, 12, 11 };
        var scoreB = new List<int> { 14, 13, 11 };
        
        var result = HandEvaluator.CompareScores(scoreA, scoreB);
        
        Assert.Equal(-1, result);
    }

    [Fact]
    public void CompareScores_Equal_ReturnsZero()
    {
        var scoreA = new List<int> { 14, 13, 12 };
        var scoreB = new List<int> { 14, 13, 12 };
        
        var result = HandEvaluator.CompareScores(scoreA, scoreB);
        
        Assert.Equal(0, result);
    }

    [Fact]
    public void FindTieBreakWinners_SingleWinner_ReturnsOnePlayer()
    {
        var player1 = new Player { Name = "Player1", Hand = new List<string> { "AH", "KH", "QH", "JH", "9H" }, HandType = "Flush" };
        var player2 = new Player { Name = "Player2", Hand = new List<string> { "AH", "KH", "QH", "JH", "8H" }, HandType = "Flush" };
        var tiedPlayers = new List<Player> { player1, player2 };
        
        var result = HandEvaluator.FindTieBreakWinners(tiedPlayers);
        
        Assert.Single(result);
        Assert.Equal("Player1", result[0].Name);
    }

    [Fact]
    public void FindTieBreakWinners_TrueTie_ReturnsMultiplePlayers()
    {
        var player1 = new Player { Name = "Player1", Hand = new List<string> { "AH", "KH", "QH", "JH", "9H" }, HandType = "Flush" };
        var player2 = new Player { Name = "Player2", Hand = new List<string> { "AS", "KS", "QS", "JS", "9S" }, HandType = "Flush" };
        var tiedPlayers = new List<Player> { player1, player2 };
        
        var result = HandEvaluator.FindTieBreakWinners(tiedPlayers);
        
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void CalculateHandScore_FourOfAKind_ReturnsCorrectScore()
    {
        var hand = new List<string> { "AH", "AS", "AD", "AC", "KH" };
        
        var result = HandEvaluator.CalculateHandScore(hand, "Four of a Kind");
        
        Assert.Equal(14, result[0]); // Four aces
        Assert.Equal(13, result[1]); // King kicker
    }

    [Fact]
    public void CalculateHandScore_FullHouse_ReturnsCorrectScore()
    {
        var hand = new List<string> { "AH", "AS", "AD", "KC", "KH" };
        
        var result = HandEvaluator.CalculateHandScore(hand, "Full House");
        
        Assert.Equal(14, result[0]); // Three aces
        Assert.Equal(13, result[1]); // Pair of kings
    }

    [Fact]
    public void CalculateHandScore_TwoPair_ReturnsCorrectScore()
    {
        var hand = new List<string> { "AH", "AS", "KD", "KC", "QH" };
        
        var result = HandEvaluator.CalculateHandScore(hand, "Two Pair");
        
        Assert.Equal(14, result[0]); // Pair of aces
        Assert.Equal(13, result[1]); // Pair of kings
        Assert.Equal(12, result[2]); // Queen kicker
    }
}
