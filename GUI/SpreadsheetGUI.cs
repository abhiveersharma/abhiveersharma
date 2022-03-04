using SpreadsheetGrid_Core;
using SS;
using System.Diagnostics;

namespace GUI
{
    public partial class SpreadsheetGUI : Form
    {
        private AbstractSpreadsheet spreadsheet;

        public SpreadsheetGUI()
        {
            InitializeComponent();

            spreadsheet = new Spreadsheet(s => true, s => s, "six");
            this.spreadsheetGrid.SelectionChanged += selectCell;
            cellNameTextBox.Text = "A1";
        }

        private string ConvertColRowToVariable(int col, int row)
        {
            char[] Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            return Alphabet[col] + (row + 1).ToString();
        }
        private void ConvertVariableToColRow(string variable, out int col, out int row)
        {
            char[] Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] variableArr = variable.ToCharArray();
            col = 0;
            foreach (char abc in Alphabet)
            {
                if (variableArr[0] == abc)
                    break;
                col = abc;
            }
            if (variable.Length == 2)
                row = Convert.ToInt32(variableArr[1]) - 1;
            else
                row = Convert.ToInt32(variableArr[1] + variableArr[2]) - 1;
        }


        private void selectCell(SpreadsheetGridWidget sender)
        {
            sender.GetSelection(out int col, out int row);
            string selectedCellName = ConvertColRowToVariable(col, row);
            cellNameTextBox.Text = selectedCellName;

            this.spreadsheetGrid.GetValue(col, row, out string selectedCellValue); //Also display the formula in the main selected cell formula txt box
            object cellValue = this.spreadsheet.GetCellValue(selectedCellName); //Eval cell value in spreadsheet
            cellValueTextBox.Text = Convert.ToString(cellValue); 

            cellContentsTextBox.Text = selectedCellValue; //Also, change contents txt box to show what is in cell

            cellContentsTextBox.Focus();
        }


        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Spreadsheet_Window.getAppContext().RunForm(new SpreadsheetGUI());
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "sprd files (*.sprd)|*.sprd|All files (*.*)|*.*";

                if(saveDialog.ShowDialog() == DialogResult.OK)
                {
                    spreadsheet.Save(saveDialog.FileName);
                }

            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "sprd files (*.sprd)|*.sprd|All files (*.*)|*.*";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    string version = spreadsheet.GetSavedVersion(openDialog.FileName);
                    Spreadsheet sprd = new Spreadsheet(openDialog.FileName, s => true, s => s, version);
                    spreadsheet = new Spreadsheet();
                    
                    foreach(string name in spreadsheet.GetNamesOfAllNonemptyCells())
                    {
                        spreadsheetGrid.SetValue( col,  row, spreadsheet.GetCellValue(name));


                    }
                    //Gets version of file.. should be "six"

                    //spreadsheet
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(spreadsheet.Changed == false)
                Close();
            else
            {
                MessageBox.Show("You have not saved your spreadsheet, are you sure you want to close?", "Unsaved Data!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void evaluateFormulaButton_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void SpreadsheetGUI_KeyPress(object sender, KeyPressEventArgs e)
        {
            

        }
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
                this.spreadsheetGrid.GetSelection(out int col, out int row); //Get location of cell to change (currently selected cell)
                string selectedCellName = ConvertColRowToVariable(col, row); //Convert cell location to cell/variable name

                string caseInsensitive = cellContentsTextBox.Text.ToUpper();
                try
                {
                    this.spreadsheet.SetContentsOfCell(selectedCellName, caseInsensitive); //Sets the contents of the cell to whatever text was entered into txt box
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

            }


            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            Form helpForm = new Form();
            helpForm.Show();
            MessageBox.Show("Welcome to Spreadsheet!\n" +
                "\tFile menu: The file menu in the top right corner allows you to... \n" +
                "\t- open a previously saved spreadsheet\n" +
                "\t- save your current spreadsheet\n" +
                "\t- open any number of new spreadsheets\n" +
                "\t- close the current spreadsheet (to avoid warnings, make sure to save first!)\n\n" +
                "\tCell Operations: \n" +
                "\t- to select a cell, click on the desired cell or user the arrow keys\n" +
                "\t- to edit a cell's contents, click on the Formula textbox and enter a new formula!\n" +
                "\tAdditional Features: \n" +
                "\t- Default help text in formula box" +
                "\t- Ctrl+s saves spreadsheet" + 
                "\t- Enter key evaluates the typed formula" +
                "\t- Mouse over formula textbox for tooltip"
                , "Help Menu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cellContentsTextBox_TextChanged(object sender, EventArgs e)
        {
            this.spreadsheetGrid.GetSelection(out int col, out int row); //Get location of cell to change (currently selected cell)
            string selectedCellName = ConvertColRowToVariable(col, row); //Change cell location to cell/variable name

        }

        private void cellContentsTextBox_Leave(object sender, EventArgs e)
        {
            this.spreadsheetGrid.GetSelection(out int col, out int row); //Get location of cell to change (currently selected cell)
            string selectedCellName = ConvertColRowToVariable(col, row); //Convert cell location to cell/variable name

            object cellValue = this.spreadsheet.GetCellValue(selectedCellName); //Eval cell value in spreadsheet
            cellValueTextBox.Text = Convert.ToString(cellValue); //
        }

        private void evaluateFormulaButton_Click(object sender, EventArgs e)
        {
            this.spreadsheetGrid.GetSelection(out int col, out int row); //Get location of cell to change (currently selected cell)
            string selectedCellName = ConvertColRowToVariable(col, row); //Convert cell location to cell/variable name

            string caseInsensitive = cellContentsTextBox.Text.ToUpper();
            try
            {
                this.spreadsheet.SetContentsOfCell(selectedCellName, caseInsensitive); //Sets the contents of the cell to whatever text was entered into txt box
            }
            catch (FormulaFormatException exception)
            {
                MessageBox.Show(exception.Message, "Invalid Formula!", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }

            object cellValue = this.spreadsheet.GetCellValue(selectedCellName); //Try to evaluate the cell's value in the spreadsheet
            cellValueTextBox.Text = Convert.ToString(cellValue); //Set textbox to show evaluated cell value

            this.spreadsheetGrid.SetValue(col, row, Convert.ToString(cellValue)); //Display the formula in the cell in grid

        }

        private void cellContentsTextBox_MouseMove(object sender, MouseEventArgs e)
        {
            hoverMouseToolTip.SetToolTip(cellContentsTextBox, "Start each formula with '='");
        }
    }
}