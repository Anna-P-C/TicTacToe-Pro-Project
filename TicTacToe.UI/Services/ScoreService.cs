using System.Text.Json;
using TicTacToe.UI.Models;

namespace TicTacToe.UI.Services
{
    public class ScoreService
    {
        private static ScoreService? _instance;
        private static readonly object _lock = new object();
        private readonly string _filePath = "scores.json";

        private ScoreService() { }

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

        public List<Player> GetTopPlayers()
        {
            if (!File.Exists(_filePath)) return new List<Player>();

            try
            {
                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Player>>(json) ?? new List<Player>();
            }
            catch
            {
                return new List<Player>();
            }
        }

        public void SaveScore(Player player)
        {
            var players = GetTopPlayers();
            var existingPlayer = players.FirstOrDefault(p => p.Name == player.Name);

            if (existingPlayer != null)
            {
                existingPlayer.Score += 1;
            }
            else
            {
                player.Score = 1;
                players.Add(player);
            }

            string json = JsonSerializer.Serialize(players.OrderByDescending(p => p.Score).ToList());
            File.WriteAllText(_filePath, json);
        }
    }
}