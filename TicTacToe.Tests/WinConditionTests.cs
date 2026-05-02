using Xunit;
using TicTacToe.UI.Services;

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

        // Перевірка вертикалей на 5х5
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
    }
}