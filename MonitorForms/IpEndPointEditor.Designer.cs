namespace MonitorForms
{
    partial class IpEndPointEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ipAddressMaskTextBox = new System.Windows.Forms.MaskedTextBox();
            this.portNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ipAddressMaskTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.portNumericUpDown, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(150, 52);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Адрес:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Порт:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ipAddressMaskTextBox
            // 
            this.ipAddressMaskTextBox.AsciiOnly = true;
            this.ipAddressMaskTextBox.Culture = new System.Globalization.CultureInfo("");
            this.ipAddressMaskTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipAddressMaskTextBox.HidePromptOnLeave = true;
            this.ipAddressMaskTextBox.HideSelection = false;
            this.ipAddressMaskTextBox.Location = new System.Drawing.Point(50, 3);
            this.ipAddressMaskTextBox.Mask = "000.000.000.000";
            this.ipAddressMaskTextBox.Name = "ipAddressMaskTextBox";
            this.ipAddressMaskTextBox.PromptChar = ' ';
            this.ipAddressMaskTextBox.Size = new System.Drawing.Size(97, 20);
            this.ipAddressMaskTextBox.TabIndex = 0;
            this.ipAddressMaskTextBox.TypeValidationCompleted += new System.Windows.Forms.TypeValidationEventHandler(this.ipAddressMaskTextBox_TypeValidationCompleted);
            // 
            // portNumericUpDown
            // 
            this.portNumericUpDown.Location = new System.Drawing.Point(50, 29);
            this.portNumericUpDown.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.portNumericUpDown.Name = "portNumericUpDown";
            this.portNumericUpDown.Size = new System.Drawing.Size(58, 20);
            this.portNumericUpDown.TabIndex = 1;
            this.portNumericUpDown.Value = new decimal(new int[] {
            1952,
            0,
            0,
            0});
            // 
            // IpEndPointEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "IpEndPointEditor";
            this.Size = new System.Drawing.Size(150, 52);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox ipAddressMaskTextBox;
        private System.Windows.Forms.NumericUpDown portNumericUpDown;
    }
}
