using SpreadsheetGrid_Core;
using SS;
using System.Diagnostics;

namespace GUI
{
    /// <summary>
    /// Author: Joe Zachary, Daniel Kopta, H. James de St. Germain, Abhiveer Sharma, & Greyson Mitra
    /// Partner: Abhiveer Sharma & Greyson Mitra
    /// Date of Creation: February 20, 2022
    /// Course: CS 3500, University of Utah, School of Computing
    /// Copyright: CS 3500 and Abhiveer Sharma - This work may not be copied for use in Academic Coursework. 
    /// I, Abhiveer Sharma, and I, Greyson Mitra, certify that we wrote this code from scratch and did not copy it in part or whole from  
    /// another source. All references used in the completion of the assignment are cited in my README file.
    /// 
    /// This partial GUI class represents a Windows Form window that contains our Spreadsheet with its accompanying GUI. 
    /// It has typical functions of a GUI like opening new files, saving the current file, opening saved spreadsheets, and closing the current window.
    /// The spreadsheet has error messages for invalid formulas and closing without saving. It has the typical cells and cell functionality. This class consists of 
    /// an initialized form that has spreadsheet object tied to it and it also has a lot of event handlers and such to handle the user interacting with the GUI.
    /// </summary>
    public partial class SpreadsheetGUI : Form
    {
        private AbstractSpreadsheet spreadsheet;

        /// <summary>
        /// Initializes the GUI with all of the designer buttons, textboxes, and other components. Also creates a spreadsheet
        /// object that backs the GUI. Sets an default selected cell as well as per the assignment instructions.
        /// </summary>
        public SpreadsheetGUI()
        {
            InitializeComponent();

            spreadsheet = new Spreadsheet(s => true, s => s, "six");
            this.spreadsheetGrid.SelectionChanged += selectCell;
            cellNameTextBox.Text = "A1";
        }

        /// <summary>
        /// Simple helper method to convert an column number and row number into a cell name that uses letters and numbers
        /// 
        /// Example: col = 0 and row = 0 ---> cell name is "A1" 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns>String representing a cell's name. For example, "A1" or "C55".</returns>
        private string ConvertColRowToVariable(int col, int row)
        {
            char[] Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            return Alphabet[col] + (row + 1).ToString();
        }

        /// <summary>
        /// Converts a cell name to two numbers that represent the column number and row number.
        /// Outputs these values as "out" parameters.
        /// Example:  cell name is "A1" ---> col = 0 and row = 0 
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        private void ConvertVariableToColRow(string variable, out int col, out int row)
        {
            char[] Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] variableArr = variable.ToCharArray();
            col = 0;
            foreach (char abc in Alphabet) //Check if any of the uppercase alphabet letters match the first char of the variable
            {
                if (variableArr[0] == abc)
                    break;
                col = variable[0] - 'A';
            }
            if (variable.Length == 2) //2 cases for variable/cell names of 2 or 3 length. Ex: C2 or C99
                int.TryParse(variable.Substring(1), out row);
            else
                int.TryParse(variable.Substring(1,2), out row);
            
            row = row - 1;
        }

        /// <summary>
        /// Delegate for SpreadsheetGrid to change the selected cell. Gets the selected cell's name and displays in multiple places in the GUI
        /// Also, makes sure to display the cell's value and contents as well when selected. 
        /// </summary>
        /// <param name="sender"></param>
        private void selectCell(SpreadsheetGridWidget sender)
        {
            sender.GetSelection(out int col, out int row);
            string selectedCellName = ConvertColRowToVariable(col, row);
            cellNameTextBox.Text = selectedCellName; //Display currently selected cell at top of window.

            this.spreadsheetGrid.GetValue(col, row, out string selectedCellValue); //Also display the formula in the main selected cell formula txt box
            object cellValue = this.spreadsheet.GetCellValue(selectedCellName); //Eval cell value in spreadsheet
            cellValueTextBox.Text = selectedCellValue; //Convert.ToString(cellValue); //Then display the value at top

            cellContentsTextBox.Text = "="+spreadsheet.GetCellContents(selectedCellName).ToString(); //Also, change contents txt box to show what is in cell

            cellContentsTextBox.Focus(); //Focus textbox for easy typing when you've just selected a cell
        }

        /// <summary>
        /// Runs a new instance of the GUI in a new form/window when the "new" menu option is clicked or keyboard shortcut-ted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Spreadsheet_Window.getAppContext().RunForm(new SpreadsheetGUI());
        }

        /// <summary>
        /// Saves the current spreadsheet using XML saving implemented in spreadsheet object. Uses windows save file dialog to make it simple to navigate 
        /// what you want to name the file, and where to save it. Can view either .sprd files only or all files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "sprd files (*.sprd)|*.sprd|All files (*.*)|*.*";

                if(saveDialog.ShowDialog() == DialogResult.OK)
                {
                    spreadsheet.Save(saveDialog.FileName); //works!
                }
            }
        }

        /// <summary>
        /// Opens a spreadsheet that has been previously saved. Allows user to choose which through file dialog. Also provides safety error popup if spreadsheet isn't saved.
        /// See openFileHelper()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (spreadsheet.Changed == true)
            {
                DialogResult = MessageBox.Show("You have not saved your spreadsheet, are you sure you want to close?\n\nClick Ok to close anyway", "Unsaved Data!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (DialogResult == DialogResult.OK) //Let the user open if they want to erase unsaved data
                    openFileHelper();
            }
            else
            {
                openFileHelper();
            }
        }

        /// <summary>
        /// Uses file dialog to filter only .sprd files or all files so user can easily choose which to open. Also, clears old spreadsheet and then 
        /// loads new spreadsheet cells 
        /// </summary>
        private void openFileHelper()
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "sprd files (*.sprd)|*.sprd|All files (*.*)|*.*";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    string version = spreadsheet.GetSavedVersion(openDialog.FileName);
                    Spreadsheet sprd = new Spreadsheet(openDialog.FileName, s => true, s => s, version);
                    this.spreadsheetGrid.Clear(); // clear the old contents from the spreadsheet                    
                    foreach (string name in sprd.GetNamesOfAllNonemptyCells())
                    {
                        ConvertVariableToColRow(name, out int col, out int row);
                        this.spreadsheetGrid.SetValue(col, row, sprd.GetCellValue(name).ToString()); //Set the values of the cells to show in the grid
                        this.spreadsheet.SetContentsOfCell(name, sprd.GetCellContents(name).ToString()); //Also put the cell contents into our current spreadsheet so no cell dependencies break upon loading
                    }
                }
            }
        }

        /// <summary>
        /// Attempts to close spreadsheet. Triggers SpreadsheetGUI_FormClosing event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close(); //Closing handled by FormClosing function below
        }

        /// <summary>
        /// When the spreadsheet is going to be closed by window close X or by close file menu option, then either provides warning for unsaved data
        /// or closes window. Also allows user to close anyway.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadsheetGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (spreadsheet.Changed == false)
                e.Cancel = false;
            else
            {   //Save the result of what the user clicked on to DialogResult
                DialogResult = MessageBox.Show("You have not saved your spreadsheet, are you sure you want to close?\n\nClick Ok to close anyway", "Unsaved Data!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (DialogResult == DialogResult.OK)
                    e.Cancel = false;
                else
                    e.Cancel = true; //Cancels the closing of the form so it stays open for saving
            }
        }

        /// <summary>
        /// Event handler for any keys getting pressed. Needed to make ProcessCmdKey work for keyboard navigation of spreadsheet GUI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadsheetGUI_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        
        /// <summary>
        /// processes key presses using keydata. Currently allows movement around cells using arrow keys or tabs. Also uses Enter key to evaluate formulas.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Right)
            {
                spreadsheetGrid.GetSelection(out int col, out int row);
                spreadsheetGrid.SetSelection(col + 1, row);
            }
            if (keyData == Keys.Left)
            {
                spreadsheetGrid.GetSelection(out int col, out int row);
                spreadsheetGrid.SetSelection(col - 1, row);
            }
            if (keyData == Keys.Up)
            {
                spreadsheetGrid.GetSelection(out int col, out int row);
                spreadsheetGrid.SetSelection(col, row-1);
            }
            if (keyData == Keys.Down)
            {
                spreadsheetGrid.GetSelection(out int col, out int row);
                spreadsheetGrid.SetSelection(col, row + 1);
            }
            if (keyData == Keys.Tab)
            {
                spreadsheetGrid.GetSelection(out int col, out int row);
                spreadsheetGrid.SetSelection(col + 1, row);
            }

            if (keyData == Keys.Enter)
            {
                evaluateFormulaHelper();   
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Event Handler to provide new help window/Form for when user presses help button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpButton_Click(object sender, EventArgs e)
        {
            Form helpForm = new HelpFormGUI();
            helpForm.Show();
        }

        /// <summary>
        /// When the textbox is 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cellContentsTextBox_TextChanged(object sender, EventArgs e)
        {
            this.spreadsheetGrid.GetSelection(out int col, out int row); //Get location of cell to change (currently selected cell)
            string selectedCellName = ConvertColRowToVariable(col, row); //Change cell location to cell/variable name
        }

        /// <summary>
        /// When the user clicks or navigates out of the formula/cell contents textbox, then the cell's value is updated in the cell value textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cellContentsTextBox_Leave(object sender, EventArgs e)
        {
            this.spreadsheetGrid.GetSelection(out int col, out int row); //Get location of cell to change (currently selected cell)
            string selectedCellName = ConvertColRowToVariable(col, row); //Convert cell location to cell/variable name
            this.spreadsheetGrid.GetValue(col, row, out string selectedCellValue); //Also display the formula in the main selected cell formula txt box
            cellValueTextBox.Text = selectedCellValue;  
        }

        /// <summary>
        /// Event handler for when evaluate button is pressed. See evaluateFormulaHelper()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void evaluateFormulaButton_Click(object sender, EventArgs e)
        {
            evaluateFormulaHelper();  
        }

        /// <summary>
        /// Evaluates the formula when the user presses enter or clicks the button in the GUI. Any cell names in the formula are converted to upper case, 
        /// any exceptions are caught, and the spreadsheet is then updated to show the correct values and cell contents. Also takes care of cell dependencies. 
        /// If a dependee cell is changed, all the cell's with contents/formulas that depend on that cell are also updated. 
        /// 
        /// <para> Lastly, if there are many cells to update when a dependee is changed, then let a background worker do that work in another thread so the GUI thread
        /// doesn't freeze up for the user.</para>
        /// </summary>
        /// <exception cref="FormulaFormatException"></exception>
        /// <exception cref="CircularException"></exception>
        private void evaluateFormulaHelper()
        {
            this.spreadsheetGrid.GetSelection(out int col, out int row); //Get location of cell to change (currently selected cell)
            string selectedCellName = ConvertColRowToVariable(col, row); //Convert cell location to cell/variable name

            string caseInsensitive = cellContentsTextBox.Text.ToUpper();
            try
            {
                IList<string> listOfDeps = this.spreadsheet.SetContentsOfCell(selectedCellName, caseInsensitive); //Sets the contents of the cell to whatever text was entered into txt box
                
                if(listOfDeps.Count > 50) //If there is a lot of recalculation to do, then do it in the background worker
                    longCalcBGWorker.RunWorkerAsync(listOfDeps);
                else //if not a lot, just do it in the main thread
                {
                    foreach (string dependent in listOfDeps) //Change all the cells that depend on the changing cell so that they all update
                    {
                        this.spreadsheet.GetCellValue(dependent);
                        ConvertVariableToColRow(dependent, out col, out row);
                        object depValue = this.spreadsheet.GetCellValue(dependent);
                        this.spreadsheetGrid.SetValue(col, row, Convert.ToString(depValue));
                    }
                }
            }
            catch (FormulaFormatException exception)
            {
                MessageBox.Show(exception.Message, "Invalid Formula!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CircularException circE)
            {
                MessageBox.Show("Cannot reference the current cell inside of itself!", "Invalid Formula!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            object cellValue = this.spreadsheet.GetCellValue(selectedCellName); //Try to evaluate the cell's value in the spreadsheet
            cellValueTextBox.Text = Convert.ToString(cellValue); //Set textbox to show evaluated cell value

            this.spreadsheetGrid.SetValue(col, row, Convert.ToString(cellValue)); //Display the formula in the cell in grid
                                                                                  // Call the background worker with arguments:
        }

        /// <summary>
        /// Event handler for when user mouses over the formula/cell contents textbox, it provides a popup tooltip to remind the user to write formulas with = at the beginning!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cellContentsTextBox_MouseMove(object sender, MouseEventArgs e)
        {
            hoverMouseToolTip.SetToolTip(cellContentsTextBox, "Start each formula with '='");
        }

        /// <summary>
        /// Background worker to update cell values when a dependee cell changes and there are many dependent cells. Does so in separate thread from GUI
        /// for user convenience.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void longCalcBGWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            foreach (string dependent in (IList<string>)e.Argument) //Change all the cells that depend on the changing cell so that they all update
            {
                this.spreadsheet.GetCellValue(dependent);
                ConvertVariableToColRow(dependent, out int col, out int row);
                object depValue = this.spreadsheet.GetCellValue(dependent);
                this.spreadsheetGrid.SetValue(col, row, Convert.ToString(depValue));
            }
        }
     //End of SpreadsheetGUI Form class
    }

    /// <summary>
    /// Custom "helper" form to use for help menu. Has its own components and disposes of itself.
    /// </summary>
    public class HelpFormGUI : Form
    {
        private System.ComponentModel.IContainer components = null;
        private Label helpMessageLabel;

        /// <summary>
        /// Makes sure form resources are disposed of properly
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Sets up components of help menu which is just a label with text to explain the spreadsheet features. Sets sizes and other positioning, and text data as well.
        /// </summary>
        public HelpFormGUI()
        {
            this.components = new System.ComponentModel.Container();
            this.helpMessageLabel = new Label();
            this.SuspendLayout();

            this.helpMessageLabel.AutoSize = true;
            this.helpMessageLabel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            this.helpMessageLabel.Location = new Point(0, 0);
            this.helpMessageLabel.Text = "Welcome to Spreadsheet!\n" +
                "\tFile menu: The file menu in the top left corner allows you to... \n" +
                "\t- open a previously saved spreadsheet\n" +
                "\t- save your current spreadsheet\n" +
                "\t- open any number of new spreadsheets\n" +
                "\t- close the current spreadsheet (to avoid warnings, make sure to save first!)\n\n" +
                "\tCell Operations: \n" +
                "\t- to select a cell, click on the desired cell or use the arrow or tab keys\n" +
                "\t- to edit a cell's contents, click on the Formula textbox and enter a new formula, then click the evaluate button or press Enter!\n\n" +
                "\tAdditional Features: \n" +
                "\t- Default help text in formula box\n" +
                "\t- Keyboard shortcuts for file menu options (see file menu)\n" +
                "\t- Enter key evaluates the typed formula\n" +
                "\t- Mouse over formula textbox for tooltip\n" +
                "\t- Custom Form for help menu!\n";

            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(500, 300);
            this.Text = "Help Window";
            this.Controls.Add(this.helpMessageLabel);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}