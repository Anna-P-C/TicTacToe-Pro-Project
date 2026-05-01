using TicTacToe.UI.Core;

namespace TicTacToe.UI.Services
{
    public class GameEngine : IGameLogic
    {
        private char[,] _board;
        private int _size;

        public GameEngine()
        {
            InitializeNewBoard(3);
        }

        public void InitializeNewBoard(int size)
        {
            _size = size;
            _board = new char[size, size];
        }

        public bool MakeMove(int row, int col, char symbol)
        {
            if (row >= 0 && row < _size && col >= 0 && col < _size && _board[row, col] == '\0')
            {
                _board[row, col] = symbol;
                return true;
            }
            return false;
        }

        public char CheckWinner()
        {
            for (int i = 0; i < _size; i++)
            {
                if (CheckLine(i, 0, 0, 1)) return _board[i, 0];
                if (CheckLine(0, i, 1, 0)) return _board[0, i];
            }

            if (CheckLine(0, 0, 1, 1)) return _board[0, 0];
            if (CheckLine(0, _size - 1, 1, -1)) return _board[0, _size - 1];

            return '\0';
        }

        private bool CheckLine(int startRow, int startCol, int dRow, int dCol)
        {
            char first = _board[startRow, startCol];
            if (first == '\0') return false;

            for (int i = 1; i < _size; i++)
            {
                if (_board[startRow + i * dRow, startCol + i * dCol] != first)
                    return false;
            }
            return true;
        }

        public void ResetBoard()
        {
            _board = new char[_size, _size];
        }

        public char[,] GetBoard() => _board;

        public char[,] GetBoardState() => _board;

        public void SetBoard(char[,] board)
        {
            int inputSize = board.GetLength(0);
            if (inputSize != _size) InitializeNewBoard(inputSize);

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _board[i, j] = board[i, j];
                }
            }
        }
    }
}