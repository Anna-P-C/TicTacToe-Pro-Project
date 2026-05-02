using Xunit;
using TicTacToe.UI.Services;
using TicTacToe.UI.Models; 
namespace TicTacToe.Tests
{
    public class GameLogicTests
    {
        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void InitializeBoard_CorrectSizeCreated(int size)
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(size);
            var board = engine.GetBoard();

            Assert.Equal(size, board.GetLength(0));
            Assert.Equal(size, board.GetLength(1));
        }

        [Fact]
        public void CheckWinner_5x5_DiagonalWin_ReturnsX()
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(5);

            
            for (int i = 0; i < 5; i++)
                engine.MakeMove(i, i, 'X');

            Assert.Equal('X', engine.CheckWinner());
        }

        [Fact]
        public void TournamentManager_ScoresUpdateCorrectly()
        {
            var manager = new TournamentManager();

          
            manager.RegisterWin('X');
            Assert.Equal(100, manager.TotalScore);

            
            Assert.Equal(2, manager.CurrentRound);
        }
    }
}