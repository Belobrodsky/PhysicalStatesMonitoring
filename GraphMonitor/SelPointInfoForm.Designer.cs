namespace GraphMonitor
{
    sealed partial class SelPointInfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.selInfoGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // selInfoGrid
            // 
            this.selInfoGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selInfoGrid.HelpVisible = false;
            this.selInfoGrid.Location = new System.Drawing.Point(0, 0);
            this.selInfoGrid.Name = "selInfoGrid";
            this.selInfoGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.selInfoGrid.Size = new System.Drawing.Size(424, 111);
            this.selInfoGrid.TabIndex = 0;
            this.selInfoGrid.ToolbarVisible = false;
            // 
            // SelPointInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 111);
            this.Controls.Add(this.selInfoGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SelPointInfoForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SelPointInfoForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelPointInfoForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid selInfoGrid;
    }
}