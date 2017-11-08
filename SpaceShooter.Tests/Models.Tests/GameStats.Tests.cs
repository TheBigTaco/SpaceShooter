using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceShooter.Models;

namespace SpaceShooter.Models.Tests
{
    [TestClass]
    public class GameStatsTests : IDisposable
    {
        public void Dispose()
        {
            DB.ClearAll();
        }
        public GameStatsTests()
        {
            DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=8889; database=space_shooter_test;";
        }
        [TestMethod]
        public void Equals_Same_True()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();

            GameStats stats1 = new GameStats(player1.Id, 100, 1, 1000, new DateTime(2013, 10, 3));
            GameStats stats2 = new GameStats(player1.Id, 100, 1, 1000, new DateTime(2013, 10, 3));
            Assert.AreEqual(stats1, stats2);
        }
        [TestMethod]
        public void Equals_NotSame_False()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();
            string salt2 = Player.MakeSalt();
            Hash hash2 = new Hash("password1", salt2);
            Player player2 = new Player("lydianlights", hash2.Result, salt2);
            player2.Save();

            GameStats stats1 = new GameStats(player1.Id, 100, 1, 1000, new DateTime(2013, 10, 3));
            GameStats stats2 = new GameStats(player2.Id, 100000, 100, 9999, new DateTime(2013, 10, 3));
            Assert.AreNotEqual(stats1, stats2);
        }
        [TestMethod]
        public void GetAllOrderedByScore_GetsAllStatsFromDatabase_AllStats()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();
            string salt2 = Player.MakeSalt();
            Hash hash2 = new Hash("password1", salt2);
            Player player2 = new Player("lydianlights", hash2.Result, salt2);
            player2.Save();
            GameStats stats1 = new GameStats(player1.Id, 100, 1, 1000, new DateTime(2013, 10, 3));
            stats1.Save();
            GameStats stats2 = new GameStats(player2.Id, 100000, 100, 9999, new DateTime(2013, 10, 3));
            stats2.Save();

            List<GameStats> result = GameStats.GetAllOrderedByScore();
            List<GameStats> test = new List<GameStats> {stats2, stats1};

            CollectionAssert.AreEqual(test, result);
        }
        [TestMethod]
        public void GetPlayerHighScore_GetsHighestScore_HighestScore()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();
            GameStats stats1 = new GameStats(player1.Id, 100, 1, 1000, new DateTime(2013, 10, 3));
            stats1.Save();
            GameStats stats2 = new GameStats(player1.Id, 100000, 100, 9999, new DateTime(2013, 10, 3));
            stats2.Save();
            GameStats stats3 = new GameStats(player1.Id, 222, 100, 9999, new DateTime(2013, 10, 3));
            stats3.Save();

            long result = GameStats.GetPlayerHighScore(player1.Id);
            long test = 100000;

            Assert.AreEqual(test, result);
        }
        [TestMethod]
        public void GetPlayerTotalScore_GetsTotalScore_TotalScore()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();
            GameStats stats1 = new GameStats(player1.Id, 100, 1, 1000, new DateTime(2013, 10, 3));
            stats1.Save();
            GameStats stats2 = new GameStats(player1.Id, 100000, 100, 9999, new DateTime(2013, 10, 3));
            stats2.Save();
            GameStats stats3 = new GameStats(player1.Id, 222, 100, 9999, new DateTime(2013, 10, 3));
            stats3.Save();

            long result = GameStats.GetPlayerTotalScore(player1.Id);
            long test = 100322;

            Assert.AreEqual(test, result);
        }
        [TestMethod]
        public void GetPlayerTotalEnemiesDestroyed_GetsTotalEnemiesDestroyed_TotalEnemiesDestroyed()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();
            GameStats stats1 = new GameStats(player1.Id, 100, 1, 1000, new DateTime(2013, 10, 3));
            stats1.Save();
            GameStats stats2 = new GameStats(player1.Id, 100000, 100, 9999, new DateTime(2013, 10, 3));
            stats2.Save();
            GameStats stats3 = new GameStats(player1.Id, 222, 100, 9999, new DateTime(2013, 10, 3));
            stats3.Save();

            long result = GameStats.GetPlayerTotalEnemiesDestroyed(player1.Id);
            long test = 201;

            Assert.AreEqual(test, result);
        }
        [TestMethod]
        public void GetPlayerTotalTimePlayed_GetsTotalTimePlayed_TotalTimePlayed()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();
            GameStats stats1 = new GameStats(player1.Id, 100, 1, 1000, new DateTime(2013, 10, 3));
            stats1.Save();
            GameStats stats2 = new GameStats(player1.Id, 100000, 100, 9999, new DateTime(2013, 10, 3));
            stats2.Save();
            GameStats stats3 = new GameStats(player1.Id, 222, 100, 9999, new DateTime(2013, 10, 3));
            stats3.Save();

            long result = GameStats.GetPlayerTotalTimePlayed(player1.Id);
            long test = 20998;

            Assert.AreEqual(test, result);
        }
        [TestMethod]
        public void GetPlayerMostRecentStats_GetsMostRecentStats_MostRecentStats()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();
            GameStats stats1 = new GameStats(player1.Id, 100, 1, 1000, new DateTime(2017, 10, 3));
            stats1.Save();
            GameStats stats2 = new GameStats(player1.Id, 100000, 100, 9999, new DateTime(2017, 11, 3));
            stats2.Save();
            GameStats stats3 = new GameStats(player1.Id, 222, 100, 9999, new DateTime(2013, 10, 3));
            stats3.Save();

            GameStats result = GameStats.GetPlayerMostRecentStats(player1.Id);
            GameStats test = stats2;

            Assert.AreEqual(test, result);
        }
    }
}
