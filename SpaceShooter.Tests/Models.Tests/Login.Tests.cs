using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceShooter.Models;

namespace SpaceShooter.Models.Tests
{
  [TestClass]
  public class LoginTests : IDisposable
  {
    public void Dispose()
    {
      DB.ClearAll();
    }
    public LoginTests()
    {
      DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=8889; database=space_shooter_test;";
    }
    [TestMethod]
    public void MakeSalt_MakeSaltForUser_Salt()
    {
      Assert.AreNotEqual(0, Login.MakeSalt());
    }
    [TestMethod]
    public void MakeSalt_CheckSaltsDifferentPerUser_False()
    {
      string salt1 = Login.MakeSalt();
      string salt2 = Login.MakeSalt();
      string hash1 = Login.HashAlgorithm("password", salt1);
      string hash2 = Login.HashAlgorithm("password", salt2);
      Login login1 = new Login("thebigtaco", hash1, salt1);
      Login login2 = new Login("duhbigotaku", hash2, salt2);
      Assert.AreNotEqual(login1.Salt, login2.Salt);
    }
    [TestMethod]
    public void HashAlgorithm_HashPassword_String()
    {
      string salt = Login.MakeSalt();
      string result = Login.HashAlgorithm("password", salt);
      Assert.AreNotEqual(0, result);
    }
  }
}
