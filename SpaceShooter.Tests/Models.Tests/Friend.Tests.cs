using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceShooter.Models;

namespace SpaceShooter.Models.Tests
{
    [TestClass]
    public class FriendTests : IDisposable
    {
        public void Dispose()
        {
            DB.ClearAll();
        }
        public FriendTests()
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

            Friend friend1 = new Friend(player1.Username, player1.Id);
            Friend friend2 = new Friend(player1.Username, player1.Id);
            Assert.AreEqual(friend1, friend2);
        }
        public void GetAllFriendsForPlayer_GetFriendsForASinglePlayer_FriendsList()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();
            string salt2 = Player.MakeSalt();
            Hash hash2 = new Hash("password", salt2);
            Player player2 = new Player("duhbigotaku", hash2.Result, salt2);
            player2.Save();
            FriendPair friendpair1 = new FriendPair(player1.Id, player2.Id);
            friendpair1.Save();

            Friend test = new Friend(player1.Username, player1.Id);
            Friend result = Friend.GetAllFriendsForPlayer(player1.Id)[0];

            Assert.AreEqual(test, result);
        }
    }
}
