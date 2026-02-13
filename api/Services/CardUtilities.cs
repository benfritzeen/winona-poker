using System.Collections.Generic;
using System.Linq;

namespace api.Services
{
    public static class CardUtilities
    {
        public static readonly Dictionary<string, int> RankOrder = new Dictionary<string, int>
        {
            {"A", 14}, {"K", 13}, {"Q", 12}, {"J", 11},
            {"10", 10}, {"9", 9}, {"8", 8}, {"7", 7}, {"6", 6}, {"5", 5}, {"4", 4}, {"3", 3}, {"2", 2}
        };

        public static string GetRank(string card) => 
            card.Length == 3 ? card.Substring(0, 2) : card.Substring(0, 1);

        public static string GetSuit(string card) => 
            card.Substring(card.Length - 1, 1);

        public static int GetRankValue(string card) => 
            RankOrder[GetRank(card)];

        public static Dictionary<string, int> GetRankCounts(List<string> hand) =>
            hand.GroupBy(GetRank).ToDictionary(g => g.Key, g => g.Count());

        public static Dictionary<string, int> GetSuitCounts(List<string> hand) =>
            hand.GroupBy(GetSuit).ToDictionary(g => g.Key, g => g.Count());

        public static List<int> GetSortedRankValues(List<string> hand) =>
            hand.Select(GetRankValue).OrderByDescending(x => x).ToList();
    }
}
