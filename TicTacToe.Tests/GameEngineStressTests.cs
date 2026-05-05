using Xunit;
using TicTacToe.UI.Services;
using System;
using System.Collections.Generic;
using Assert = Xunit.Assert;

namespace TicTacToe.Tests
{
    public class GameEngineStressTests
    {
        [Fact]
        public void StressTest_MultipleBoardInitializations()
        {
            var engine = new GameEngine();
            
            for (int i = 0; i < 50; i++)
            {
                int size = (i % 2 == 0) ? 3 : 5;
                engine.InitializeNewBoard(size);
                var board = engine.GetBoard();

                Assert.NotNull(board);
                Assert.Equal(size, board.GetLength(0));
            }
        }

        [Fact]
        public void StressTest_MassiveMoveSequence()
        {
            var engine = new GameEngine();
            engine.InitializeNewBoard(5);

         
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    char symbol = ((row + col) % 2 == 0) ? 'X' : 'O';
                    engine.MakeMove(row, col, symbol);

                    var currentBoard = engine.GetBoard();
                    Assert.Equal(symbol, currentBoard[row, col]);

                    
                    var potentialWinner = engine.CheckWinner();
                    Assert.True(potentialWinner == 'X' || potentialWinner == 'O' || potentialWinner == '\0');
                }
            }
        }

        [Fact]
        public void DataValidation_HighLoad_ScoreService()
        {
            var scoreService = ScoreService.Instance;
            scoreService.ClearAllScores();

           
            for (int k = 0; k < 40; k++)
            {
                var player = new TicTacToe.UI.Models.Player("Gamer_" + k, 'X');
                scoreService.SaveScore(player, k * 10);
            }

            var topPlayers = scoreService.GetTopPlayers(10);
            Assert.Equal(10, topPlayers.Count);
         
            Assert.True(topPlayers[0].Score > topPlayers[9].Score);
        }

        [Fact]
        public void Analytics_Deep_History_Validation()
        {
            var analytics = AnalyticsService.Instance;

            for (int i = 0; i < 20; i++)
            {
                analytics.StartRoundTimer();
                analytics.RegisterMove();
                analytics.SaveSession("Anna_Test", 10, 5, 120);

                var advice = analytics.GetPerformanceAdvice(i, 2);
                Assert.NotNull(advice);
            }

            Assert.True(analytics != null);
        }
    }
}