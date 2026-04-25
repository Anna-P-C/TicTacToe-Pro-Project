using System.Text.Json;
using TicTacToe.UI.Models;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TicTacToe.UI.Services
{
    
    public class ScoreService
    {
        private static ScoreService? _instance;
        private static readonly object _lock = new object();
        private readonly string _filePath = "scores.json";

        private ScoreService()
        {
          
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public static ScoreService Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new ScoreService();
                    return _instance;
                }
            }
        }

       
        public List<Player> GetPlayers()
        {
            if (!File.Exists(_filePath)) return new List<Player>();

            try
            {
                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Player>>(json) ?? new List<Player>();
            }
            catch (Exception)
            {
                
                return new List<Player>();
            }
        }

        
        public List<Player> GetTopPlayers(int count = 5)
        {
            return GetPlayers()
                .OrderByDescending(p => p.Score)
                .Take(count)
                .ToList();
        }

       
        public void SaveScore(Player player, int pointsEarned)
        {
            var players = GetPlayers();
            var existingPlayer = players.FirstOrDefault(p => p.Name == player.Name);

            if (existingPlayer != null)
            {
                
                existingPlayer.Score += pointsEarned;
                existingPlayer.LastPlayed = DateTime.Now; 
            }
            else
            {
                player.Score = pointsEarned;
                player.LastPlayed = DateTime.Now;
                players.Add(player);
            }

            SaveToFile(players);
        }

        public void ClearAllScores()
        {
            SaveToFile(new List<Player>());
        }

        private void SaveToFile(List<Player> players)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(players.OrderByDescending(p => p.Score).ToList(), options);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception)
            {
               
            }
        }
    }
}