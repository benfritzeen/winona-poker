using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;

namespace api.Services
{
    public class HandEvaluator
    {
        private const string StraightFlush = "Straight Flush";
        private const string FourOfAKind = "Four of a Kind";
        private const string FullHouse = "Full House";
        private const string Flush = "Flush";
        private const string Straight = "Straight";
        private const string ThreeOfAKind = "Three of a Kind";
        private const string TwoPair = "Two Pair";
        private const string OnePair = "One Pair";
        private const string HighCard = "High Card";

        public static readonly List<string> HandStrength = new List<string>
        {
            StraightFlush, FourOfAKind, FullHouse, Flush, Straight, 
            ThreeOfAKind, TwoPair, OnePair, HighCard
        };

        public static string ClassifyHand(List<string> hand)
        {
            var rankCounts = CardUtilities.GetRankCounts(hand);
            var suitCounts = CardUtilities.GetSuitCounts(hand);
            var rankValues = CardUtilities.GetSortedRankValues(hand);

            bool isFlush = IsFlush(suitCounts);
            bool isStraight = IsStraight(rankValues);
            
            if (isFlush && isStraight) return StraightFlush;
            if (rankCounts.Values.Any(c => c == 4)) return FourOfAKind;
            if (rankCounts.Values.Contains(3) && rankCounts.Values.Contains(2)) return FullHouse;
            if (isFlush) return Flush;
            if (isStraight) return Straight;
            if (rankCounts.Values.Any(c => c == 3)) return ThreeOfAKind;
            if (rankCounts.Values.Count(c => c == 2) == 2) return TwoPair;
            if (rankCounts.Values.Any(c => c == 2)) return OnePair;
            return HighCard;
        }

        public static List<int> CalculateHandScore(List<string> hand, string handType)
        {
            var rankCounts = CardUtilities.GetRankCounts(hand);
            var rankValues = CardUtilities.GetSortedRankValues(hand);
            var score = new List<int>();

            switch (handType)
            {
                case StraightFlush:
                case Straight:
                    score.Add(rankValues.Max());
                    break;

                case FourOfAKind:
                    score.Add(CardUtilities.RankOrder[rankCounts.First(kv => kv.Value == 4).Key]);
                    break;

                case FullHouse:
                    score.Add(CardUtilities.RankOrder[rankCounts.First(kv => kv.Value == 3).Key]);
                    break;

                case Flush:
                case HighCard:
                    score.AddRange(rankValues);
                    break;

                case ThreeOfAKind:
                    score.Add(CardUtilities.RankOrder[rankCounts.First(kv => kv.Value == 3).Key]);
                    break;

                case TwoPair:
                    score.AddRange(rankCounts.Where(kv => kv.Value == 2)
                        .Select(kv => CardUtilities.RankOrder[kv.Key]).OrderByDescending(x => x).ToList());
                    score.Add(CardUtilities.RankOrder[rankCounts.First(kv => kv.Value == 1).Key]);
                    break;

                case OnePair:
                    score.Add(CardUtilities.RankOrder[rankCounts.First(kv => kv.Value == 2).Key]);
                    score.AddRange(rankCounts.Where(kv => kv.Value == 1)
                        .Select(kv => CardUtilities.RankOrder[kv.Key]).OrderByDescending(x => x));
                    break;
            }
            return score;
        }

        public static int CompareScores(List<int> a, List<int> b)
        {
            for (int i = 0; i < Math.Min(a.Count, b.Count); i++)
            {
                if (a[i] > b[i]) return 1;
                if (a[i] < b[i]) return -1;
            }
            return 0;
        }

        public static List<Player> FindTieBreakWinners(List<Player> tiedPlayers)
        {
            var bestPlayers = new List<Player>();
            List<int> bestScore = new List<int>();

            foreach (var player in tiedPlayers)
            {
                var score = CalculateHandScore(player.Hand, player.HandType);
                int comparison = bestPlayers.Count == 0 ? 1 : CompareScores(score, bestScore);
                
                if (comparison > 0)
                {
                    bestPlayers.Clear();
                    bestPlayers.Add(player);
                    bestScore = score;
                }
                else if (comparison == 0)
                {
                    bestPlayers.Add(player);
                }
            }

            return bestPlayers.Count > 0 ? bestPlayers : tiedPlayers;
        }

        private static bool IsStraight(List<int> rankValues) =>
            rankValues.Distinct().Count() == 5 && rankValues[0] - rankValues[4] == 4;

        private static bool IsFlush(Dictionary<string, int> suitCounts) =>
            suitCounts.Values.Any(c => c == 5);
    }
}
