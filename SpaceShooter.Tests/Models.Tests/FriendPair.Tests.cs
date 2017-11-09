using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceShooter.Models;

namespace SpaceShooter.Models.Tests
{
    [TestClass]
    public class FriendPairTests : IDisposable
    {
        public void Dispose()
        {
            DB.ClearAll();
        }
        public FriendPairTests()
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

            FriendPair friend1 = new FriendPair(player1.Id, player2.Id);
            FriendPair friend2 = new FriendPair(player1.Id, player2.Id);
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

            FriendPair friend1 = new FriendPair(player1.Id, player2.Id);
            FriendPair friend2 = new FriendPair(player1.Id, player3.Id);
            Assert.AreNotEqual(friend1, friend2);
        }
        [TestMethod]
        public void GetAllFriendPairs_GetAllFriendPairsFromDatabase_FriendPairsList()
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

            FriendPair friend1 = new FriendPair(player1.Id, player2.Id);
            friend1.Save();
            FriendPair friend2 = new FriendPair(player1.Id, player3.Id);
            friend2.Save();

            List<FriendPair> result = FriendPair.GetAllFriendPairs();
            List<FriendPair> test = new List<FriendPair>{friend1, friend2};
            CollectionAssert.AreEqual(test, result);
        }
        [TestMethod]
        public void CheckForFriend_ChecksIfFriendsAlready_True()
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

            FriendPair friend1 = new FriendPair(player1.Id, player2.Id);
            friend1.Save();
            FriendPair friend2 = new FriendPair(player1.Id, player3.Id);
            friend2.Save();

            bool? result = FriendPair.CheckForFriend(player1.Id, player2.Id);
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void CheckForFriend_ChecksIfFriendsAlready_False()
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

            FriendPair friend1 = new FriendPair(player1.Id, player2.Id);
            friend1.Save();
            FriendPair friend2 = new FriendPair(player1.Id, player3.Id);
            friend2.Save();

            bool? result = FriendPair.CheckForFriend(player2.Id, player3.Id);
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void CheckForFriend_ChecksIfFriendsAlready_Null()
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

            FriendPair friend1 = new FriendPair(player1.Id, player2.Id);
            friend1.Save();
            FriendPair friend2 = new FriendPair(player1.Id, player3.Id);
            friend2.Save();

            bool? result = FriendPair.CheckForFriend(player2.Id, player2.Id);
            Assert.AreEqual(null, result);
        }
    }
}
