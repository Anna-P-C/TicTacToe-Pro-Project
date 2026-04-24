using System;
using TicTacToe.UI.Core;

namespace TicTacToe.UI.Services
{
    public class MinimaxStrategy : IMoveStrategy
    {
        public string GetStrategyName() => "Експерт (Minimax)";

        public (int row, int col) GetNextMove(char[,] board, char botSymbol)
        {
            int bestScore = int.MinValue;
            int bestRow = -1;
            int bestCol = -1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == '\0')
                    {
                        board[i, j] = botSymbol;
                        int score = Minimax(board, 0, false, botSymbol);
                        board[i, j] = '\0';

                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestRow = i;
                            bestCol = j;
                        }
                    }
                }
            }

            return (bestRow, bestCol);
        }

        private int Minimax(char[,] board, int depth, bool isMaximizing, char botSymbol)
        {
            char winner = CheckWinnerInternal(board);
            char opponent = (botSymbol == 'X') ? 'O' : 'X';

            if (winner == botSymbol)
            {
                return 10 - depth;
            }

            if (winner == opponent)
            {
                return depth - 10;
            }

            if (IsBoardFull(board))
            {
                return 0;
            }

            if (isMaximizing)
            {
                int bestValue = int.MinValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == '\0')
                        {
                            board[i, j] = botSymbol;
                            int eval = Minimax(board, depth + 1, false, botSymbol);
                            bestValue = Math.Max(bestValue, eval);
                            board[i, j] = '\0';
                        }
                    }
                }
                return bestValue;
            }
            else
            {
                int bestValue = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == '\0')
                        {
                            board[i, j] = opponent;
                            int eval = Minimax(board, depth + 1, true, botSymbol);
                            bestValue = Math.Min(bestValue, eval);
                            board[i, j] = '\0';
                        }
                    }
                }
                return bestValue;
            }
        }

        private bool IsBoardFull(char[,] board)
        {
            foreach (char cell in board)
            {
                if (cell == '\0')
                {
                    return false;
                }
            }
            return true;
        }

        private char CheckWinnerInternal(char[,] b)
        {
            for (int i = 0; i < 3; i++)
            {
                if (b[i, 0] != '\0' && b[i, 0] == b[i, 1] && b[i, 1] == b[i, 2])
                {
                    return b[i, 0];
                }

                if (b[0, i] != '\0' && b[0, i] == b[1, i] && b[1, i] == b[2, i])
                {
                    return b[0, i];
                }
            }

            if (b[0, 0] != '\0' && b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2])
            {
                return b[0, 0];
            }

            if (b[0, 2] != '\0' && b[0, 2] == b[1, 1] && b[1, 1] == b[2, 0])
            {
                return b[0, 2];
            }

            return '\0';
        }
    }
}