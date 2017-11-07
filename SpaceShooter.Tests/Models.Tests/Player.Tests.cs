using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceShooter.Models;

namespace SpaceShooter.Models.Tests
{
    [TestClass]
    public class PlayerTests : IDisposable
    {
        public void Dispose()
        {
            DB.ClearAll();
        }
        public PlayerTests()
        {
            DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=8889; database=space_shooter_test;";
        }
        [TestMethod]
        public void MakeSalt_MakeSaltForUser_Salt()
        {
            Assert.AreNotEqual(Player.MakeSalt(), Player.MakeSalt());
        }
        [TestMethod]
        public void MakeSalt_CheckSaltsDifferentPerUser_False()
        {
            string salt1 = Player.MakeSalt();
            string salt2 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Hash hash2 = new Hash("password", salt2);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            Player player2 = new Player("duhbigotaku", hash2.Result, salt2);
            Assert.AreNotEqual(player1.Salt, player2.Salt);
        }
        [TestMethod]
        public void MakeSalt_SameOutcomeForSameUser_SameSalt()
        {
            string salt = Player.MakeSalt();
            Hash hash1 = new Hash("password1", salt);
            Hash hash2 = new Hash("password1", salt);
            Assert.AreEqual(hash1.Result, hash2.Result);
        }
        [TestMethod]
        public void MakeSalt_LengthIsAlways42_42()
        {
            int[] counts = new int[100];
            for (int i = 0; i < counts.Length; i++)
            {
                string salt = Player.MakeSalt();
                counts[i] = salt.Length;
                Assert.AreEqual(42, counts[i]);
            }
        }
        [TestMethod]
        public void Equals_SamePlayer_True()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            Player player2 = new Player("thebigtaco", hash1.Result, salt1);
            Assert.AreEqual(player1, player2);
        }
        [TestMethod]
        public void Equals_NotSamePlayer_True()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            string salt2 = Player.MakeSalt();
            Hash hash2 = new Hash("password_awesome", salt2);
            Player player2 = new Player("lydianlights", hash2.Result, salt2);
            Assert.AreNotEqual(player1, player2);
        }
        [TestMethod]
        public void Save_IncrementsPlayerId_IdChanged()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();

            Assert.AreNotEqual(0, player1.Id);
        }
        [TestMethod]
        public void GetAll_GetsAllPlayersFromDatabase_AllPlayers()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            string salt2 = Player.MakeSalt();
            Hash hash2 = new Hash("password2", salt2);
            Player player2 = new Player("lydianlights", hash2.Result, salt2);
            string salt3 = Player.MakeSalt();
            Hash hash3 = new Hash("drowssap", salt3);
            Player player3 = new Player("myoldone", hash3.Result, salt3);
            player1.Save();
            player2.Save();
            player3.Save();

            List<Player> result = Player.GetAll();
            List<Player> test = new List<Player> {player1, player2, player3};

            CollectionAssert.AreEqual(test, result);
        }
        [TestMethod]
        public void FindById_GetsPlayerWithId_Player()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            string salt2 = Player.MakeSalt();
            Hash hash2 = new Hash("password2", salt2);
            Player player2 = new Player("lydianlights", hash2.Result, salt2);
            string salt3 = Player.MakeSalt();
            Hash hash3 = new Hash("drowssap", salt3);
            Player player3 = new Player("myoldone", hash3.Result, salt3);
            player1.Save();
            player2.Save();
            player3.Save();

            Player result = Player.FindById(player2.Id);
            Player test = player2;

            Assert.AreEqual(test, result);
        }
        [TestMethod]
        public void DoesUsernameExist_UsernameExistsInDatabase_True()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();

            bool result = Player.DoesUsernameExist("thebigtaco");

            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void DoesUsernameExist_UsernameDoesntExistInDatabase_False()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();

            bool result = Player.DoesUsernameExist("thebigtacoooooooooooooooo");

            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void DoesUsernameExist_UsernameExistsButDifferentCaseInDatabase_True()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();

            bool result = Player.DoesUsernameExist("THEBIGTACO");

            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void FindByUsername_GetsPlayerWithUsername_Player()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            string salt2 = Player.MakeSalt();
            Hash hash2 = new Hash("password2", salt2);
            Player player2 = new Player("lydianlights", hash2.Result, salt2);
            string salt3 = Player.MakeSalt();
            Hash hash3 = new Hash("drowssap", salt3);
            Player player3 = new Player("myoldone", hash3.Result, salt3);
            player1.Save();
            player2.Save();
            player3.Save();

            Player result = Player.FindByUsername(player2.Username);
            Player test = player2;

            Assert.AreEqual(test, result);
        }
        [TestMethod]
        public void Login_PlayerEntersCorrectPassword_Session()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();

            Session result = Player.Login("thebigtaco", "password");

            Assert.AreNotEqual(null, result);
        }
        [TestMethod]
        public void Login_PlayerEntersWrongPassword_Null()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();

            Session result = Player.Login("thebigtaco", "passwords");

            Assert.AreEqual(null, result);
        }
        [TestMethod]
        public void Login_PlayerEntersWrongUsername_Null()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();

            Session result = Player.Login("thebigtac0", "password");

            Assert.AreEqual(null, result);
        }
    }
}
