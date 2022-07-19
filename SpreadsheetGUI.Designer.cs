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
            this.spreadsheetGrid.AutoScroll = true;
            this.spreadsheetGrid.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.spreadsheetGrid.Location = new System.Drawing.Point(0, 175);
            this.spreadsheetGrid.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.spreadsheetGrid.MinimumSize = new System.Drawing.Size(186, 213);
            this.spreadsheetGrid.Name = "spreadsheetGrid";
            this.spreadsheetGrid.Size = new System.Drawing.Size(1920, 213);
            this.spreadsheetGrid.TabIndex = 0;
            // 
            // cellNameTextBox
            // 
            this.cellNameTextBox.Location = new System.Drawing.Point(565, 62);
            this.cellNameTextBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.cellNameTextBox.Name = "cellNameTextBox";
            this.cellNameTextBox.ReadOnly = true;
            this.cellNameTextBox.Size = new System.Drawing.Size(78, 39);
            this.cellNameTextBox.TabIndex = 1;
            // 
            // cellContentsTextBox
            // 
            this.cellContentsTextBox.Location = new System.Drawing.Point(1229, 62);
            this.cellContentsTextBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.cellContentsTextBox.Name = "cellContentsTextBox";
            this.cellContentsTextBox.PlaceholderText = "Start each formula with \'=\'";
            this.cellContentsTextBox.Size = new System.Drawing.Size(418, 39);
            this.cellContentsTextBox.TabIndex = 2;
            this.cellContentsTextBox.TextChanged += new System.EventHandler(this.cellContentsTextBox_TextChanged);
            this.cellContentsTextBox.Leave += new System.EventHandler(this.cellContentsTextBox_Leave);
            this.cellContentsTextBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cellContentsTextBox_MouseMove);
            // 
            // cellValueTextBox
            // 
            this.cellValueTextBox.Location = new System.Drawing.Point(904, 62);
            this.cellValueTextBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.cellValueTextBox.Name = "cellValueTextBox";
            this.cellValueTextBox.ReadOnly = true;
            this.cellValueTextBox.Size = new System.Drawing.Size(182, 39);
            this.cellValueTextBox.TabIndex = 3;
            // 
            // selectedCellLabel
            // 
            this.selectedCellLabel.AutoSize = true;
            this.selectedCellLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.selectedCellLabel.Location = new System.Drawing.Point(390, 62);
            this.selectedCellLabel.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.selectedCellLabel.Name = "selectedCellLabel";
            this.selectedCellLabel.Size = new System.Drawing.Size(172, 36);
            this.selectedCellLabel.TabIndex = 4;
            this.selectedCellLabel.Text = "Selected Cell:";
            // 
            // selectedCellValueLabel
            // 
            this.selectedCellValueLabel.AutoSize = true;
            this.selectedCellValueLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.selectedCellValueLabel.Location = new System.Drawing.Point(657, 62);
            this.selectedCellValueLabel.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.selectedCellValueLabel.Name = "selectedCellValueLabel";
            this.selectedCellValueLabel.Size = new System.Drawing.Size(244, 36);
            this.selectedCellValueLabel.TabIndex = 5;
            this.selectedCellValueLabel.Text = "Selected Cell Value:";
            // 
            // selectedCellContentsLabel
            // 
            this.selectedCellContentsLabel.AutoSize = true;
            this.selectedCellContentsLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.selectedCellContentsLabel.Location = new System.Drawing.Point(1111, 62);
            this.selectedCellContentsLabel.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.selectedCellContentsLabel.Name = "selectedCellContentsLabel";
            this.selectedCellContentsLabel.Size = new System.Drawing.Size(119, 36);
            this.selectedCellContentsLabel.TabIndex = 6;
            this.selectedCellContentsLabel.Text = "Formula:";
            // 
            // helpButton
            // 
            this.helpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helpButton.Location = new System.Drawing.Point(1781, 0);
            this.helpButton.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(139, 49);
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(11, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(1924, 44);
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(71, 36);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(291, 44);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(291, 44);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(291, 44);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(291, 44);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // evaluateFormulaButton
            // 
            this.evaluateFormulaButton.Location = new System.Drawing.Point(1662, 62);
            this.evaluateFormulaButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.evaluateFormulaButton.Name = "evaluateFormulaButton";
            this.evaluateFormulaButton.Size = new System.Drawing.Size(145, 43);
            this.evaluateFormulaButton.TabIndex = 9;
            this.evaluateFormulaButton.Text = "Evaluate";
            this.evaluateFormulaButton.UseVisualStyleBackColor = true;
            this.evaluateFormulaButton.Click += new System.EventHandler(this.evaluateFormulaButton_Click);
            this.evaluateFormulaButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SpreadsheetGUI_KeyPress);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1129);
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
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.MinimumSize = new System.Drawing.Size(336, 278);
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