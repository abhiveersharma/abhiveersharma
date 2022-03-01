using SpreadsheetGrid_Core;
using SS;

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

    }
}