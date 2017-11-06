using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceShooter.Models;

namespace SpaceShooter.Models.Tests
{
    [TestClass]
    public class HashTests : IDisposable
    {
        public void Dispose()
        {
            DB.ClearAll();
        }
        public HashTests()
        {
            DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=8889; database=space_shooter_test;";
        }
        [TestMethod]
        public void Result_LengthIsAlways42_42()
        {
            int[] counts = new int[100];
            for (int i = 0; i < counts.Length; i++)
            {
                string salt = Player.MakeSalt();
                string hash = new Hash("password1", salt).Result;
                counts[i] = hash.Length;
                Assert.AreEqual(42, counts[i]);
            }
        }
    }
}
