using TicTacToe.UI.Core;

namespace TicTacToe.UI.Services
{
    public class GameEngine : IGameLogic
    {
        private char[,] _board = new char[3, 3];

        public bool MakeMove(int row, int col, char symbol)
        {
            if (_board[row, col] == '\0')
            {
                _board[row, col] = symbol;
                return true;
            }
            return false;
        }

        public char CheckWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_board[i, 0] != '\0' && _board[i, 0] == _board[i, 1] && _board[i, 1] == _board[i, 2]) return _board[i, 0];
                if (_board[0, i] != '\0' && _board[0, i] == _board[1, i] && _board[1, i] == _board[2, i]) return _board[0, i];
            }

            if (_board[0, 0] != '\0' && _board[0, 0] == _board[1, 1] && _board[1, 1] == _board[2, 2]) return _board[0, 0];
            if (_board[0, 2] != '\0' && _board[0, 2] == _board[1, 1] && _board[1, 1] == _board[2, 0]) return _board[0, 2];

            return '\0';
        }

        public void ResetBoard() => _board = new char[3, 3];
        public char[,] GetBoardState() => _board;
    }
}