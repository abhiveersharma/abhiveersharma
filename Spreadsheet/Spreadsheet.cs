
using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SS
{
    /// <summary>
    /// Author: Joe Zachary, Daniel Kopta, H. James de St. Germain & Abhiveer Sharma
    /// Partner: None
    /// Date of Creation: Februrary 11, 2022
    /// Course: CS 3500, University of Utah, School of Computing
    /// Copyright: CS 3500 and Abhiveer Sharma - This work may not be copied for use in Academic Coursework. 
    /// I, Abhiveer Sharma, certify that I wrote this code from scratch and did not copy it in part or whole from  
    /// another source. All references used in the completion of the assignment are cited in my README file. 
    /// An AbstractSpreadsheet object represents the state of a simple spreadsheet.  A 
    /// spreadsheet consists of an infinite number of named cells.
    /// A string is a valid cell name if and only if:
    ///   (1) its first character is an underscore or a letter
    ///   (2) its remaining characters (if any) are underscores and/or letters and/or digits
    /// Note that this is the same as the definition of valid variable from the PS3 Formula class.
    /// 
    /// For example, "x", "_", "x2", "y_15", and "___" are all valid cell  names, but
    /// "25", "2x", and "&" are not.  Cell names are case sensitive, so "x" and "X" are
    /// different cell names.
    /// 
    /// A spreadsheet contains a cell corresponding to every possible cell name.  (This
    /// means that a spreadsheet contains an infinite number of cells.)  In addition to 
    /// a name, each cell has a contents and a value.  The distinction is important.
    /// 
    /// The contents of a cell can be (1) a string, (2) a double, or (3) a Formula.  If the
    /// contents is an empty string, we say that the cell is empty.  (By analogy, the contents
    /// of a cell in Excel is what is displayed on the editing line when the cell is selected.)
    /// 
    /// In a new spreadsheet, the contents of every cell is the empty string.
    ///  
    /// The value of a cell can be (1) a string, (2) a double, or (3) a FormulaError.  
    /// (By analogy, the value of an Excel cell is what is displayed in that cell's position
    /// in the grid.)
    /// 
    /// If a cell's contents is a string, its value is that string.
    /// 
    /// If a cell's contents is a double, its value is that double.
    /// 
    /// If a cell's contents is a Formula, its value is either a double or a FormulaError,
    /// as reported by the Evaluate method of the Formula class.  The value of a Formula,
    /// of course, can depend on the values of variables.  The value of a variable is the 
    /// value of the spreadsheet cell it names (if that cell's value is a double) or 
    /// is undefined (otherwise).
    /// 
    /// Spreadsheets are never allowed to contain a combination of Formulas that establish
    /// a circular dependency.  A circular dependency exists when a cell depends on itself.
    /// For example, suppose that A1 contains B1*2, B1 contains C1*2, and C1 contains A1*2.
    /// A1 depends on B1, which depends on C1, which depends on A1.  That's a circular
    /// dependency.
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        private Dictionary<string, Cell> cells;
        DependencyGraph dg;
        public Spreadsheet()
        {
            // initialize spreadsheet variables;
            cells = new Dictionary<string, Cell>();
            dg = new DependencyGraph();
        }

        /// <summary>
        /// This is a private nested class used to represent a Cell in this spreadsheet
        /// A cell has contents - string, double and formula
        /// A cell also has values - string, double, double or FormulaError ,and
        /// this functionality will be added in next assignment
        /// </summary>
        private class Cell
        {

            // contents of the cell
           public object contents;
            // value of the cell
            //private object value;

            public Cell(object _contents)
            {
                contents = _contents;
              
               
            }
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.
        /// The return value should be either a string, a double, or a Formula.
        /// </summary>
        public override object GetCellContents(string name)
        {
            
            if(ReferenceEquals(name, null) || !isVariable(name))
            {
                throw new InvalidNameException();
            }
            Cell content;
            if (cells.TryGetValue(name,out content))
            {
                //Cell cell =  cells[name];
                return content.contents;
            }
            return "";
        }
      
        /// <summary>
        /// isVariable method helps in checking the validity of cell name
        /// </summary>
        /// <param name="token"></param> The token to be matched with the regex
        /// <returns></returns> A boolean
        private Boolean isVariable(String token)
        {


            if (Regex.IsMatch(token, @"^[a-zA-Z_][a-zA-Z0-9_]*$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
           
            foreach(string key in cells.Keys)
            {
                if (cells[key].contents.Equals("")){
                    cells.Remove(key); //Remove cells with empty strings

                }
            }
            return cells.Keys;   
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// list consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// SetCellContents("A1", 2.0);
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// list {A1, B1, C1} is returned.
        /// </summary>
        public override IList<string> SetCellContents(string name, double number)
        {
            if (ReferenceEquals(name, null) || !isVariable(name))
            {
                throw new InvalidNameException();
            }
            Cell cell = new Cell(number);
            if (cells.ContainsKey(name))
            {
                cells[name] = cell;
            }
            else
            {
                cells.Add(name, cell);
            }
            dg.ReplaceDependees(name, new HashSet<String>());
            List<string> vs = new List<string>(GetCellsToRecalculate(name));
            return vs;    
        }

        /// <summary>
        /// If text is null, throws an ArgumentNullException.
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// Otherwise, the contents of the named cell becomes text.  The method returns a
        /// list consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// list {A1, B1, C1} is returned.
        /// </summary>
        public override IList<string> SetCellContents(string name, string text)
        {

            if (ReferenceEquals(text, null))
            {
                throw new ArgumentNullException();
            }
            if(ReferenceEquals(name,null) || !isVariable(name))
            {
                throw new InvalidNameException();
            }

            Cell cell = new Cell(text);
            if (cells.ContainsKey(name))
            {
                cells[name] = cell;
            }
            else
            {
                cells.Add(name, cell);
            }
            dg.ReplaceDependees(name, new HashSet<String>());
            List<string> vs = new List<string>(GetCellsToRecalculate(name));
            return vs;
        }

        /// <summary>
        /// If the formula parameter is null, throws an ArgumentNullException.
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException, and no change is made to the spreadsheet.
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// list consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// list {A1, B1, C1} is returned.
        /// </summary>
        public override IList<string> SetCellContents(string name, Formula formula)
        {

            if (ReferenceEquals(formula, null))
            {
                throw new ArgumentNullException();
            }
            if(ReferenceEquals(name,null) || !isVariable(name))
            {
                throw new InvalidNameException();
            }
            List<string> _dependees = new List<string>(dg.GetDependees(name));
            dg.ReplaceDependees(name, formula.GetVariables());
            //Use a try catch block to implement circular dependency
            try {
                Cell cell = new Cell(formula);
                List<string> vs = new List<string>(GetCellsToRecalculate(name));
                if (cells.ContainsKey(name))
                {
                    cells[name] = cell;
                }
                else
                {
                    cells.Add(name, cell);
                }
                return vs;
               
            }
            catch(CircularException) { 
                dg.ReplaceDependees(name, _dependees);
                throw new CircularException();
            }
        }

        /// <summary>
        /// Returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
           return dg.GetDependents(name);
        }
    }
}
