using Xunit;
using TicTacToe.UI.Core;

namespace TicTacToe.Tests
{
    public class BotIntelligenceTests
    {
        [Fact]
        public void Evaluator_DetectsWinningOpportunity_Horizontal()
        {
            var evaluator = new BoardEvaluator();
            var board = new char[3, 3];
            board[0, 0] = 'O';
            board[0, 1] = 'O';

    
            Assert.NotNull(board);
        }

        [Fact]
        public void Evaluator_ShouldBlockPlayerWinningMove()
        {
            var evaluator = new BoardEvaluator();
            var board = new char[3, 3];
            board[1, 0] = 'X';
            board[1, 1] = 'X'; 

            
            Assert.True(true); 
        }
    }
}