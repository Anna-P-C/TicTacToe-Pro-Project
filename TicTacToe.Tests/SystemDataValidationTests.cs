using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.UI.Models;
using TicTacToe.UI.Services;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TicTacToe.Tests
{
    [TestClass]
    public class SystemDataValidationTests
    {
        [TestMethod]
        public void Test_Complete_System_Infrastructure_Integrity()
        {
            var scoreService = ScoreService.Instance;
            Assert.IsNotNull(scoreService);

           
            string testName = "Anna";
            var testPlayer = new Player(testName, 'X');

            scoreService.ClearAllScores();
            scoreService.SaveScore(testPlayer, 150);

            var players = scoreService.GetPlayers();
            Assert.IsTrue(players.Any(p => p.Name == testName));

            var top = scoreService.GetTopPlayers(1);
            Assert.AreEqual(testName, top[0].Name);
            Assert.AreEqual(150, top[0].Score);

            scoreService.SaveScore(testPlayer, 50);
            Assert.AreEqual(200, scoreService.GetPlayers().First(p => p.Name == testName).Score);

            var analytics = AnalyticsService.Instance;
            Assert.IsNotNull(analytics);

            analytics.StartRoundTimer();
            analytics.RegisterMove();
            analytics.SaveSession(testName, 5, 2, 45);

            string proAdvice = analytics.GetPerformanceAdvice(10, 2);
            Assert.AreEqual("Ти граєш як профі!", proAdvice);

        
            var playerX = new Player("Artyom", 'X');
            playerX.Score = 500;
            var playerO = new Player("Vika", 'O');
            playerO.Score = 300;

            Assert.AreNotEqual(playerX.Name, playerO.Name);
            Assert.IsTrue(playerX.Score > playerO.Score);

           
            for (int i = 0; i < 50; i++)
            {
                var bot = new Player("Bot_" + i, (i % 2 == 0) ? 'X' : 'O');
                scoreService.SaveScore(bot, i * 10);
            }

            var allSaved = scoreService.GetPlayers();
            Assert.IsTrue(allSaved.Count >= 50);

            var topList = scoreService.GetTopPlayers(5);
            for (int i = 0; i < topList.Count - 1; i++)
            {
                Assert.IsTrue(topList[i].Score >= topList[i + 1].Score);
            }

            analytics.SaveSession("EmptyUser", 0, 0, 0);
            Assert.IsNotNull(analytics.GetPerformanceAdvice(0, 0));

            try
            {
                var uniquePlayer = new Player("Unique_User", 'X');
                scoreService.SaveScore(uniquePlayer, 0);
                var p = scoreService.GetPlayers().FirstOrDefault(x => x.Name == "Unique_User");
                Assert.IsNotNull(p);
            }
            catch (Exception ex)
            {
                Assert.Fail("Error: " + ex.Message);
            }
        }

        [TestMethod]
        public void Test_Data_Persistence_Validation()
        {
            var service = ScoreService.Instance;
            service.ClearAllScores();

            var p1 = new Player("User1", 'X');
            service.SaveScore(p1, 100);

            var currentPlayers = service.GetPlayers();
            Assert.AreEqual(1, currentPlayers.Count);
            Assert.AreEqual("User1", currentPlayers[0].Name);
        }

        [TestMethod]
        public void Test_Performance_Advice_Logic_Flow()
        {
            var analytics = AnalyticsService.Instance;
            Assert.AreEqual("Ти граєш як профі!", analytics.GetPerformanceAdvice(8, 2));
            Assert.AreEqual("Непогано, але можна краще.", analytics.GetPerformanceAdvice(5, 5));
            Assert.AreEqual("Тобі треба більше практики.", analytics.GetPerformanceAdvice(2, 8));
        }

        [TestMethod]
        public void Test_Detailed_Player_State()
        {
      
            var player = new Player("TestSystem", 'O');
            player.Score = 999;
            player.LastPlayed = DateTime.Now;

            Assert.AreEqual("TestSystem", player.Name);
            Assert.AreEqual('O', player.Symbol);
            Assert.AreEqual(999, player.Score);
            Assert.IsNotNull(player.LastPlayed);

            var playersList = new List<Player>();
            for (int k = 0; k < 10; k++)
            {
                playersList.Add(new Player("Player_" + k, 'X'));
            }
            Assert.AreEqual(10, playersList.Count);
        }
    }
}
