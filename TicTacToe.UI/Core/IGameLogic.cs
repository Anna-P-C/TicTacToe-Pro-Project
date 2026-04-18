namespace TicTacToe.UI.Core
{
    public interface IGameLogic
    {
        bool MakeMove(int row, int col, char symbol);
        char CheckWinner();
        void ResetBoard();
        char[,] GetBoardState();
    }
}