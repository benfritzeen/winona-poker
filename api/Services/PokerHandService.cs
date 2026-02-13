using System;
using System.Collections.Generic;
using api.Models;

namespace api.Services
{
    public class PokerHandService
    {
        private readonly Random _random = new Random();
        private List<string> _deck;
        private List<Player> _players = new List<Player>();

        public PokerHandService()
        {
            _deck = new List<string>(Deck.Cards);
        }
        
        public List<string> DealHand(string playerName)
        {
            var hand = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                int index = _random.Next(_deck.Count);
                hand.Add(_deck[index]);
                _deck.RemoveAt(index);
            }
            _players.Add(new Player { Name = playerName, Hand = hand });
            return hand;
        }

        public List<Player> EvaluateHands()
        {
            var potentialWinners = FindPotentialWinners();
            AssignResults(potentialWinners);
            return _players;
        }

        private List<Player> FindPotentialWinners()
        {
            int bestRank = HandEvaluator.HandStrength.Count;
            var winners = new List<Player>();

            foreach (var player in _players)
            {
                player.HandType = HandEvaluator.ClassifyHand(player.Hand);
                player.GameResult = GameResult.Loss;
                int rank = HandEvaluator.HandStrength.IndexOf(player.HandType);

                if (rank != -1 && rank < bestRank)
                {
                    bestRank = rank;
                    winners.Clear();
                    winners.Add(player);
                }
                else if (rank == bestRank)
                {
                    winners.Add(player);
                }
            }
            return winners;
        }

        private void AssignResults(List<Player> potentialWinners)
        {
            if (potentialWinners.Count > 1)
            {
                var finalWinners = HandEvaluator.FindTieBreakWinners(potentialWinners);
                foreach (var player in finalWinners)
                {
                    player.GameResult = finalWinners.Count > 1 ? GameResult.Tie : GameResult.Win;
                }
            }
            else if (potentialWinners.Count == 1)
            {
                potentialWinners[0].GameResult = GameResult.Win;
            }
        }
    }
}
