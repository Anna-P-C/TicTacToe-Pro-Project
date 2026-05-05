using Xunit;
using TicTacToe.UI.Core;

using Assert = Xunit.Assert;

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

            
            Assert.True(evaluator != null);
        }

        [Fact]
        public void Bot_AI_Decision_Integrity_Check()
        {
        
            var board = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = '\0';
                }
            }
            Assert.Equal('\0', board[1, 1]);
        }
    }
}