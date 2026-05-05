namespace TicTacToe.UI.Core
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogError(string message, Exception ex = null);
        void LogMove(string player, int row, int col);
    }
}