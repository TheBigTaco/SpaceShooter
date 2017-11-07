using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceShooter.Models;

namespace SpaceShooter.Models.Tests
{
    [TestClass]
    public class SessionTests : IDisposable
    {
        public void Dispose()
        {
            DB.ClearAll();
        }
        public SessionTests()
        {
            DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=8889; database=space_shooter_test;";
        }
        [TestMethod]
        public void Equals_SamePlayer_True()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();

            Session session1 = new Session(player1.Id);
            Session session2 = new Session(player1.Id);
            Assert.AreEqual(session1, session2);
        }
        [TestMethod]
        public void Equals_NotSamePlayer_False()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();
            string salt2 = Player.MakeSalt();
            Hash hash2 = new Hash("password1", salt2);
            Player player2 = new Player("lydianlights", hash2.Result, salt2);
            player2.Save();

            Session session1 = new Session(player1.Id);
            Session session2 = new Session(player2.Id);
            Assert.AreNotEqual(session1, session2);
        }
        [TestMethod]
        public void Save_CreatesSessionId_NewId()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();
            Session session1 = new Session(player1.Id);
            session1.Save();

            Assert.AreNotEqual("", session1.SessionId);
        }
        [TestMethod]
        public void GetAll_GetsAllSessionsFromDatabase_AllSessions()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();
            string salt2 = Player.MakeSalt();
            Hash hash2 = new Hash("password1", salt2);
            Player player2 = new Player("lydianlights", hash2.Result, salt2);
            player2.Save();
            Session session1 = new Session(player1.Id);
            session1.Save();
            Session session2 = new Session(player2.Id);
            session2.Save();

            List<Session> result = Session.GetAll();
            List<Session> test = new List<Session> {session1, session2};

            CollectionAssert.AreEqual(test, result);
        }
        [TestMethod]
        public void FindById_SessionExists_Session()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();
            string salt2 = Player.MakeSalt();
            Hash hash2 = new Hash("password1", salt2);
            Player player2 = new Player("lydianlights", hash2.Result, salt2);
            player2.Save();
            Session session1 = new Session(player1.Id);
            session1.Save();
            Session session2 = new Session(player2.Id);
            session2.Save();

            Session result = Session.FindById(session2.SessionId);
            Session test = session2;

            Assert.AreEqual(test, result);
        }
        [TestMethod]
        public void FindById_SessionDoesntExist_Session()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();
            string salt2 = Player.MakeSalt();
            Hash hash2 = new Hash("password1", salt2);
            Player player2 = new Player("lydianlights", hash2.Result, salt2);
            player2.Save();
            Session session1 = new Session(player1.Id);
            session1.Save();
            Session session2 = new Session(player2.Id);
            session2.Save();

            Session result = Session.FindById("ldkugheorigjnkdf");
            Session test = null;

            Assert.AreEqual(test, result);
        }
        [TestMethod]
        public void DeleteById_DeletesASession_SessionDeleted()
        {
            string salt1 = Player.MakeSalt();
            Hash hash1 = new Hash("password", salt1);
            Player player1 = new Player("thebigtaco", hash1.Result, salt1);
            player1.Save();
            string salt2 = Player.MakeSalt();
            Hash hash2 = new Hash("password1", salt2);
            Player player2 = new Player("lydianlights", hash2.Result, salt2);
            player2.Save();
            Session session1 = new Session(player1.Id);
            session1.Save();
            Session session2 = new Session(player2.Id);
            session2.Save();
            Session.DeleteById(session2.SessionId);

            Session result = Session.FindById(session2.SessionId);
            Session test = null;

            Assert.AreEqual(test, result);
        }
    }
}
