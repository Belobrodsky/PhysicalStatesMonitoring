namespace MonitorForms
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.scudIpEndPoint = new MonitorForms.IpEndPointEditor();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.iptIpEndPoint = new MonitorForms.IpEndPointEditor();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pathsTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.logFilePathSelector = new MonitorForms.FilePathSelector();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.emulFilePathSelector = new MonitorForms.FilePathSelector();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.connectionTabPage = new System.Windows.Forms.TabPage();
            this.dataTabPage = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.pathsTabPage.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.connectionTabPage.SuspendLayout();
            this.dataTabPage.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(441, 161);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel3, 2);
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(435, 155);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.scudIpEndPoint);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(211, 149);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "СКУД";
            // 
            // scudIpEndPoint
            // 
            this.scudIpEndPoint.Address = "   .   .   .";
            this.scudIpEndPoint.AutoSize = true;
            this.scudIpEndPoint.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.scudIpEndPoint.Dock = System.Windows.Forms.DockStyle.Top;
            this.scudIpEndPoint.Location = new System.Drawing.Point(3, 16);
            this.scudIpEndPoint.Name = "scudIpEndPoint";
            this.scudIpEndPoint.Port = 1952;
            this.scudIpEndPoint.Size = new System.Drawing.Size(205, 52);
            this.scudIpEndPoint.TabIndex = 2;
            this.scudIpEndPoint.IsAddressValidChanged += new System.EventHandler(this.ipEndPointEditor_IsAddressValidChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.iptIpEndPoint);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(220, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 149);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ИПТ";
            // 
            // iptIpEndPoint
            // 
            this.iptIpEndPoint.Address = "   .   .   .";
            this.iptIpEndPoint.AutoSize = true;
            this.iptIpEndPoint.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.iptIpEndPoint.Dock = System.Windows.Forms.DockStyle.Top;
            this.iptIpEndPoint.Location = new System.Drawing.Point(3, 16);
            this.iptIpEndPoint.Name = "iptIpEndPoint";
            this.iptIpEndPoint.Port = 1952;
            this.iptIpEndPoint.Size = new System.Drawing.Size(206, 52);
            this.iptIpEndPoint.TabIndex = 3;
            this.iptIpEndPoint.IsAddressValidChanged += new System.EventHandler(this.ipEndPointEditor_IsAddressValidChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.AutoSize = true;
            this.cancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(302, 205);
            this.cancelButton.MinimumSize = new System.Drawing.Size(75, 0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.AutoSize = true;
            this.okButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(221, 205);
            this.okButton.MinimumSize = new System.Drawing.Size(75, 0);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "ОК";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.dataTabPage);
            this.tabControl1.Controls.Add(this.pathsTabPage);
            this.tabControl1.Controls.Add(this.connectionTabPage);
            this.tabControl1.Location = new System.Drawing.Point(9, 9);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(455, 193);
            this.tabControl1.TabIndex = 1;
            // 
            // pathsTabPage
            // 
            this.pathsTabPage.Controls.Add(this.tableLayoutPanel5);
            this.pathsTabPage.Location = new System.Drawing.Point(4, 22);
            this.pathsTabPage.Name = "pathsTabPage";
            this.pathsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.pathsTabPage.Size = new System.Drawing.Size(447, 167);
            this.pathsTabPage.TabIndex = 0;
            this.pathsTabPage.Text = "Файлы";
            this.pathsTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.logFilePathSelector, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.emulFilePathSelector, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(441, 161);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // logFilePathSelector
            // 
            this.logFilePathSelector.AutoSize = true;
            this.logFilePathSelector.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.logFilePathSelector.Caption = "Файл результатов:";
            this.logFilePathSelector.Dock = System.Windows.Forms.DockStyle.Top;
            this.logFilePathSelector.FileDialog = this.saveFileDialog1;
            this.logFilePathSelector.FilePath = "";
            this.logFilePathSelector.Location = new System.Drawing.Point(0, 0);
            this.logFilePathSelector.Margin = new System.Windows.Forms.Padding(0);
            this.logFilePathSelector.Name = "logFilePathSelector";
            this.logFilePathSelector.Size = new System.Drawing.Size(441, 49);
            this.logFilePathSelector.TabIndex = 1;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Текстовые файлы|*.txt";
            this.saveFileDialog1.OverwritePrompt = false;
            this.saveFileDialog1.SupportMultiDottedExtensions = true;
            this.saveFileDialog1.Title = "Выберите файл для записи результата";
            // 
            // emulFilePathSelector
            // 
            this.emulFilePathSelector.AutoSize = true;
            this.emulFilePathSelector.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.emulFilePathSelector.Caption = "Эмулятор ИПТ:";
            this.emulFilePathSelector.Dock = System.Windows.Forms.DockStyle.Top;
            this.emulFilePathSelector.FileDialog = this.saveFileDialog2;
            this.emulFilePathSelector.FilePath = "";
            this.emulFilePathSelector.Location = new System.Drawing.Point(0, 49);
            this.emulFilePathSelector.Margin = new System.Windows.Forms.Padding(0);
            this.emulFilePathSelector.Name = "emulFilePathSelector";
            this.emulFilePathSelector.Size = new System.Drawing.Size(441, 49);
            this.emulFilePathSelector.TabIndex = 0;
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.Filter = "Исполняемые файлы|*.exe";
            this.saveFileDialog2.OverwritePrompt = false;
            this.saveFileDialog2.SupportMultiDottedExtensions = true;
            this.saveFileDialog2.Title = "Эмулятор ИПТ";
            // 
            // connectionTabPage
            // 
            this.connectionTabPage.Controls.Add(this.tableLayoutPanel1);
            this.connectionTabPage.Location = new System.Drawing.Point(4, 22);
            this.connectionTabPage.Name = "connectionTabPage";
            this.connectionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.connectionTabPage.Size = new System.Drawing.Size(447, 167);
            this.connectionTabPage.TabIndex = 1;
            this.connectionTabPage.Text = "Соединение";
            this.connectionTabPage.UseVisualStyleBackColor = true;
            // 
            // dataTabPage
            // 
            this.dataTabPage.Controls.Add(this.tableLayoutPanel2);
            this.dataTabPage.Location = new System.Drawing.Point(4, 22);
            this.dataTabPage.Name = "dataTabPage";
            this.dataTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.dataTabPage.Size = new System.Drawing.Size(447, 167);
            this.dataTabPage.TabIndex = 2;
            this.dataTabPage.Text = "Данные";
            this.dataTabPage.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ControlDarkDark;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.tableLayoutPanel2.SetRowSpan(this.propertyGrid1, 2);
            this.propertyGrid1.Size = new System.Drawing.Size(113, 155);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.ToolbarVisible = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.21088F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.78912F));
            this.tableLayoutPanel2.Controls.Add(this.propertyGrid1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(441, 161);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(983, 438);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.pathsTabPage.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.connectionTabPage.ResumeLayout(false);
            this.dataTabPage.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private FilePathSelector logFilePathSelector;
        private FilePathSelector emulFilePathSelector;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage pathsTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TabPage connectionTabPage;
        private System.Windows.Forms.TabPage dataTabPage;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private IpEndPointEditor scudIpEndPoint;
        private IpEndPointEditor iptIpEndPoint;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}