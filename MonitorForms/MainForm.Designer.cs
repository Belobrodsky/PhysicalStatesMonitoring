namespace MonitorForms
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.normalizeButton = new System.Windows.Forms.ToolStripButton();
            this.startButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addSeriesButton = new System.Windows.Forms.ToolStripButton();
            this.removeSeriesButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.logVisibleButton = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.scudIptSplitContainer = new System.Windows.Forms.SplitContainer();
            this.scudTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.scudListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.iptTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.iptListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.startReadingtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.runEmulatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mbcliVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.graphChart1 = new GraphMonitor.GraphChart();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scudIptSplitContainer)).BeginInit();
            this.scudIptSplitContainer.Panel1.SuspendLayout();
            this.scudIptSplitContainer.Panel2.SuspendLayout();
            this.scudIptSplitContainer.SuspendLayout();
            this.scudTableLayoutPanel.SuspendLayout();
            this.iptTableLayoutPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 25;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalizeButton,
            this.startButton,
            this.toolStripSeparator1,
            this.addSeriesButton,
            this.removeSeriesButton,
            this.toolStripSeparator5,
            this.logVisibleButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1083, 27);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // normalizeButton
            // 
            this.normalizeButton.CheckOnClick = true;
            this.normalizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.normalizeButton.Image = ((System.Drawing.Image)(resources.GetObject("normalizeButton.Image")));
            this.normalizeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.normalizeButton.Name = "normalizeButton";
            this.normalizeButton.Size = new System.Drawing.Size(98, 24);
            this.normalizeButton.Text = "Нормализовать";
            this.normalizeButton.CheckedChanged += new System.EventHandler(this.normalizeButton_CheckedChanged);
            // 
            // startButton
            // 
            this.startButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.startButton.Image = ((System.Drawing.Image)(resources.GetObject("startButton.Image")));
            this.startButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(50, 24);
            this.startButton.Text = "Начать";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // addSeriesButton
            // 
            this.addSeriesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addSeriesButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addSeriesButton.Image = ((System.Drawing.Image)(resources.GetObject("addSeriesButton.Image")));
            this.addSeriesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addSeriesButton.Name = "addSeriesButton";
            this.addSeriesButton.Size = new System.Drawing.Size(23, 24);
            this.addSeriesButton.Text = "+";
            this.addSeriesButton.ToolTipText = "Добавить параметр";
            this.addSeriesButton.Click += new System.EventHandler(this.addSeriesButton_Click);
            // 
            // removeSeriesButton
            // 
            this.removeSeriesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.removeSeriesButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.removeSeriesButton.Image = ((System.Drawing.Image)(resources.GetObject("removeSeriesButton.Image")));
            this.removeSeriesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeSeriesButton.Name = "removeSeriesButton";
            this.removeSeriesButton.Size = new System.Drawing.Size(23, 24);
            this.removeSeriesButton.Text = "-";
            this.removeSeriesButton.ToolTipText = "Удалить параметр";
            this.removeSeriesButton.Click += new System.EventHandler(this.removeSeriesButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // logVisibleButton
            // 
            this.logVisibleButton.CheckOnClick = true;
            this.logVisibleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.logVisibleButton.Image = ((System.Drawing.Image)(resources.GetObject("logVisibleButton.Image")));
            this.logVisibleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.logVisibleButton.Name = "logVisibleButton";
            this.logVisibleButton.Size = new System.Drawing.Size(79, 24);
            this.logVisibleButton.Text = "Лог ошибок";
            this.logVisibleButton.ToolTipText = "Отображение/скрытие лога программы";
            this.logVisibleButton.Click += new System.EventHandler(this.logVisibleButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(679, 232);
            this.dataGridView1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.graphChart1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1083, 472);
            this.splitContainer1.SplitterDistance = 236;
            this.splitContainer1.TabIndex = 4;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.scudIptSplitContainer);
            this.splitContainer2.Size = new System.Drawing.Size(1083, 232);
            this.splitContainer2.SplitterDistance = 679;
            this.splitContainer2.TabIndex = 2;
            // 
            // scudIptSplitContainer
            // 
            this.scudIptSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scudIptSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.scudIptSplitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.scudIptSplitContainer.Name = "scudIptSplitContainer";
            // 
            // scudIptSplitContainer.Panel1
            // 
            this.scudIptSplitContainer.Panel1.Controls.Add(this.scudTableLayoutPanel);
            // 
            // scudIptSplitContainer.Panel2
            // 
            this.scudIptSplitContainer.Panel2.Controls.Add(this.iptTableLayoutPanel);
            this.scudIptSplitContainer.Size = new System.Drawing.Size(400, 232);
            this.scudIptSplitContainer.SplitterDistance = 184;
            this.scudIptSplitContainer.TabIndex = 1;
            // 
            // scudTableLayoutPanel
            // 
            this.scudTableLayoutPanel.ColumnCount = 1;
            this.scudTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.scudTableLayoutPanel.Controls.Add(this.scudListBox, 0, 1);
            this.scudTableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.scudTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scudTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.scudTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.scudTableLayoutPanel.Name = "scudTableLayoutPanel";
            this.scudTableLayoutPanel.RowCount = 2;
            this.scudTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.scudTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.scudTableLayoutPanel.Size = new System.Drawing.Size(184, 232);
            this.scudTableLayoutPanel.TabIndex = 0;
            // 
            // scudListBox
            // 
            this.scudListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scudListBox.FormattingEnabled = true;
            this.scudListBox.Location = new System.Drawing.Point(3, 16);
            this.scudListBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.scudListBox.Name = "scudListBox";
            this.scudListBox.Size = new System.Drawing.Size(178, 216);
            this.scudListBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "СКУД";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iptTableLayoutPanel
            // 
            this.iptTableLayoutPanel.ColumnCount = 1;
            this.iptTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.iptTableLayoutPanel.Controls.Add(this.iptListBox, 0, 1);
            this.iptTableLayoutPanel.Controls.Add(this.label2, 0, 0);
            this.iptTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iptTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.iptTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.iptTableLayoutPanel.Name = "iptTableLayoutPanel";
            this.iptTableLayoutPanel.RowCount = 2;
            this.iptTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.iptTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.iptTableLayoutPanel.Size = new System.Drawing.Size(212, 232);
            this.iptTableLayoutPanel.TabIndex = 1;
            // 
            // iptListBox
            // 
            this.iptListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iptListBox.FormattingEnabled = true;
            this.iptListBox.Location = new System.Drawing.Point(3, 16);
            this.iptListBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.iptListBox.Name = "iptListBox";
            this.iptListBox.Size = new System.Drawing.Size(206, 216);
            this.iptListBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "ИПТ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1083, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem,
            this.toolStripSeparator3,
            this.startReadingtoolStripMenuItem,
            this.toolStripSeparator4,
            this.runEmulatorToolStripMenuItem,
            this.toolStripSeparator2,
            this.mbcliVersionToolStripMenuItem});
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.serverToolStripMenuItem.Text = "Сервер";
            this.serverToolStripMenuItem.DropDownOpening += new System.EventHandler(this.serverToolStripMenuItem_DropDownOpening);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.connectToolStripMenuItem.Text = "Подключиться";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.disconnectToolStripMenuItem.Text = "Отключиться";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(194, 6);
            // 
            // startReadingtoolStripMenuItem
            // 
            this.startReadingtoolStripMenuItem.Name = "startReadingtoolStripMenuItem";
            this.startReadingtoolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.startReadingtoolStripMenuItem.Text = "Начать чтение данных";
            this.startReadingtoolStripMenuItem.Click += new System.EventHandler(this.startReadingtoolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(194, 6);
            // 
            // runEmulatorToolStripMenuItem
            // 
            this.runEmulatorToolStripMenuItem.Name = "runEmulatorToolStripMenuItem";
            this.runEmulatorToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.runEmulatorToolStripMenuItem.Text = "Запустить эмулятор";
            this.runEmulatorToolStripMenuItem.Click += new System.EventHandler(this.runEmulatorToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(194, 6);
            // 
            // mbcliVersionToolStripMenuItem
            // 
            this.mbcliVersionToolStripMenuItem.Name = "mbcliVersionToolStripMenuItem";
            this.mbcliVersionToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.mbcliVersionToolStripMenuItem.Text = "Версия mbcli.dll";
            this.mbcliVersionToolStripMenuItem.Click += new System.EventHandler(this.mbcliVersionToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.settingsToolStripMenuItem.Text = "Настройки...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 51);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.logTextBox);
            this.splitContainer4.Size = new System.Drawing.Size(1083, 646);
            this.splitContainer4.SplitterDistance = 472;
            this.splitContainer4.TabIndex = 6;
            // 
            // logTextBox
            // 
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.HideSelection = false;
            this.logTextBox.Location = new System.Drawing.Point(0, 0);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(1083, 170);
            this.logTextBox.TabIndex = 0;
            // 
            // graphChart1
            // 
            this.graphChart1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphChart1.Count = 1;
            this.graphChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphChart1.Location = new System.Drawing.Point(0, 0);
            this.graphChart1.MonitorValues = null;
            this.graphChart1.Name = "graphChart1";
            this.graphChart1.SelectedPoint = null;
            this.graphChart1.SelectedSeries = null;
            this.graphChart1.Size = new System.Drawing.Size(1083, 236);
            this.graphChart1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 697);
            this.Controls.Add(this.splitContainer4);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.scudIptSplitContainer.Panel1.ResumeLayout(false);
            this.scudIptSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scudIptSplitContainer)).EndInit();
            this.scudIptSplitContainer.ResumeLayout(false);
            this.scudTableLayoutPanel.ResumeLayout(false);
            this.scudTableLayoutPanel.PerformLayout();
            this.iptTableLayoutPanel.ResumeLayout(false);
            this.iptTableLayoutPanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GraphMonitor.GraphChart graphChart1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton normalizeButton;
        private System.Windows.Forms.ToolStripButton startButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton addSeriesButton;
        private System.Windows.Forms.ToolStripButton removeSeriesButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runEmulatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mbcliVersionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem startReadingtoolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox scudListBox;
        private System.Windows.Forms.SplitContainer scudIptSplitContainer;
        private System.Windows.Forms.TableLayoutPanel scudTableLayoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel iptTableLayoutPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox iptListBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton logVisibleButton;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TextBox logTextBox;
    }
}

