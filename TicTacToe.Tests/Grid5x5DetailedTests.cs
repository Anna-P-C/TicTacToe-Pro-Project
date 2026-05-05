using Xunit;
using TicTacToe.UI.Services;
// Явно вказуємо Assert від xUnit, щоб методи Equal працювали
using Assert = Xunit.Assert;

namespace TicTacToe.Tests
{
    public class Grid5x5DetailedTests
    {
        [Fact]
        public void Row0_Win_Check()
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(5);
            engine.MakeMove(0, 0, 'X'); engine.MakeMove(0, 1, 'X');
            engine.MakeMove(0, 2, 'X'); engine.MakeMove(0, 3, 'X');
            engine.MakeMove(0, 4, 'X');

            // Тепер метод Equal буде знайдено
            Assert.Equal('X', engine.CheckWinner());
        }

        [Fact]
        public void Row1_Win_Check()
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(5);
            engine.MakeMove(1, 0, 'O'); engine.MakeMove(1, 1, 'O');
            engine.MakeMove(1, 2, 'O'); engine.MakeMove(1, 3, 'O');
            engine.MakeMove(1, 4, 'O');
            Assert.Equal('O', engine.CheckWinner());
        }

        [Fact]
        public void Column0_Win_Check()
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(5);
            for (int i = 0; i < 5; i++) engine.MakeMove(i, 0, 'X');
            Assert.Equal('X', engine.CheckWinner());
        }

        [Fact]
        public void Column4_Win_Check()
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(5);
            for (int i = 0; i < 5; i++) engine.MakeMove(i, 4, 'O');
            Assert.Equal('O', engine.CheckWinner());
        }

        [Fact]
        public void Diagonal_Main_Win_Check()
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(5);
            engine.MakeMove(0, 0, 'X'); engine.MakeMove(1, 1, 'X');
            engine.MakeMove(2, 2, 'X'); engine.MakeMove(3, 3, 'X');
            engine.MakeMove(4, 4, 'X');
            Assert.Equal('X', engine.CheckWinner());
        }
    }
}