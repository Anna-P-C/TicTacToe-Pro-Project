using Xunit;
using TicTacToe.UI.Services;

using Assert = Xunit.Assert;

namespace TicTacToe.Tests
{
    public class WinConditionTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void CheckWinner_5x5_HorizontalWin_ReturnsX(int row)
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(5);
            for (int col = 0; col < 5; col++) engine.MakeMove(row, col, 'X');
            Assert.Equal('X', engine.CheckWinner());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void CheckWinner_5x5_VerticalWin_ReturnsO(int col)
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(5);
            for (int row = 0; row < 5; row++) engine.MakeMove(row, col, 'O');
            Assert.Equal('O', engine.CheckWinner());
        }

        [Fact]
        public void CheckWinner_5x5_MainDiagonalWin_ReturnsX()
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(5);
            for (int i = 0; i < 5; i++) engine.MakeMove(i, i, 'X');
            Assert.Equal('X', engine.CheckWinner());
        }

        [Fact]
        public void CheckWinner_5x5_ReverseDiagonalWin_ReturnsO()
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(5);
            for (int i = 0; i < 5; i++) engine.MakeMove(i, 4 - i, 'O');
            Assert.Equal('O', engine.CheckWinner());
        }

        [Fact]
        public void CheckWinner_3x3_DiagonalWin_ReturnsX()
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(3);
            engine.MakeMove(0, 0, 'X');
            engine.MakeMove(1, 1, 'X');
            engine.MakeMove(2, 2, 'X');
            Assert.Equal('X', engine.CheckWinner());
        }

        [Fact]
        public void GameEngine_MovesCount_ShouldIncrease_Detailed()
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(3);

          
            Assert.NotNull(engine.GetBoard());

          
            engine.MakeMove(0, 0, 'X');
            engine.MakeMove(1, 1, 'O');

           
            var board = engine.GetBoard();
            Assert.Equal('X', board[0, 0]);
            Assert.Equal('O', board[1, 1]);
            Assert.Equal('\0', board[2, 2]);
        }
    }
}