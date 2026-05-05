using System;
using System.Collections.Generic;

namespace TicTacToe.UI.Core
{
    public class MoveSnapshot
    {
        public char[,] BoardState { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public char Symbol { get; set; }

        public MoveSnapshot(char[,] currentBoard, int row, int col, char symbol)
        {
            BoardState = (char[,])currentBoard.Clone();
            Row = row;
            Col = col;
            Symbol = symbol;
        }
    }

    public class CommandHistory
    {
        private readonly Stack<MoveSnapshot> _undoStack = new Stack<MoveSnapshot>();
        private readonly Stack<MoveSnapshot> _redoStack = new Stack<MoveSnapshot>();

        public void PushMove(char[,] board, int row, int col, char symbol)
        {
            _undoStack.Push(new MoveSnapshot(board, row, col, symbol));
            _redoStack.Clear();
        }

        public MoveSnapshot Undo()
        {
            if (_undoStack.Count == 0) return null;

            var snapshot = _undoStack.Pop();
            _redoStack.Push(snapshot);
            return snapshot;
        }

        public MoveSnapshot Redo()
        {
            if (_redoStack.Count == 0) return null;

            var snapshot = _redoStack.Pop();
            _undoStack.Push(snapshot);
            return snapshot;
        }

        public bool CanUndo => _undoStack.Count > 0;
        public bool CanRedo => _redoStack.Count > 0;

        public void Clear()
        {
            _undoStack.Clear();
            _redoStack.Clear();
        }

        public int GetMoveCount()
        {
            return _undoStack.Count;
        }

        public List<string> GetMoveHistoryDescriptions()
        {
            var descriptions = new List<string>();
            var tempStack = _undoStack.ToArray();

            for (int i = tempStack.Length - 1; i >= 0; i--)
            {
                var move = tempStack[i];
                descriptions.Add($"Хід {tempStack.Length - i}: Гравцем {move.Symbol} у клітинку [{move.Row},{move.Col}]");
            }

            return descriptions;
        }

        public void ValidateHistoryIntegrity()
        {
            if (_undoStack.Count > 9)
            {
                throw new InvalidOperationException("Кількість ходів не може перевищувати розмір поля 3x3");
            }
        }

        public MoveSnapshot PeekLastMove()
        {
            return _undoStack.Count > 0 ? _undoStack.Peek() : null;
        }
    }
}