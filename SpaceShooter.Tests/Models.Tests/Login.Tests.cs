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
      Login login1 = new Login("thebigtaco", "password");
      Login login2 = new Login("duhbigotaku", "password");
      Assert.AreNotEqual(login1.Salt, login2.Salt);
    }
  }
}
