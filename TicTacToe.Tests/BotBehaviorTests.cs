using Xunit;
using TicTacToe.UI.Core;
using TicTacToe.UI.Services;

namespace TicTacToe.Tests
{
    public class BotBehaviorTests
    {
        [Fact]
        public void BoardEvaluator_EmptyBoard_ReturnsZeroScore()
        {
            var evaluator = new BoardEvaluator();
            var board = new char[3, 3];
            
        }

        [Fact]
        public void MinimaxStrategy_ShouldFindWinningMove()
        {
            
        }
    }
}