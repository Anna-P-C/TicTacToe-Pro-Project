using Xunit;
using TicTacToe.UI.Services;
using System.Linq;

using Assert = Xunit.Assert;

namespace TicTacToe.Tests
{
    public class AchievementLogicTests
    {
        [Fact]
        public void FastWin_Achievement_ShouldUnlock()
        {
            var manager = AchievementManager.Instance;
            manager.CheckFastWin(4);
            var unlocked = manager.GetUnlockedAchievements();

           
            Assert.Contains(unlocked, a => a.Id == "fast_win");
        }

        [Fact]
        public void CornerMaster_Achievement_ShouldUnlock()
        {
            var manager = AchievementManager.Instance;
            char[,] board = new char[3, 3];
            board[0, 0] = 'X'; board[0, 2] = 'X';
            board[2, 0] = 'X'; board[2, 2] = 'X';

            manager.CheckCorners(board, 'X');
            var unlocked = manager.GetUnlockedAchievements();
            Assert.Contains(unlocked, a => a.Id == "corner_master");
        }

        [Fact]
        public void Tournament_Strategist_Achievement_Check()
        {
            var manager = AchievementManager.Instance;
            manager.CheckTournamentStatus(3, 0);
            var unlocked = manager.GetUnlockedAchievements();
            Assert.Contains(unlocked, a => a.Id == "strategist");
        }

        [Fact]
        public void TotalGames_Persistent_Achievement_Check()
        {
            var manager = AchievementManager.Instance;
            manager.CheckTotalGames(10);
            var unlocked = manager.GetUnlockedAchievements();
            Assert.Contains(unlocked, a => a.Id == "persistent");
        }
    }
}