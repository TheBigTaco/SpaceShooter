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
      Assert.AreNotEqual(Login.MakeSalt(), Login.MakeSalt());
    }
    [TestMethod]
    public void MakeSalt_CheckSaltsDifferentPerUser_False()
    {
      string salt1 = Login.MakeSalt();
      string salt2 = Login.MakeSalt();
      Hash hash1 = new Hash("password", salt1);
      Hash hash2 = new Hash("password", salt2);
      Login login1 = new Login("thebigtaco", hash1.Result, salt1);
      Login login2 = new Login("duhbigotaku", hash2.Result, salt2);
      Assert.AreNotEqual(login1.Salt, login2.Salt);
    }
  }
}
