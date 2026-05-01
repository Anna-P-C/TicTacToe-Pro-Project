using TicTacToe.UI.Core;

namespace TicTacToe.UI.Services
{
    public class EasyStrategy : IMoveStrategy
    {
        public string GetStrategyName() => "Низька (Random)";

        public (int row, int col) GetNextMove(char[,] board, char botSymbol)
        {
            Random rand = new Random();
            int r, c;
            do
            {
                r = rand.Next(0, 3);
                c = rand.Next(0, 3);
            } while (board[r, c] != '\0');

            return (r, c);
        }
    }
}