using Xunit;
using TicTacToe.UI.Services;
using TicTacToe.UI.Core;

namespace TicTacToe.Tests
{
    public class GameLogicTests
    {
        private readonly GameEngine _engine;

        public GameLogicTests()
        {
            _engine = new GameEngine();
        }

        [Fact]
        public void Board_ShouldBeEmpty_OnStart()
        {
            var board = _engine.GetBoard();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Assert.Equal('\0', board[i, j]);
                }
            }
        }

        [Fact]
        public void MakeMove_ShouldSetSymbol_WhenCellIsEmpty()
        {
            bool success = _engine.MakeMove(0, 0, 'X');
            var board = _engine.GetBoard();

            Assert.True(success);
            Assert.Equal('X', board[0, 0]);
        }

        [Fact]
        public void MakeMove_ShouldReturnFalse_WhenCellIsOccupied()
        {
            _engine.MakeMove(1, 1, 'X');
            bool success = _engine.MakeMove(1, 1, 'O');

            Assert.False(success);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void CheckWinner_ShouldDetectHorizontalWin(int row)
        {
            _engine.ResetBoard();
            _engine.MakeMove(row, 0, 'X');
            _engine.MakeMove(row, 1, 'X');
            _engine.MakeMove(row, 2, 'X');

            Assert.Equal('X', _engine.CheckWinner());
        }

        [Fact]
        public void TournamentManager_ShouldProgress_AfterWin()
        {
            var manager = new TournamentManager();
            int initialRound = manager.CurrentRound;

            manager.RegisterWin('X');

            Assert.Equal(initialRound + 1, manager.CurrentRound);
            Assert.Equal(100, manager.TotalScore);
        }

        [Fact]
        public void Tournament_ShouldEnd_WhenBotWins()
        {
            var manager = new TournamentManager();
            manager.RegisterWin('O');

            Assert.False(manager.IsTournamentActive);
        }
    }
}