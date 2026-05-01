namespace TicTacToe.UI.Core
{
    public interface IGameLogic
    {
        bool MakeMove(int row, int col, char symbol);
        char CheckWinner();
        char[,] GetBoard();
        char[,] GetBoardState(); 
        void ResetBoard();
        void SetBoard(char[,] board);
        void InitializeNewBoard(int size);
    }
}