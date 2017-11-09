using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceShooter.Models;

namespace SpaceShooter.Models.Tests
{
    [TestClass]
    public class PlayerListEntryTests : IDisposable
    {
        public void Dispose()
        {
            DB.ClearAll();
        }
        public PlayerListEntryTests()
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
            PlayerListEntry leaderboard1 = new PlayerListEntry(player1.Id, player1.Username, 1000);
            PlayerListEntry leaderboard2 = new PlayerListEntry(player1.Id, player1.Username, 1000);

            Assert.AreEqual(leaderboard1, leaderboard2);
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

            PlayerListEntry leaderboard1 = new PlayerListEntry(player1.Id, player1.Username, 1000);
            PlayerListEntry leaderboard2 = new PlayerListEntry(player2.Id, player2.Username, 23458);

            Assert.AreNotEqual(leaderboard1, leaderboard2);
        }
        [TestMethod]
        public void GetLeaderboard_GetsLeaderboardStats_Stats()
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

            PlayerListEntry leaderboard1 = new PlayerListEntry(player1.Id, player1.Username, stats1.Score);
            PlayerListEntry leaderboard2 = new PlayerListEntry(player2.Id, player2.Username, stats2.Score);

            List<PlayerListEntry> result = PlayerListEntry.GetLeaderboard();
            List<PlayerListEntry> test = new List<PlayerListEntry> {leaderboard2, leaderboard1};

            CollectionAssert.AreEqual(test, result);
        }
        [TestMethod]
        public void GetSearchResults_GetsSearchResults_Results()
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

            PlayerListEntry search1 = new PlayerListEntry(player1.Id, player1.Username, stats1.Score);
            PlayerListEntry search2 = new PlayerListEntry(player2.Id, player2.Username, stats2.Score);

            Dictionary<string, PlayerListEntry> result = PlayerListEntry.GetSearchResults("");
            Dictionary<string, PlayerListEntry> test = new Dictionary<string, PlayerListEntry> {
                {player2.Username, search2},
                {player1.Username, search1}
            };

            CollectionAssert.AreEqual(test, result);
        }
    }
}
