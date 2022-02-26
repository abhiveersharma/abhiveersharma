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
            this.spreadsheetGrid = new SpreadsheetGrid_Core.SpreadsheetGridWidget();
            this.cellNameTextBox = new System.Windows.Forms.TextBox();
            this.cellContentsTextBox = new System.Windows.Forms.TextBox();
            this.cellValueTextBox = new System.Windows.Forms.TextBox();
            this.selectedCellLabel = new System.Windows.Forms.Label();
            this.selectedCellValueLabel = new System.Windows.Forms.Label();
            this.selectedCellContentsLabel = new System.Windows.Forms.Label();
            this.helpButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // spreadsheetGrid
            // 
            this.spreadsheetGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadsheetGrid.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.spreadsheetGrid.Location = new System.Drawing.Point(0, 80);
            this.spreadsheetGrid.MinimumSize = new System.Drawing.Size(100, 100);
            this.spreadsheetGrid.Name = "spreadsheetGrid";
            this.spreadsheetGrid.Size = new System.Drawing.Size(1141, 568);
            this.spreadsheetGrid.TabIndex = 0;
            // 
            // cellNameTextBox
            // 
            this.cellNameTextBox.Location = new System.Drawing.Point(216, 7);
            this.cellNameTextBox.Name = "cellNameTextBox";
            this.cellNameTextBox.ReadOnly = true;
            this.cellNameTextBox.Size = new System.Drawing.Size(44, 23);
            this.cellNameTextBox.TabIndex = 1;
            // 
            // cellContentsTextBox
            // 
            this.cellContentsTextBox.Location = new System.Drawing.Point(565, 8);
            this.cellContentsTextBox.Name = "cellContentsTextBox";
            this.cellContentsTextBox.Size = new System.Drawing.Size(227, 23);
            this.cellContentsTextBox.TabIndex = 2;
            // 
            // cellValueTextBox
            // 
            this.cellValueTextBox.Location = new System.Drawing.Point(391, 7);
            this.cellValueTextBox.Name = "cellValueTextBox";
            this.cellValueTextBox.ReadOnly = true;
            this.cellValueTextBox.Size = new System.Drawing.Size(100, 23);
            this.cellValueTextBox.TabIndex = 3;
            // 
            // selectedCellLabel
            // 
            this.selectedCellLabel.AutoSize = true;
            this.selectedCellLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.selectedCellLabel.Location = new System.Drawing.Point(130, 9);
            this.selectedCellLabel.Name = "selectedCellLabel";
            this.selectedCellLabel.Size = new System.Drawing.Size(86, 17);
            this.selectedCellLabel.TabIndex = 4;
            this.selectedCellLabel.Text = "Selected Cell:";
            // 
            // selectedCellValueLabel
            // 
            this.selectedCellValueLabel.AutoSize = true;
            this.selectedCellValueLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.selectedCellValueLabel.Location = new System.Drawing.Point(269, 9);
            this.selectedCellValueLabel.Name = "selectedCellValueLabel";
            this.selectedCellValueLabel.Size = new System.Drawing.Size(122, 17);
            this.selectedCellValueLabel.TabIndex = 5;
            this.selectedCellValueLabel.Text = "Selected Cell Value:";
            // 
            // selectedCellContentsLabel
            // 
            this.selectedCellContentsLabel.AutoSize = true;
            this.selectedCellContentsLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.selectedCellContentsLabel.Location = new System.Drawing.Point(504, 9);
            this.selectedCellContentsLabel.Name = "selectedCellContentsLabel";
            this.selectedCellContentsLabel.Size = new System.Drawing.Size(61, 17);
            this.selectedCellContentsLabel.TabIndex = 6;
            this.selectedCellContentsLabel.Text = "Formula:";
            // 
            // helpButton
            // 
            this.helpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helpButton.Location = new System.Drawing.Point(1054, 3);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(75, 23);
            this.helpButton.TabIndex = 7;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = true;
            // 
            // SpreadsheetGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1141, 649);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.selectedCellContentsLabel);
            this.Controls.Add(this.selectedCellValueLabel);
            this.Controls.Add(this.selectedCellLabel);
            this.Controls.Add(this.cellValueTextBox);
            this.Controls.Add(this.cellContentsTextBox);
            this.Controls.Add(this.cellNameTextBox);
            this.Controls.Add(this.spreadsheetGrid);
            this.HelpButton = true;
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "SpreadsheetGUI";
            this.Text = "Spreadsheet Window";
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
    }
}