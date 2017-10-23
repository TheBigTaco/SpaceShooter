using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rename.Models;

namespace Rename.Models.Tests
{
  [TestClass]
  public class ClassTests : IDisposable
  {
    public void Dispose()
    {
      Class.ClearAll
    }
    [TestMethod]
    public void Method_Description_ExpectedValue()
    {
      Assert.AreEqual(var1, method(input));
    }
  }
}
