
using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

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

        private bool changed;

        public override bool Changed
        {
            get
            {
                return changed;
            }

            protected set
            {
                changed = value;
            }
        }

        //zero argument constructor
        public Spreadsheet():base(s=>true,s=>s,"default")
        {
            // initialize spreadsheet variables;
            cells = new Dictionary<string, Cell>();
            dg = new DependencyGraph();
            Changed = false;
        }
        //three argument constructor
        public Spreadsheet(Func<string,bool>isValid, Func<string,string>normalize,string version) : base(isValid,normalize,version)
        {
            // initialize spreadsheet variables;
            cells = new Dictionary<string, Cell>();
            dg = new DependencyGraph();
            Changed = false;
        }
        //four argument constructor
        public Spreadsheet(String filePath,Func<string, bool> isValid, Func<string, string> normalize, string version) : base(isValid, normalize, version)
        {
            // initialize spreadsheet variables;
            cells = new Dictionary<string, Cell>();
            dg = new DependencyGraph();
            Changed = false;
        }
        /// <summary>
        /// isVariable method helps in checking the validity of cell name
        /// </summary>
        /// <param name="token"></param> The token to be matched with the regex
        /// <returns></returns> A boolean

        private Boolean isVariable(String token)
        {
            if (Regex.IsMatch(token, @"^[a-zA-Z_][a-zA-Z0-9_]*$") && IsValid(token))
            {
                return true;
            }
            else
            {
                return false;
            }
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
            public object values;

            public Cell(object _contents)
            {
                if(_contents is string)
                {
                    contents = _contents;
                    values = contents;
                   
                }
                if(_contents is double)
                {
                    contents = _contents;
                    values = contents;
                }
             
            }

            public Cell (object _contents, Func<string, double> lookup)
            {
                if (_contents is Formula)
                {
                    contents = _contents;
                    Formula value = (Formula)contents;
                    values = value.Evaluate(lookup);
                }
            }

            /// <summary>
            ///     Helper method for re-evaluating formulas when their dependees 
            ///     are changed
            ///     This method should only be used on cells that have a Formula as
            ///     their contents. 
            /// </summary>
            /// <param name="lookup">Lookup delegate for value</param>
            public void ReEvaluate(Func<string, double> lookup)
            {
                if (contents is Formula)
                {
                    Formula reEvaluate = (Formula)contents;
                    values = reEvaluate.Evaluate(lookup);
                }
            }


        }

        /// <summary>
        /// Helper method for evaluating functions by finding values
        /// associated with a cell
        /// </summary>
        /// <param name="name">The cell name to be evaluated </param>
        /// <returns></returns>
        private double lookupValue(String name)
        {
            Object cellValue = GetCellValue(name);
            if (cellValue is Double)
            {
                return (double)cellValue;
            }
            else
            {
                throw new ArgumentException("error");
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
            Cell cell;
            name = Normalize(name);
            if (cells.TryGetValue(name,out cell))
            {
                return cell.contents;
            }
            return "";
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
        protected override IList<string> SetCellContents(string name, double number)
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
        protected override IList<string> SetCellContents(string name, string text)
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
        protected override IList<string> SetCellContents(string name, Formula formula)
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
                Cell cell = new Cell(formula,lookupValue);
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
        /// <summary>
        ///   <para>Sets the contents of the named cell to the appropriate value. </para>
        ///   <para>
        ///       First, if the content parses as a double, the contents of the named
        ///       cell becomes that double.
        ///   </para>
        ///
        ///   <para>
        ///       Otherwise, if content begins with the character '=', an attempt is made
        ///       to parse the remainder of content into a Formula.  
        ///       There are then three possible outcomes:
        ///   </para>
        ///
        ///   <list type="number">
        ///       <item>
        ///           If the remainder of content cannot be parsed into a Formula, a 
        ///           SpreadsheetUtilities.FormulaFormatException is thrown.
        ///       </item>
        /// 
        ///       <item>
        ///           If changing the contents of the named cell to be f
        ///           would cause a circular dependency, a CircularException is thrown,
        ///           and no change is made to the spreadsheet.
        ///       </item>
        ///
        ///       <item>
        ///           Otherwise, the contents of the named cell becomes f.
        ///       </item>
        ///   </list>
        ///
        ///   <para>
        ///       Finally, if the content is a string that is not a double and does not
        ///       begin with an "=" (equal sign), save the content as a string.
        ///   </para>
        /// </summary>
        ///
        /// <exception cref="InvalidNameException"> 
        ///   If the name parameter is null or invalid, throw an InvalidNameException
        /// </exception>
        /// 
        /// <exception cref="SpreadsheetUtilities.FormulaFormatException"> 
        ///   If the content is "=XYZ" where XYZ is an invalid formula, throw a FormulaFormatException.
        /// </exception>
        /// 
        /// <exception cref="CircularException"> 
        ///   If changing the contents of the named cell to be the formula would 
        ///   cause a circular dependency, throw a CircularException.  
        ///   (NOTE: No change is made to the spreadsheet.)
        /// </exception>
        /// 
        /// <param name="name"> The cell name that is being changed</param>
        /// <param name="content"> The new content of the cell</param>
        /// 
        /// <returns>
        ///       <para>
        ///           This method returns a list consisting of the passed in cell name,
        ///           followed by the names of all other cells whose value depends, directly
        ///           or indirectly, on the named cell. The order of the list MUST BE any
        ///           order such that if cells are re-evaluated in that order, their dependencies 
        ///           are satisfied by the time they are evaluated.
        ///       </para>
        ///
        ///       <para>
        ///           For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        ///           list {A1, B1, C1} is returned.  If the cells are then evaluate din the order:
        ///           A1, then B1, then C1, the integrity of the Spreadsheet is maintained.
        ///       </para>
        /// </returns>
        public override IList<string> SetContentsOfCell(string name, string content)
        {
            if (ReferenceEquals(content, null))
            {
                throw new ArgumentNullException();
            }
            if (ReferenceEquals(name, null) || !(isVariable(name)))
            {
                throw new InvalidNameException();
            }
            List<String> calculatedCells;
            double value;
            if (content.Equals(""))
            {
                calculatedCells = new List<string>(SetCellContents(name, content));
            }
            else if (Double.TryParse(content, out value))
            {
                calculatedCells = new List<string>(SetCellContents(name, value));

            }
            else if (content.Substring(0, 1).Equals("="))
            {
                Formula f = new Formula(content.Substring(1, content.Length - 1), Normalize, IsValid);
                calculatedCells = new List<string>(SetCellContents(name, f));

            }
            else
            {
                calculatedCells = new List<string>(SetCellContents(name, content));
            }

            Changed = true;

            foreach (string token in calculatedCells)
            {
                Cell cellValue;

                if (cells.TryGetValue(token, out cellValue))
                    cellValue.ReEvaluate(lookupValue);
            }

            return calculatedCells;
        }


        /// <summary>
        ///   Look up the version information in the given file. If there are any problems opening, reading, 
        ///   or closing the file, the method should throw a SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        /// 
        /// <remarks>
        ///   In an ideal world, this method would be marked static as it does not rely on an existing SpreadSheet
        ///   object to work; indeed it should simply open a file, lookup the version, and return it.  Because
        ///   C# does not support this syntax, we abused the system and simply create a "regular" method to
        ///   be implemented by the base class.
        /// </remarks>
        /// 
        /// <exception cref="SpreadsheetReadWriteException"> 
        ///   Thrown if any problem occurs while reading the file or looking up the version information.
        /// </exception>
        /// 
        /// <param name="filename"> The name of the file (including path, if necessary)</param>
        /// <returns>Returns the version information of the spreadsheet saved in the named file.</returns>

        public override string GetSavedVersion(string filename)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Writes the contents of this spreadsheet to the named file using an XML format.
        /// The XML elements should be structured as follows:
        /// 
        /// <spreadsheet version="version information goes here">
        /// 
        /// <cell>
        /// <name>cell name goes here</name>
        /// <contents>cell contents goes here</contents>    
        /// </cell>
        /// 
        /// </spreadsheet>
        /// 
        /// There should be one cell element for each non-empty cell in the spreadsheet.  
        /// If the cell contains a string, it should be written as the contents.  
        /// If the cell contains a double d, d.ToString() should be written as the contents.  
        /// If the cell contains a Formula f, f.ToString() with "=" prepended should be written as the contents.
        /// 
        /// If there are any problems opening, writing, or closing the file, the method should throw a
        /// SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        public override void Save(string filename)
        {
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "  ";
                using (XmlWriter writer = XmlWriter.Create(filename, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("spreadsheet");
                    //write all the elements => cell name, contents and spreadsheet version
                    writer.WriteAttributeString("version", Version);

                    foreach (string name in cells.Keys)
                    {
                        writer.WriteStartElement("cell");
                        writer.WriteElementString("name", name);
                        if (GetCellContents(name) is string)
                        {
                            string content = (string)GetCellContents(name);
                            writer.WriteElementString("contents", content);
                        }
                        if (GetCellContents(name) is double)
                        {
                            double content = (double)GetCellContents(name);
                            writer.WriteElementString("contents", content.ToString());
                        }
                        if (GetCellContents(name) is Formula)
                        {
                            Formula content = (Formula)GetCellContents(name);
                            writer.WriteElementString("contents", "=" + content.ToString());
                        }
                        writer.WriteEndElement(); //this closes the cell tag
                    }
                    //writer.WriteElementString("name","cellname");
                    // writer.WriteElementString("contents", "cell.contents");

                    writer.WriteEndElement(); // this closes the spreadsheet tag

                    writer.WriteEndDocument();
                }
            }
                

            catch (XmlException e)
            {
                throw new SpreadsheetReadWriteException(e.ToString());
            }
            catch (IOException e)
            {
                throw new SpreadsheetReadWriteException(e.ToString());
            }

        }

        /// <summary>
        /// If name is invalid, throws an InvalidNameException.
        /// </summary>
        ///
        /// <exception cref="InvalidNameException"> 
        ///   If the name is invalid, throw an InvalidNameException
        /// </exception>
        /// 
        /// <param name="name"> The name of the cell that we want the value of (will be normalized)</param>
        /// 
        /// <returns>
        ///   Returns the value (as opposed to the contents) of the named cell.  The return
        ///   value should be either a string, a double, or a SpreadsheetUtilities.FormulaError.
        /// </returns>
        public override object GetCellValue(string name)
        {
            if (!isVariable(name))
            {
                throw new InvalidNameException();
            }


            Cell cell;

            if (cells.TryGetValue(name, out cell))
            {

                return cell.values;
            }
            else
            {
                return "";
            }

        }

       

        //doubts about PS5
        //When a program creates a new Spreadsheet object, your constructor should use the provided IsValid
        //delegate parameter before making calls to such functions as: GetCellContents;


    }
}
