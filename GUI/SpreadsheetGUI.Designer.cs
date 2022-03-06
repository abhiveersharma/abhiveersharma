namespace GUI
{
    partial class SpreadsheetGUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.spreadsheetGrid = new SpreadsheetGrid_Core.SpreadsheetGridWidget();
            this.cellNameTextBox = new System.Windows.Forms.TextBox();
            this.cellContentsTextBox = new System.Windows.Forms.TextBox();
            this.cellValueTextBox = new System.Windows.Forms.TextBox();
            this.selectedCellLabel = new System.Windows.Forms.Label();
            this.selectedCellValueLabel = new System.Windows.Forms.Label();
            this.selectedCellContentsLabel = new System.Windows.Forms.Label();
            this.helpButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.evaluateFormulaButton = new System.Windows.Forms.Button();
            this.hoverMouseToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.longCalcBGWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // spreadsheetGrid
            // 
            this.spreadsheetGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadsheetGrid.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.spreadsheetGrid.Location = new System.Drawing.Point(0, 82);
            this.spreadsheetGrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.spreadsheetGrid.MinimumSize = new System.Drawing.Size(100, 100);
            this.spreadsheetGrid.Name = "spreadsheetGrid";
            this.spreadsheetGrid.Size = new System.Drawing.Size(1034, 448);
            this.spreadsheetGrid.TabIndex = 0;
            // 
            // cellNameTextBox
            // 
            this.cellNameTextBox.Location = new System.Drawing.Point(304, 29);
            this.cellNameTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cellNameTextBox.Name = "cellNameTextBox";
            this.cellNameTextBox.ReadOnly = true;
            this.cellNameTextBox.Size = new System.Drawing.Size(44, 23);
            this.cellNameTextBox.TabIndex = 1;
            // 
            // cellContentsTextBox
            // 
            this.cellContentsTextBox.Location = new System.Drawing.Point(662, 29);
            this.cellContentsTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cellContentsTextBox.Name = "cellContentsTextBox";
            this.cellContentsTextBox.PlaceholderText = "Start each formula with \'=\'";
            this.cellContentsTextBox.Size = new System.Drawing.Size(227, 23);
            this.cellContentsTextBox.TabIndex = 2;
            this.cellContentsTextBox.TextChanged += new System.EventHandler(this.cellContentsTextBox_TextChanged);
            this.cellContentsTextBox.Leave += new System.EventHandler(this.cellContentsTextBox_Leave);
            this.cellContentsTextBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cellContentsTextBox_MouseMove);
            // 
            // cellValueTextBox
            // 
            this.cellValueTextBox.Location = new System.Drawing.Point(487, 29);
            this.cellValueTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cellValueTextBox.Name = "cellValueTextBox";
            this.cellValueTextBox.ReadOnly = true;
            this.cellValueTextBox.Size = new System.Drawing.Size(100, 23);
            this.cellValueTextBox.TabIndex = 3;
            // 
            // selectedCellLabel
            // 
            this.selectedCellLabel.AutoSize = true;
            this.selectedCellLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.selectedCellLabel.Location = new System.Drawing.Point(210, 29);
            this.selectedCellLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.selectedCellLabel.Name = "selectedCellLabel";
            this.selectedCellLabel.Size = new System.Drawing.Size(86, 17);
            this.selectedCellLabel.TabIndex = 4;
            this.selectedCellLabel.Text = "Selected Cell:";
            // 
            // selectedCellValueLabel
            // 
            this.selectedCellValueLabel.AutoSize = true;
            this.selectedCellValueLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.selectedCellValueLabel.Location = new System.Drawing.Point(354, 29);
            this.selectedCellValueLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.selectedCellValueLabel.Name = "selectedCellValueLabel";
            this.selectedCellValueLabel.Size = new System.Drawing.Size(122, 17);
            this.selectedCellValueLabel.TabIndex = 5;
            this.selectedCellValueLabel.Text = "Selected Cell Value:";
            // 
            // selectedCellContentsLabel
            // 
            this.selectedCellContentsLabel.AutoSize = true;
            this.selectedCellContentsLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.selectedCellContentsLabel.Location = new System.Drawing.Point(598, 29);
            this.selectedCellContentsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.selectedCellContentsLabel.Name = "selectedCellContentsLabel";
            this.selectedCellContentsLabel.Size = new System.Drawing.Size(61, 17);
            this.selectedCellContentsLabel.TabIndex = 6;
            this.selectedCellContentsLabel.Text = "Formula:";
            // 
            // helpButton
            // 
            this.helpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helpButton.Location = new System.Drawing.Point(959, 0);
            this.helpButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(75, 23);
            this.helpButton.TabIndex = 7;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1036, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // evaluateFormulaButton
            // 
            this.evaluateFormulaButton.Location = new System.Drawing.Point(895, 29);
            this.evaluateFormulaButton.Margin = new System.Windows.Forms.Padding(2);
            this.evaluateFormulaButton.Name = "evaluateFormulaButton";
            this.evaluateFormulaButton.Size = new System.Drawing.Size(78, 20);
            this.evaluateFormulaButton.TabIndex = 9;
            this.evaluateFormulaButton.Text = "Evaluate";
            this.evaluateFormulaButton.UseVisualStyleBackColor = true;
            this.evaluateFormulaButton.Click += new System.EventHandler(this.evaluateFormulaButton_Click);
            this.evaluateFormulaButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.evaluateFormulaButton_KeyPress);
            // 
            // hoverMouseToolTip
            // 
            this.hoverMouseToolTip.AutoPopDelay = 5000;
            this.hoverMouseToolTip.InitialDelay = 1000;
            this.hoverMouseToolTip.ReshowDelay = 100;
            // 
            // longCalcBGWorker
            // 
            this.longCalcBGWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.longCalcBGWorker_DoWork);
            // 
            // SpreadsheetGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 529);
            this.Controls.Add(this.evaluateFormulaButton);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.selectedCellContentsLabel);
            this.Controls.Add(this.selectedCellValueLabel);
            this.Controls.Add(this.selectedCellLabel);
            this.Controls.Add(this.cellValueTextBox);
            this.Controls.Add(this.cellContentsTextBox);
            this.Controls.Add(this.cellNameTextBox);
            this.Controls.Add(this.spreadsheetGrid);
            this.Controls.Add(this.menuStrip1);
            this.HelpButton = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(193, 168);
            this.Name = "SpreadsheetGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spreadsheet Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpreadsheetGUI_FormClosing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SpreadsheetGUI_KeyPress);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SpreadsheetGrid_Core.SpreadsheetGridWidget spreadsheetGrid;
        private TextBox cellNameTextBox;
        private TextBox cellContentsTextBox;
        private TextBox cellValueTextBox;
        private Label selectedCellLabel;
        private Label selectedCellValueLabel;
        private Label selectedCellContentsLabel;
        private Button helpButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private Button evaluateFormulaButton;
        private ToolTip hoverMouseToolTip;
        private System.ComponentModel.BackgroundWorker longCalcBGWorker;
    }
}