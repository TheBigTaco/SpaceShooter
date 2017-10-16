using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rename.Models;

namespace Rename.Tests
{
  [TestClass]
  public class RenameTest
  {
    [TestMethod]
    public void Method_Description_ExpectedValue()
    {
      Assert.AreEqual(var1, method(input));
    }
  }
}
