using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SS
{
    [TestClass]
    public class SpreadsheetTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            IList<string> contents = spreadsheet.SetCellContents("A1", 2.0);
          

            Assert.AreEqual(2.0, spreadsheet.GetCellContents("A1"));
        }
    }
}