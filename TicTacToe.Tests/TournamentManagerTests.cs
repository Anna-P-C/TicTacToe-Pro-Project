using Xunit;
using TicTacToe.UI.Services;

namespace TicTacToe.Tests
{
    public class TournamentManagerTests
    {
        [Fact]
        public void Tournament_FullCycle_CheckTotalScore()
        {
            var manager = new TournamentManager();

           
            manager.RegisterWin('X');
            Assert.Equal(100, manager.TotalScore);
            Assert.Equal(2, manager.CurrentRound);

           
            manager.RegisterWin('X');
            Assert.Equal(400, manager.TotalScore); 

          
            manager.RegisterWin('X');
            Assert.Equal(1400, manager.TotalScore); 
        }

        [Fact]
        public void Tournament_BotWins_ScoreDoesNotIncreaseForPlayer()
        {
            var manager = new TournamentManager();
            manager.RegisterWin('O'); 

            Assert.Equal(0, manager.TotalScore);
            Assert.Equal(1, manager.BotWins);
        }

        [Fact]
        public void Tournament_Reset_ShouldClearAllStats()
        {
            var manager = new TournamentManager();
            manager.RegisterWin('X');
            manager.ResetTournament();

            Assert.Equal(1, manager.CurrentRound);
            Assert.Equal(0, manager.TotalScore);
            Assert.Equal(0, manager.PlayerWins);
        }
    }
}