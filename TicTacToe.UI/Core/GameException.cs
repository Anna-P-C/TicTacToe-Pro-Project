using System;

namespace TicTacToe.UI.Core
{
    public class GameException : Exception
    {
        public GameException(string message) : base(message) { }
    }

    public class InvalidMoveException : GameException
    {
        public int Row { get; }
        public int Col { get; }

        public InvalidMoveException(int row, int col)
            : base($"Хід у клітинку [{row}, {col}] неможливий, вона вже зайнята!")
        {
            Row = row;
            Col = col;
        }
    }

    public class TournamentException : GameException
    {
        public TournamentException(string message) : base(message) { }
    }
}
