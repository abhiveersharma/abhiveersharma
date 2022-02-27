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
    }
}