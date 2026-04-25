using System;
using System.IO;
using TicTacToe.UI.Core;

namespace TicTacToe.UI.Services
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath;
        private static readonly object _lock = new object();

        public FileLogger(string fileName = "game_history.log")
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        public void LogInfo(string message)
        {
            WriteToFile($"[INFO] [{DateTime.Now}] {message}");
        }

        public void LogError(string message, Exception? ex = null)
        {
            string errorText = $"[ERROR] [{DateTime.Now}] {message}";
            if (ex != null) errorText += $" | Exception: {ex.Message}";
            WriteToFile(errorText);
        }

        public void LogMove(string player, int row, int col)
        {
            WriteToFile($"[MOVE] [{DateTime.Now}] Гравця {player} у клітинку [{row}, {col}]");
        }

        private void WriteToFile(string text)
        {
            lock (_lock)
            {
                try
                {
                    using (StreamWriter writer = File.AppendText(_filePath))
                    {
                        writer.WriteLine(text);
                    }
                }
                catch { }
            }
        }
    }
}