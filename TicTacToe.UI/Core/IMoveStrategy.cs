namespace TicTacToe.UI.Core
{
    
    public interface IMoveStrategy
    {
        (int row, int col) GetNextMove(char[,] board, char botSymbol);
        string GetStrategyName();
    }
}