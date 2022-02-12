using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using System;
using System.Collections.Generic;

namespace SS
{
    /// <summary>
    /// Author: H. James de St. Germain, Abhiveer Sharma
    /// Partner: None
    /// Date of Creation: Februrary 11, 2022
    /// Course: CS 3500, University of Utah, School of Computing
    /// Copyright: CS 3500 and Abhiveer Sharma - This work may not be copied for use in Academic Coursework. 
    /// I, Abhiveer Sharma, certify that I wrote this code from scratch and did not copy it in part or whole from  
    /// another source. All references used in the completion of the assignment are cited in my README file. 
    /// I test all the methods like GetCellContents, SetCellContents, GetNamesOfAllNonemptyCells
    /// This is a test class for Spreadsheet class  and
    /// is intendedto contain all SpreadsheetTest Unit Tests
    /// </summary>
    /// 
    [TestClass]
    public class SpreadsheetTests
    {
        //Testing exceptions of GetCellContents method
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test1()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.GetCellContents(null);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.GetCellContents("5a");
        }

        //Testing GetCellContents and SetCellContents methods

        [TestMethod]
        public void Test3()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A4", 5.5);
            Assert.AreEqual(5.5, sheet.GetCellContents("A4"));
        }

        [TestMethod]
        public void Test4()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A4", 5.5);
            sheet.SetCellContents("A4", 6.0);
            Assert.IsFalse((double)sheet.GetCellContents("A4") == 5.5);
        }

        [TestMethod]
        public void Test5()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A4", 5.5);
            sheet.SetCellContents("A4", 6.0);
            Assert.IsTrue((double)sheet.GetCellContents("A4") == 6.0);
        }

        [TestMethod]
        public void Test6()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A4", "a1");
            Assert.AreEqual("a1", sheet.GetCellContents("A4"));
        }

        [TestMethod]
        public void Test7()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A4", "a1");
            sheet.SetCellContents("A4", "a2");
            Assert.AreEqual("a2", sheet.GetCellContents("A4"));
        }

        [TestMethod]
        public void Test8()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            Formula f = new Formula("a2");
            sheet.SetCellContents("A4", f);
            Assert.AreEqual(f, sheet.GetCellContents("A4"));
        }

        [TestMethod]
        public void Test9()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            Formula f1 = new Formula("a2");
            Formula f2 = new Formula("a3");
            sheet.SetCellContents("A4", f1);
            sheet.SetCellContents("A4", f2);
            Assert.AreEqual(f2, sheet.GetCellContents("A4"));
        }

        //Testing exceptions of SetCellContents methods

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test10()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A1", 2.5);
            sheet.SetCellContents("4a", 5.5);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test11()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents(null, 5.5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test12()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A4", (string)null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test13()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A4", "a1");
            sheet.SetCellContents(null, "a2");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test14()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A4", "a1");
            sheet.SetCellContents("4a", "a2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test15()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A4", (Formula)null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test16()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            Formula f = new Formula("x5");
            sheet.SetCellContents("A4", f);
            sheet.SetCellContents("4A", f);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test17()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            Formula f = new Formula("x5");
            sheet.SetCellContents(null, f);
        }

        //Testing GetNamesOfAllNonemptyCells method

        [TestMethod]
        public void Test18()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A4", "a1");
            sheet.SetCellContents("A5", 2.5);
            HashSet<string> expected = new HashSet<string>();
            expected.Add("A4");
            expected.Add("A5");
            Assert.IsTrue(expected.SetEquals(sheet.GetNamesOfAllNonemptyCells())); ;
        }

        [TestMethod]
        public void Test19()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A4", "");
            sheet.SetCellContents("A5", 2.5);
            HashSet<string> expected = new HashSet<string>();
            expected.Add("A4");
            expected.Add("A5");
            Assert.IsFalse(expected.SetEquals(sheet.GetNamesOfAllNonemptyCells())); ;
        }

        //Testing Circular Exceptions

        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void Test20()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            Formula f1 = new Formula("A2");
            Formula f2 = new Formula("A4");
            sheet.SetCellContents("A4", f1);
            sheet.SetCellContents("A2", f2);
        }

        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void Test21()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            Formula f1 = new Formula("A2");
            Formula f2 = new Formula("A4+5");
            sheet.SetCellContents("A4", f1);
            sheet.SetCellContents("A5", 2.5);
            sheet.SetCellContents("A2", f2);
        }


    
    }
}