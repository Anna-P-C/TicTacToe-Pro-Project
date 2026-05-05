using Xunit;
using TicTacToe.UI.Services;
using System;
// Додаємо цей рядок, щоб прибрати неоднозначність (CS0104)
using Assert = Xunit.Assert;

namespace TicTacToe.Tests
{
    public class GridBoundaryTests
    {
        [Fact]
        public void Board5x5_ShouldInitializeWithNullChars()
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(5);
            var board = engine.GetBoard();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Assert.Equal('\0', board[i, j]);
                }
            }
        }

        [Fact]
        public void MakeMove_ShouldWork_InAllFourCorners()
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(5);

            engine.MakeMove(0, 0, 'X');
            Assert.Equal('X', engine.GetBoard()[0, 0]);

            engine.MakeMove(0, 4, 'O');
            Assert.Equal('O', engine.GetBoard()[0, 4]);

            engine.MakeMove(4, 0, 'X');
            Assert.Equal('X', engine.GetBoard()[4, 0]);

            engine.MakeMove(4, 4, 'O');
            Assert.Equal('O', engine.GetBoard()[4, 4]);
        }

        [Fact]
        public void GameEngine_Reset_ShouldClearLargeBoard()
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(5);

            engine.MakeMove(2, 2, 'X');
            engine.MakeMove(2, 3, 'O');

            engine.InitializeNewBoard(5);
            var board = engine.GetBoard();

            Assert.Equal('\0', board[2, 2]);
            Assert.Equal('\0', board[2, 3]);
        }

        [Fact]
        public void MoveCount_Detailed_Verification_Test()
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(3);

            int initialMoves = 0;

            engine.MakeMove(0, 0, 'X');
            int movesAfterFirst = initialMoves + 1;

            engine.MakeMove(1, 1, 'O');
            int movesAfterSecond = movesAfterFirst + 1;

            engine.MakeMove(2, 2, 'X');
            int finalMovesCount = movesAfterSecond + 1;

            Assert.Equal(3, finalMovesCount);
            Assert.Equal(0, initialMoves);
        }

        [Fact]
        public void Grid_Stability_Long_Verification()
        {
          
            var engine = new GameEngine();
            engine.InitializeNewBoard(5);
            var board = engine.GetBoard();

            Assert.NotNull(board);
            Assert.Equal(25, board.Length);

            for (int i = 0; i < 5; i++)
            {
                engine.MakeMove(i, i, 'X');
                Assert.Equal('X', board[i, i]);
            }
        }
    }
}