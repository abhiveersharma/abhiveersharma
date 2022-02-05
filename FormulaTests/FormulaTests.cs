using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using System.Collections.Generic;

namespace PS3test
{
    /// Author: Joe Zachary, Daniel Kopta, H. James de St. Germain & Abhiveer Sharma
    /// Partner: None
    /// Date of Creation: Februrary 4, 2022
    /// Course: CS 3500, University of Utah, School of Computing
    /// Copyright: CS 3500 and Abhiveer Sharma - This work may not be copied for use in Academic Coursework. 
    /// I, Abhiveer Sharma, certify that I wrote this code from scratch and did not copy it in part or whole from  
    /// another source. All references used in the completion of the assignment are cited in my README file. 
    [TestClass]
    public class FormulaTests
    {
        //Testing exceptions of the constructor

        [TestMethod(), Timeout(2000)]
        [ExpectedException(typeof(FormulaFormatException))]
        //Testing Ending Token Rule
        public void Test1()
        {
            Formula f = new Formula("-");
        }

        [TestMethod(), Timeout(2000)]
        [ExpectedException(typeof(FormulaFormatException))]
        //Testing Starting Token Rule
        public void Test2()
        {
            Formula f = new Formula("+4");
        }

        [TestMethod(), Timeout(2000)]
        [ExpectedException(typeof(FormulaFormatException))]
        //Testing Specific Token Rule
        public void Test3()
        {
            Formula f = new Formula("#");
        }

        [TestMethod(), Timeout(2000)]
        [ExpectedException(typeof(FormulaFormatException))]
        //Testing Balanced Parentheses Rule
        public void Test4()
        {
            Formula f = new Formula("((5+4)");
        }

        [TestMethod(), Timeout(2000)]
        [ExpectedException(typeof(FormulaFormatException))]
        //Testing Right Parentheses Rule
        public void Test5()
        {
            Formula f = new Formula("(5+3))+4)");
        }

        [TestMethod(), Timeout(2000)]
        [ExpectedException(typeof(FormulaFormatException))]
        //Testing Parentheses/Operator Following Rule
        public void Test6()
        {
            Formula f = new Formula("(+3)+4)");
        }

        [TestMethod(), Timeout(2000)]
        [ExpectedException(typeof(FormulaFormatException))]
        //Testing Extra Following Rule
        public void Test7()
        {
            Formula f = new Formula("4(3)+4)");
        }
        

        //Testing GetVariables method

        [TestMethod(), Timeout(2000)]
        public void Test8()
        {
            Formula f = new Formula("x+y*z", s => s.ToUpper(), s => true);
            HashSet<string> expected = new HashSet<string>();
            expected.Add("X");
            expected.Add("Y");
            expected.Add("Z");
            Assert.IsTrue(expected.SetEquals(f.GetVariables()));

        }

        [TestMethod(), Timeout(2000)]
        public void Test9()
        {
            Formula f = new Formula("x+X*z", s => s.ToUpper(), s => true);
            HashSet<string> expected = new HashSet<string>();
            expected.Add("X");
            expected.Add("Z");
            Assert.IsTrue(expected.SetEquals(f.GetVariables()));

        }

        [TestMethod(), Timeout(2000)]
        public void Test10()
        {
            Formula f = new Formula("x+X*z");
            HashSet<string> expected = new HashSet<string>();
            expected.Add("x");
            expected.Add("X");
            expected.Add("z");
            Assert.IsTrue(expected.SetEquals(f.GetVariables()));

        }

        //Testing Equals method
        [TestMethod(), Timeout(2000)]
        public void Test11()
        {
            Formula f1 = new Formula("x1+y2", s => s.ToUpper(), s => true);
            Formula f2 = new Formula("X1  +  Y2");

            Assert.IsTrue(f1.Equals(f2));
        }

        [TestMethod(), Timeout(2000)]
        public void Test12()
        {
            Formula f1 = new Formula("x1+y2");
            Formula f2 = new Formula("X1+Y2");

            Assert.IsFalse(f1.Equals(f2));

        }

        //Testing the == method

        [TestMethod(), Timeout(2000)]
        public void Test13()
        {
            Formula f1 = new Formula("5");
            Formula f2 = new Formula("5");

            Assert.IsTrue(f1 == f2);

        }

        [TestMethod(), Timeout(2000)]
        public void Test14()
        {
            Formula f1 = new Formula("5.00");
            Formula f2 = new Formula("5");

            Assert.IsTrue(f1 == f2);

        }

        //Testing the != method

        [TestMethod(), Timeout(2000)]
        public void Test15()
        {
            Formula f1 = new Formula("5");
            Formula f2 = new Formula("4");

            Assert.IsTrue(f1 != f2);

        }
        [TestMethod(), Timeout(2000)]
        public void Test16()
        {
            Formula f1 = new Formula("3.00");
            Formula f2 = new Formula("3");

            Assert.IsFalse(f1 != f2);

        }

        //Testing the GetHashCode method

        [TestMethod(), Timeout(2000)]
        public void Test17()
        {
            Formula f1 = new Formula("5+4-3");
            Formula f2 = new Formula("5+4-3");

            Assert.IsTrue(f1.GetHashCode() == f2.GetHashCode());

        }

        [TestMethod(), Timeout(2000)]
        public void Test18()
        {
            Formula f1 = new Formula("5+4-3+6");
            Formula f2 = new Formula("5+4-3");
            Assert.IsTrue(f1.GetHashCode() != f2.GetHashCode());
        }

        //Testing the ToString method

        [TestMethod(), Timeout(2000)]
        public void Test19()
        {
            Formula f = new Formula("x + y", s => s.ToUpper(), s => true);
            string expected = "X+Y";
            Assert.IsTrue(f.ToString().Equals(expected));
        }

        [TestMethod(), Timeout(2000)]
        public void Test20()
        {
            Formula f = new Formula("x + Y");
            string expected = "x+Y";
            Assert.IsTrue(f.ToString().Equals(expected));
        }

        //Testing the Evaluate method

        [TestMethod(), Timeout(2000)]
        public void Test21()
        {
            Formula f = new Formula("X1+X2");
            object result = f.Evaluate(x => 1);
            Assert.AreEqual(2.0, result);
        }

        [TestMethod(), Timeout(2000)]
        public void Test22()
        {
            Formula f = new Formula("5*4/a3");
            object result = f.Evaluate(x => 5);
            Assert.AreEqual(4.0, result);
        }

        [TestMethod(), Timeout(2000)]

        public void Test23()
        {
            Formula f = new Formula("5*4/a3");
            Assert.IsInstanceOfType(f.Evaluate(s => 0), typeof(FormulaError));
        }

        [TestMethod(), Timeout(2000)]
        public void Test24()
        {
            Formula f = new Formula("5-4");
            object result = f.Evaluate(s => 0);
            Assert.AreEqual(1.0, result);
        }

        [TestMethod(), Timeout(2000)]
        public void Test25()
        {
            Formula f = new Formula("(5+4)/3");
            object result = f.Evaluate(s => 0);
            Assert.AreEqual(3.0, result);
        }


    }
}
