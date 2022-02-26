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
            this.spreadsheetGridWidget1 = new SpreadsheetGrid_Core.SpreadsheetGridWidget();
            this.SuspendLayout();
            // 
            // spreadsheetGridWidget1
            // 
            this.spreadsheetGridWidget1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.spreadsheetGridWidget1.Location = new System.Drawing.Point(0, -2);
            this.spreadsheetGridWidget1.Name = "spreadsheetGridWidget1";
            this.spreadsheetGridWidget1.Size = new System.Drawing.Size(1141, 650);
            this.spreadsheetGridWidget1.TabIndex = 0;
            // 
            // SpreadsheetGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1141, 649);
            this.Controls.Add(this.spreadsheetGridWidget1);
            this.Name = "SpreadsheetGUI";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private SpreadsheetGrid_Core.SpreadsheetGridWidget spreadsheetGridWidget1;
    }
}