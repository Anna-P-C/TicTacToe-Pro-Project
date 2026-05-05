using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TicTacToe.UI.Models;

namespace TicTacToe.UI.Services
{
    public class Achievement
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsUnlocked { get; set; }
        public DateTime UnlockedAt { get; set; }
    }

    public class AchievementManager
    {
        private static AchievementManager _instance;
        private static readonly object _lock = new object();
        private readonly string _filePath = "achievements.json";
        private List<Achievement> _achievements;

        private AchievementManager()
        {
            _achievements = new List<Achievement>();
            InitializeDefaultAchievements();
            LoadAchievements();
        }

        public static AchievementManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null) _instance = new AchievementManager();
                    return _instance;
                }
            }
        }

        private void InitializeDefaultAchievements()
        {
            _achievements.Add(new Achievement
            {
                Id = "fast_win",
                Title = "Блискавка",
                Description = "Перемогти менш ніж за 5 ходів"
            });

            _achievements.Add(new Achievement
            {
                Id = "corner_master",
                Title = "Майстер кутів",
                Description = "Поставити символи у всі 4 кути поля"
            });

            _achievements.Add(new Achievement
            {
                Id = "streak_3",
                Title = "Потрійний удар",
                Description = "Виграти 3 раунди поспіль"
            });

            _achievements.Add(new Achievement
            {
                Id = "first_blood",
                Title = "Перша кров",
                Description = "Перемогти у самому першому раунді"
            });

            _achievements.Add(new Achievement
            {
                Id = "strategist",
                Title = "Стратег",
                Description = "Виграти турнір без жодної поразки"
            });

            _achievements.Add(new Achievement
            {
                Id = "persistent",
                Title = "Наполегливий",
                Description = "Зіграти загалом 10 матчів"
            });
        }

        public void CheckFastWin(int movesCount)
        {
            if (movesCount <= 5)
            {
                UnlockAchievement("fast_win");
            }
        }

        public void CheckCorners(char[,] board, char playerSymbol)
        {
            bool topLeft = board[0, 0] == playerSymbol;
            bool topRight = board[0, 2] == playerSymbol;
            bool bottomLeft = board[2, 0] == playerSymbol;
            bool bottomRight = board[2, 2] == playerSymbol;

            if (topLeft && topRight && bottomLeft && bottomRight)
            {
                UnlockAchievement("corner_master");
            }
        }

        public void CheckWinStreak(int currentStreak)
        {
            if (currentStreak >= 3)
            {
                UnlockAchievement("streak_3");
            }
        }

        public void CheckTournamentStatus(int totalWins, int totalLosses)
        {
            if (totalLosses == 0 && totalWins >= 3)
            {
                UnlockAchievement("strategist");
            }
        }

        public void CheckTotalGames(int totalGames)
        {
            if (totalGames >= 10)
            {
                UnlockAchievement("persistent");
            }
        }

        private void UnlockAchievement(string id)
        {
            var achievement = _achievements.FirstOrDefault(a => a.Id == id);
            if (achievement != null && !achievement.IsUnlocked)
            {
                achievement.IsUnlocked = true;
                achievement.UnlockedAt = DateTime.Now;
                SaveAchievements();
                NotifyUser(achievement.Title);
            }
        }

        private void NotifyUser(string title)
        {
            System.Windows.Forms.MessageBox.Show(
                $"Нове досягнення розблоковано: {title}!",
                "Досягнення",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Information);
        }

        private void LoadAchievements()
        {
            if (!File.Exists(_filePath)) return;

            try
            {
                string json = File.ReadAllText(_filePath);
                var loaded = JsonSerializer.Deserialize<List<Achievement>>(json);
                if (loaded != null)
                {
                    foreach (var item in loaded)
                    {
                        var target = _achievements.FirstOrDefault(a => a.Id == item.Id);
                        if (target != null)
                        {
                            target.IsUnlocked = item.IsUnlocked;
                            target.UnlockedAt = item.UnlockedAt;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Помилка завантаження
            }
        }

        private void SaveAchievements()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(_achievements, options);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception)
            {
                // Помилка збереження
            }
        }

        public List<Achievement> GetUnlockedAchievements()
        {
            return _achievements.Where(a => a.IsUnlocked).ToList();
        }

        public int GetTotalProgress()
        {
            if (_achievements.Count == 0) return 0;
            return (_achievements.Count(a => a.IsUnlocked) * 100) / _achievements.Count;
        }
    }
}