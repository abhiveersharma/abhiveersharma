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

        private void selectCell(SpreadsheetGridWidget sender)
        {
            sender.GetSelection(out int col, out int row);
            cellNameTextBox.Text = ConvertColRowToVariable(col, row);

            this.spreadsheetGrid.GetValue(col, row, out string selectedCellValue); //Also display the formula in the main selected cell formula txt box
            cellValueTextBox.Text = selectedCellValue; //TODO: Make sure actually shows evaluated value of a formula with variables!

            cellContentsTextBox.Text = selectedCellValue; //Also, change contents txt box to show what is in cell
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
                    spreadsheet.GetSavedVersion(openDialog.FileName); //Gets version of file.. should be "six"

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


            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            Form helpForm = new Form();
            helpForm.Show();
            MessageBox.Show("Welcome to Spreadsheet!\n" +
                "\tFile menu: The file menu in the top right corner allows you to... \n" +
                "\topen a previously saved spreadsheet\n" +
                "\tsave your current spreadsheet\n" +
                "\topen any number of new spreadsheets\n" +
                "\tclose the current spreadsheet (to avoid warnings, make sure to save first!)\n\n" +
                "\tCell Operations: \n" +
                "\tto select a cell, click on the desired cell or user the arrow keys\n" +
                "\tto edit a cell's contents, click on the Formula textbox and enter a new formula!\n" +
                "\tAdditional Features: \n"
                , "Help Menu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cellContentsTextBox_TextChanged(object sender, EventArgs e)
        {
            this.spreadsheetGrid.GetSelection(out int col, out int row); //Get location of cell to change (currently selected cell)
            string selectedCellName = ConvertColRowToVariable(col, row); //Change cell location to cell/variable name
            this.spreadsheet.SetContentsOfCell(selectedCellName, cellContentsTextBox.Text); //Sets the contents of the cell to whatever text was entered into txt box
            this.spreadsheetGrid.SetValue(col, row, cellContentsTextBox.Text); //Display the formula in the cell in grid
            
        }

        private void cellContentsTextBox_Leave(object sender, EventArgs e)
        {
            //cellContentsTextBox.Text = "";
        }

    }
}