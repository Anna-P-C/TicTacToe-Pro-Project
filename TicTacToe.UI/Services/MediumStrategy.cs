using TicTacToe.UI.Core;

namespace TicTacToe.UI.Services
{
    public class MediumStrategy : IMoveStrategy
    {
        public string GetStrategyName() => "Середня (Heuristic)";

        public (int row, int col) GetNextMove(char[,] board, char botSymbol)
        {
            char opponentSymbol = botSymbol == 'X' ? 'O' : 'X';

  
            var winMove = FindWinningMove(board, botSymbol);
            if (winMove.HasValue) return winMove.Value;

           
            var blockMove = FindWinningMove(board, opponentSymbol);
            if (blockMove.HasValue) return blockMove.Value;

            
            if (board[1, 1] == '\0') return (1, 1);

            
            return new EasyStrategy().GetNextMove(board, botSymbol);
        }

        private (int, int)? FindWinningMove(char[,] board, char symbol)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == '\0')
                    {
                        board[i, j] = symbol;
                        if (IsWinning(board, symbol))
                        {
                            board[i, j] = '\0';
                            return (i, j);
                        }
                        board[i, j] = '\0';
                    }
                }
            }
            return null;
        }

        private bool IsWinning(char[,] b, char s)
        {
            for (int i = 0; i < 3; i++)
            {
                if (b[i, 0] == s && b[i, 1] == s && b[i, 2] == s) return true;
                if (b[0, i] == s && b[1, i] == s && b[2, i] == s) return true;
            }
            if (b[0, 0] == s && b[1, 1] == s && b[2, 2] == s) return true;
            if (b[0, 2] == s && b[1, 1] == s && b[2, 0] == s) return true;
            return false;
        }
    }
}