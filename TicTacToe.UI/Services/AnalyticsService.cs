using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace TicTacToe.UI.Services
{
    public class GameSessionMetrics
    {
        public string PlayerName { get; set; }
        public DateTime StartTime { get; set; }
        public int TotalMovesCount { get; set; }
        public int WinsCount { get; set; }
        public int LossesCount { get; set; }
        public double AverageMoveTimeSeconds { get; set; }
    }

    public class AnalyticsService
    {
        private static AnalyticsService _instance;
        private static readonly object _lock = new object();
        private readonly Stopwatch _roundTimer;
        private readonly List<long> _moveTimes;
        private readonly List<GameSessionMetrics> _globalHistory;
        private readonly string _logFilePath = "tournament_analytics.log";

        private AnalyticsService()
        {
            _roundTimer = new Stopwatch();
            _moveTimes = new List<long>();
            _globalHistory = new List<GameSessionMetrics>();
        }

        public static AnalyticsService Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null) _instance = new AnalyticsService();
                    return _instance;
                }
            }
        }


        public void StartRoundTimer()
        {
            _roundTimer.Restart();
            _moveTimes.Clear();
        }

        public void RegisterMove()
        {
            _moveTimes.Add(_roundTimer.ElapsedMilliseconds);
            _roundTimer.Restart();
        }

        
        public void SaveSession(string name, int wins, int losses, int moves)
        {
            var session = new GameSessionMetrics
            {
                PlayerName = name,
                StartTime = DateTime.Now,
                TotalMovesCount = moves,
                WinsCount = wins,
                LossesCount = losses,
                AverageMoveTimeSeconds = _moveTimes.Count > 0 ? _moveTimes.Average() / 1000.0 : 0
            };

            _globalHistory.Add(session);
            ExportSessionToLog(session);
        }


      
        public string GetPerformanceAdvice(int wins, int losses)
        {
            int total = wins + losses;
            double eff = total == 0 ? 0 : (double)wins / total * 100;

            if (eff >= 80) return "Ти граєш як профі!";
            if (eff >= 50) return "Непогано, але можна краще.";
            return "Тобі треба більше практики.";
        }

        private void ExportSessionToLog(GameSessionMetrics data)
        {
            try
            {
                string report = $"[{data.StartTime}] Гравець: {data.PlayerName}, Перемог: {data.WinsCount}, Ходів: {data.TotalMovesCount}\n";
                File.AppendAllText(_logFilePath, report);
            }
            catch { }
        }
    }
}