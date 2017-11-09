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
            string salt2 = Player.MakeSalt();
            Hash hash2 = new Hash("password", salt2);
            Player player2 = new Player("duhbigotaku", hash2.Result, salt2);
            player2.Save();

            Friend friend1 = new Friend(player1.Id, player2.Id);
            Friend friend2 = new Friend(player1.Id, player2.Id);
            Assert.AreEqual(friend1, friend2);
        }
        [TestMethod]
        public void Equals_NotSame_False()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();
            string salt2 = Player.MakeSalt();
            Hash hash2 = new Hash("password", salt2);
            Player player2 = new Player("duhbigotaku", hash2.Result, salt2);
            player2.Save();
            string salt3 = Player.MakeSalt();
            Hash hash3 = new Hash("password", salt3);
            Player player3 = new Player("draguni", hash3.Result, salt3);
            player3.Save();

            Friend friend1 = new Friend(player1.Id, player2.Id);
            Friend friend2 = new Friend(player1.Id, player3.Id);
            Assert.AreNotEqual(friend1, friend2);
        }
        [TestMethod]
        public void GetAllFriendPairs_GetAllFriendPairsFromDatabase_FriendsList()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();
            string salt2 = Player.MakeSalt();
            Hash hash2 = new Hash("password", salt2);
            Player player2 = new Player("duhbigotaku", hash2.Result, salt2);
            player2.Save();
            string salt3 = Player.MakeSalt();
            Hash hash3 = new Hash("password", salt3);
            Player player3 = new Player("draguni", hash3.Result, salt3);
            player3.Save();

            Friend friend1 = new Friend(player1.Id, player2.Id);
            friend1.Save();
            Friend friend2 = new Friend(player1.Id, player3.Id);
            friend2.Save();

            List<Friend> result = Friend.GetAllFriendPairs();
            List<Friend> test = new List<Friend>{friend1, friend2};
            CollectionAssert.AreEqual(test, result);
        }
    }
}
