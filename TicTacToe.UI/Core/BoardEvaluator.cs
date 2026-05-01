using System;
using System.Collections.Generic;

namespace TicTacToe.UI.Core
{
    public class BoardEvaluator
    {
        private const int WinScore = 10000;
        private const int BlockScore = 5000;
        private const int CenterWeight = 100;
        private const int CornerWeight = 50;
        private const int EdgeWeight = 10;

        public int EvaluateBoard(char[,] board, char botSymbol, char playerSymbol)
        {
            int totalScore = 0;

            totalScore += EvaluateLines(board, botSymbol, playerSymbol);
            totalScore += EvaluatePositionalWeight(board, botSymbol);
            totalScore += EvaluatePotentialPatterns(board, botSymbol, playerSymbol);

            return totalScore;
        }

        private int EvaluateLines(char[,] board, char botSymbol, char playerSymbol)
        {
            int score = 0;

            for (int i = 0; i < 3; i++)
            {
                score += AnalyzeLine(board[i, 0], board[i, 1], board[i, 2], botSymbol, playerSymbol);
                score += AnalyzeLine(board[0, i], board[1, i], board[2, i], botSymbol, playerSymbol);
            }

            score += AnalyzeLine(board[0, 0], board[1, 1], board[2, 2], botSymbol, playerSymbol);
            score += AnalyzeLine(board[0, 2], board[1, 1], board[2, 0], botSymbol, playerSymbol);

            return score;
        }

        private int AnalyzeLine(char c1, char c2, char c3, char botSymbol, char playerSymbol)
        {
            int botCount = 0;
            int playerCount = 0;
            int emptyCount = 0;

            char[] cells = { c1, c2, c3 };
            foreach (var cell in cells)
            {
                if (cell == botSymbol) botCount++;
                else if (cell == playerSymbol) playerCount++;
                else emptyCount++;
            }

            if (botCount == 3) return WinScore;
            if (botCount == 2 && emptyCount == 1) return 500;
            if (botCount == 1 && emptyCount == 2) return 50;

            if (playerCount == 2 && emptyCount == 1) return -BlockScore;
            if (playerCount == 1 && emptyCount == 2) return -100;

            return 0;
        }

        private int EvaluatePositionalWeight(char[,] board, char botSymbol)
        {
            int score = 0;

            if (board[1, 1] == botSymbol) score += CenterWeight;

            int[,] corners = { { 0, 0 }, { 0, 2 }, { 2, 0 }, { 2, 2 } };
            for (int i = 0; i < 4; i++)
            {
                if (board[corners[i, 0], corners[i, 1]] == botSymbol)
                    score += CornerWeight;
            }

            int[,] edges = { { 0, 1 }, { 1, 0 }, { 1, 2 }, { 2, 1 } };
            for (int i = 0; i < 4; i++)
            {
                if (board[edges[i, 0], edges[i, 1]] == botSymbol)
                    score += EdgeWeight;
            }

            return score;
        }

        private int EvaluatePotentialPatterns(char[,] board, char botSymbol, char playerSymbol)
        {
            int score = 0;

            if (board[0, 0] == botSymbol && board[2, 2] == botSymbol && board[1, 1] == '\0')
                score += 30;

            if (board[0, 2] == botSymbol && board[2, 0] == botSymbol && board[1, 1] == '\0')
                score += 30;

            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == botSymbol && board[i, 2] == botSymbol && board[i, 1] == '\0') score += 20;
                if (board[0, i] == botSymbol && board[2, i] == botSymbol && board[1, i] == '\0') score += 20;
            }

            return score;
        }

        public List<Tuple<int, int>> GetEmptyCells(char[,] board)
        {
            var emptyCells = new List<Tuple<int, int>>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == '\0')
                        emptyCells.Add(new Tuple<int, int>(i, j));
                }
            }
            return emptyCells;
        }
    }
}