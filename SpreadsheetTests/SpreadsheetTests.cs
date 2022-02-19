using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using System;
using System.Collections.Generic;

namespace SS
{
    /// <summary>
    /// Author: H. James de St. Germain, Abhiveer Sharma
    /// Partner: None
    /// Date of Creation: Februrary 18, 2022
    /// Course: CS 3500, University of Utah, School of Computing
    /// Copyright: CS 3500 and Abhiveer Sharma - This work may not be copied for use in Academic Coursework. 
    /// I, Abhiveer Sharma, certify that I wrote this code from scratch and did not copy it in part or whole from  
    /// another source. All references used in the completion of the assignment are cited in my README file. 
    /// I test all the methods like GetCellContents, SetContentsOfCell, GetNamesOfAllNonemptyCells, GetCellValue, Save & GetSavedVersion
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
            sheet.SetContentsOfCell("A4", "5.5");
            Assert.AreEqual(5.5, sheet.GetCellContents("A4"));
        }

        [TestMethod]
        public void Test4()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A4", "5.5");
            sheet.SetContentsOfCell("A4", "6.0");
            Assert.IsFalse((double)sheet.GetCellContents("A4") == 5.5);
        }

        [TestMethod]
        public void Test5()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A4", "5.5");
            sheet.SetContentsOfCell("A4", "6.0");
            Assert.IsTrue((double)sheet.GetCellContents("A4") == 6.0);
        }

        [TestMethod]
        public void Test6()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A4", "a1");
            Assert.AreEqual("a1", sheet.GetCellContents("A4"));
        }

        [TestMethod]
        public void Test7()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A4", "a1");
            sheet.SetContentsOfCell("A4", "a2");
            Assert.AreEqual("a2", sheet.GetCellContents("A4"));
        }

        [TestMethod]
        public void Test8()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            Formula f = new Formula("A2");
            sheet.SetContentsOfCell("A4", "=A2");
            Assert.AreEqual(f, sheet.GetCellContents("A4"));
        }

        [TestMethod]
        public void Test9()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            Formula f = new Formula("a3");
            sheet.SetContentsOfCell("A4", "=a2");
            sheet.SetContentsOfCell("A4", "=a3");
            Assert.AreEqual(f, sheet.GetCellContents("A4"));
        }

        //Testing exceptions of SetCellContents methods

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test10()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "2.5");
            sheet.SetContentsOfCell("4a", "5.5");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test11()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell(null, "5.5");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test12()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A4", (string)null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test13()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A4", "a1");
            sheet.SetContentsOfCell(null, "a2");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test14()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A4", "a1");
            sheet.SetContentsOfCell("4a", "a2");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test15()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.GetCellValue("#A1");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test16()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A4", "=a1");
            sheet.SetContentsOfCell("4A", "=a1");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test17()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell(null, "=x5");
        }

        //Testing GetNamesOfAllNonemptyCells method

        [TestMethod]
        public void Test18()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A4", "a1");
            sheet.SetContentsOfCell("A5", "2.5");
            HashSet<string> expected = new HashSet<string>();
            expected.Add("A4");
            expected.Add("A5");
            Assert.IsTrue(expected.SetEquals(sheet.GetNamesOfAllNonemptyCells())); ;
        }

        [TestMethod]
        public void Test19()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A4", "");
            sheet.SetContentsOfCell("A5", "2.5");
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
            sheet.SetContentsOfCell("A4", "=A2");
            sheet.SetContentsOfCell("A2", "=A4");
        }

        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void Test21()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A4", "=A2");
            sheet.SetContentsOfCell("A5", "2.5");
            sheet.SetContentsOfCell("A2", "=A4");
        }

        //Testing the three argument constructor

        [TestMethod]
        public void Test22()
        {
            AbstractSpreadsheet sheet = new Spreadsheet(s => true, s => s.ToUpper(), "");
            sheet.SetContentsOfCell("A1", "hello");
            Assert.AreEqual("hello", sheet.GetCellContents("a1"));
        }

        [TestMethod]
        public void Test23()
        {
            AbstractSpreadsheet sheet = new Spreadsheet(s => true, s => s.ToLower(), "");
            sheet.SetContentsOfCell("a1", "hello");
            Assert.AreEqual("hello", sheet.GetCellContents("A1"));
        }

        //Testing the Changed property of spreadsheet
        [TestMethod]
        public void Test24()
        {
            AbstractSpreadsheet sheet = new Spreadsheet(s => true, s => s.ToLower(), "");
            Assert.IsFalse(sheet.Changed);
            sheet.SetContentsOfCell("a1", "hello");
            Assert.IsTrue(sheet.Changed);

        }

        [TestMethod]
        public void Test25()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("a1", "hello");
            sheet.Save("sheet1.txt");
            Assert.IsFalse(sheet.Changed);

        }
        //Testing Save and GetSavedVersion
        [TestMethod]
        public void Test26()
        {
            AbstractSpreadsheet sheet = new Spreadsheet(s => true, s => s, "1.0");
            sheet.Save("sheet2.txt");
            Assert.AreEqual("1.0", new Spreadsheet().GetSavedVersion("sheet2.txt"));
        }

        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void Test27()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            ss.Save("sheet3.txt");
            ss = new Spreadsheet("sheet3.txt", s => true, s => s, "version");
        }





    }
}