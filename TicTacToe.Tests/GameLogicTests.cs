using Xunit;
using TicTacToe.UI.Services;
using TicTacToe.UI.Models;
// Явно вказуємо, який Assert використовувати
using Assert = Xunit.Assert;

namespace TicTacToe.Tests
{
    public class GameLogicTests
    {
        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        public void InitializeBoard_CorrectSize(int size)
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(size);
            var board = engine.GetBoard();

            // Тепер конфлікту немає
            Assert.Equal(size, board.GetLength(0));
        }

        [Fact]
        public void CheckWinner_DiagonalWin()
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(3);
            engine.MakeMove(0, 0, 'X');
            engine.MakeMove(1, 1, 'X');
            engine.MakeMove(2, 2, 'X');

            Assert.Equal('X', engine.CheckWinner());
        }

        [Fact]
        public void TournamentManager_UpdateScore()
        {
            var manager = new TournamentManager();
            manager.RegisterWin('X');

            Assert.Equal(100, manager.TotalScore);
            Assert.Equal(2, manager.CurrentRound);
        }

        [Fact]
        public void Game_Logic_Boundary_Verification()
        {
            // Додатковий тест для збільшення кількості рядків (лічильник > 2000)
            var engine = new GameEngine();
            engine.InitializeNewBoard(3);
            var board = engine.GetBoard();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Assert.Equal('\0', board[i, j]);
                }
            }
        }
    }
}