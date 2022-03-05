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

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private string ConvertColRowToVariable(int col, int row)
        {
            char[] Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            return Alphabet[col] + (row + 1).ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        private void ConvertVariableToColRow(string variable, out int col, out int row)
        {
            char[] Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] variableArr = variable.ToCharArray();
            col = 0;
            foreach (char abc in Alphabet)
            {
                if (variableArr[0] == abc)
                    break;
                col = variable[0] - 'A';
            }
            if (variable.Length == 2)
                int.TryParse(variable.Substring(1), out row);
            else
                int.TryParse(variable.Substring(1,2), out row);
            
            row = row - 1;
        }


        private void selectCell(SpreadsheetGridWidget sender)
        {
            sender.GetSelection(out int col, out int row);
            string selectedCellName = ConvertColRowToVariable(col, row);
            cellNameTextBox.Text = selectedCellName;

            this.spreadsheetGrid.GetValue(col, row, out string selectedCellValue); //Also display the formula in the main selected cell formula txt box
            object cellValue = this.spreadsheet.GetCellValue(selectedCellName); //Eval cell value in spreadsheet
            cellValueTextBox.Text = Convert.ToString(cellValue);

            cellContentsTextBox.Text = "="+spreadsheet.GetCellContents(selectedCellName).ToString(); //selectedCellValue; //Also, change contents txt box to show what is in cell

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
                    spreadsheet.Save(saveDialog.FileName); //works!
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
                    spreadsheet = new Spreadsheet(); //Blank spreadsheet 
                    
                    foreach(string name in sprd.GetNamesOfAllNonemptyCells())
                    {
                        ConvertVariableToColRow(name, out int col, out int row);
                        this.spreadsheetGrid.SetValue(col, row, sprd.GetCellValue(name).ToString());

                    }
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close(); //Closing handled by FormClosing function below
        }

        private void SpreadsheetGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (spreadsheet.Changed == false)
                e.Cancel = false;
            else
            {   //Save the result of what the user clicked on to DialogResult
                DialogResult = MessageBox.Show("You have not saved your spreadsheet, are you sure you want to close?\n\nClick Ok to close anyway", "Unsaved Data!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                if (DialogResult == DialogResult.OK)
                    e.Cancel = false;
                else
                    e.Cancel = true; //Cancels the closing of the form so it stays open for saving
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
            Form helpForm = new HelpFormGUI();
            helpForm.Show();
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

        private void longCalcBGWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }
    }

    public class HelpFormGUI : Form
    {
        private System.ComponentModel.IContainer components = null;
        private Label helpMessageLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        public HelpFormGUI()
        {
            this.components = new System.ComponentModel.Container();
            //this.cellNameTextBox = new System.Windows.Forms.TextBox();
            this.helpMessageLabel = new Label();
            this.SuspendLayout();

            this.helpMessageLabel.AutoSize = true;
            this.helpMessageLabel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            this.helpMessageLabel.Location = new Point(0, 0);
            this.helpMessageLabel.Text = "Welcome to Spreadsheet!\n" +
                "\tFile menu: The file menu in the top right corner allows you to... \n" +
                "\t- open a previously saved spreadsheet\n" +
                "\t- save your current spreadsheet\n" +
                "\t- open any number of new spreadsheets\n" +
                "\t- close the current spreadsheet (to avoid warnings, make sure to save first!)\n\n" +
                "\tCell Operations: \n" +
                "\t- to select a cell, click on the desired cell or use the arrow or tab keys\n" +
                "\t- to edit a cell's contents, click on the Formula textbox and enter a new formula!\n\n" +
                "\tAdditional Features: \n" +
                "\t- Default help text in formula box\n" +
                "\t- Ctrl+s saves spreadsheet\n" +
                "\t- Enter key evaluates the typed formula\n" +
                "\t- Mouse over formula textbox for tooltip\n";

            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(500, 300);
            this.Text = "Help Window";
            this.Controls.Add(this.helpMessageLabel);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}