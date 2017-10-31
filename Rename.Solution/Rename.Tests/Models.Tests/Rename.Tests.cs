using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rename.Models;

namespace Rename.Models.Tests
{
  [TestClass]
  public class _classTests : IDisposable
  {
    public void Dispose()
    {
      _class.ClearAll
    }
    [TestMethod]
    public void Method_Description_ExpectedValue()
    {
      Assert.AreEqual(var1, method(input));
    }
  }
}
