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
            this.iptFreqLabel = new System.Windows.Forms.ToolStripLabel();
            this.iptFreqComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.graphValuesDataGridView = new System.Windows.Forms.DataGridView();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.scudGraphSplitContainer = new System.Windows.Forms.SplitContainer();
            this.scudPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvIptSplitContainer = new System.Windows.Forms.SplitContainer();
            this.iptTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.iptListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.serverMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.startReadingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.runEmulatorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mbcliVersionItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorLogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scudMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iptMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.errorLogTextBox = new System.Windows.Forms.TextBox();
            this.errorLogcontextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.graphChart1 = new GraphMonitor.GraphChart();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graphValuesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scudGraphSplitContainer)).BeginInit();
            this.scudGraphSplitContainer.Panel1.SuspendLayout();
            this.scudGraphSplitContainer.Panel2.SuspendLayout();
            this.scudGraphSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIptSplitContainer)).BeginInit();
            this.dgvIptSplitContainer.Panel1.SuspendLayout();
            this.dgvIptSplitContainer.Panel2.SuspendLayout();
            this.dgvIptSplitContainer.SuspendLayout();
            this.iptTableLayoutPanel.SuspendLayout();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.errorLogcontextMenu.SuspendLayout();
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
            this.iptFreqLabel,
            this.iptFreqComboBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1159, 25);
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
            this.normalizeButton.Size = new System.Drawing.Size(98, 22);
            this.normalizeButton.Text = "Нормализовать";
            this.normalizeButton.CheckedChanged += new System.EventHandler(this.normalizeButton_CheckedChanged);
            // 
            // startButton
            // 
            this.startButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.startButton.Image = ((System.Drawing.Image)(resources.GetObject("startButton.Image")));
            this.startButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(50, 22);
            this.startButton.Text = "Начать";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // addSeriesButton
            // 
            this.addSeriesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addSeriesButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addSeriesButton.Image = global::MonitorForms.Properties.Resources.Add;
            this.addSeriesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addSeriesButton.Name = "addSeriesButton";
            this.addSeriesButton.Size = new System.Drawing.Size(23, 22);
            this.addSeriesButton.Text = "+";
            this.addSeriesButton.ToolTipText = "Добавить параметр";
            this.addSeriesButton.Click += new System.EventHandler(this.addSeriesButton_Click);
            // 
            // removeSeriesButton
            // 
            this.removeSeriesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeSeriesButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.removeSeriesButton.Image = global::MonitorForms.Properties.Resources.Remove;
            this.removeSeriesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeSeriesButton.Name = "removeSeriesButton";
            this.removeSeriesButton.Size = new System.Drawing.Size(23, 22);
            this.removeSeriesButton.Text = "-";
            this.removeSeriesButton.ToolTipText = "Удалить параметр";
            this.removeSeriesButton.Click += new System.EventHandler(this.removeSeriesButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // iptFreqLabel
            // 
            this.iptFreqLabel.Name = "iptFreqLabel";
            this.iptFreqLabel.Size = new System.Drawing.Size(124, 22);
            this.iptFreqLabel.Text = "Частота опроса ИПТ:";
            // 
            // iptFreqComboBox
            // 
            this.iptFreqComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.iptFreqComboBox.DropDownWidth = 75;
            this.iptFreqComboBox.Items.AddRange(new object[] {
            "1 Гц",
            "10 Гц",
            "20 Гц",
            "30 Гц",
            "40 Гц"});
            this.iptFreqComboBox.Name = "iptFreqComboBox";
            this.iptFreqComboBox.Size = new System.Drawing.Size(75, 25);
            this.iptFreqComboBox.SelectedIndexChanged += new System.EventHandler(this.iptFreqComboBox_SelectedIndexChanged);
            // 
            // graphValuesDataGridView
            // 
            this.graphValuesDataGridView.AllowUserToAddRows = false;
            this.graphValuesDataGridView.AllowUserToDeleteRows = false;
            this.graphValuesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.graphValuesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.graphValuesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphValuesDataGridView.Location = new System.Drawing.Point(0, 0);
            this.graphValuesDataGridView.Name = "graphValuesDataGridView";
            this.graphValuesDataGridView.ReadOnly = true;
            this.graphValuesDataGridView.Size = new System.Drawing.Size(648, 242);
            this.graphValuesDataGridView.TabIndex = 1;
            this.graphValuesDataGridView.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.graphValuesDataGridView_ColumnAdded);
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            this.mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.scudGraphSplitContainer);
            this.mainSplitContainer.Panel1.Controls.Add(this.toolStrip1);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.dgvIptSplitContainer);
            this.mainSplitContainer.Size = new System.Drawing.Size(1159, 491);
            this.mainSplitContainer.SplitterDistance = 245;
            this.mainSplitContainer.TabIndex = 4;
            // 
            // scudGraphSplitContainer
            // 
            this.scudGraphSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scudGraphSplitContainer.Location = new System.Drawing.Point(0, 25);
            this.scudGraphSplitContainer.Name = "scudGraphSplitContainer";
            // 
            // scudGraphSplitContainer.Panel1
            // 
            this.scudGraphSplitContainer.Panel1.Controls.Add(this.scudPropertyGrid);
            // 
            // scudGraphSplitContainer.Panel2
            // 
            this.scudGraphSplitContainer.Panel2.Controls.Add(this.graphChart1);
            this.scudGraphSplitContainer.Size = new System.Drawing.Size(1159, 220);
            this.scudGraphSplitContainer.SplitterDistance = global::MonitorForms.Properties.Settings.Default.ScudPanelWidth;
            this.scudGraphSplitContainer.TabIndex = 5;
            // 
            // scudPropertyGrid
            // 
            this.scudPropertyGrid.CategoryForeColor = System.Drawing.SystemColors.ButtonFace;
            this.scudPropertyGrid.CommandsDisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.scudPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scudPropertyGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scudPropertyGrid.HelpVisible = false;
            this.scudPropertyGrid.LineColor = System.Drawing.SystemColors.ControlDarkDark;
            this.scudPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.scudPropertyGrid.Name = "scudPropertyGrid";
            this.scudPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.scudPropertyGrid.SelectedObject = this.copyMenuItem;
            this.scudPropertyGrid.Size = new System.Drawing.Size(221, 220);
            this.scudPropertyGrid.TabIndex = 2;
            this.scudPropertyGrid.ToolbarVisible = false;
            // 
            // copyMenuItem
            // 
            this.copyMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyMenuItem.Image")));
            this.copyMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyMenuItem.Name = "copyMenuItem";
            this.copyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyMenuItem.Size = new System.Drawing.Size(181, 22);
            this.copyMenuItem.Text = "Копировать";
            this.copyMenuItem.Click += new System.EventHandler(this.copyMenuItem_Click);
            // 
            // dgvIptSplitContainer
            // 
            this.dgvIptSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIptSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.dgvIptSplitContainer.Name = "dgvIptSplitContainer";
            // 
            // dgvIptSplitContainer.Panel1
            // 
            this.dgvIptSplitContainer.Panel1.Controls.Add(this.graphValuesDataGridView);
            // 
            // dgvIptSplitContainer.Panel2
            // 
            this.dgvIptSplitContainer.Panel2.Controls.Add(this.iptTableLayoutPanel);
            this.dgvIptSplitContainer.Size = new System.Drawing.Size(1159, 242);
            this.dgvIptSplitContainer.SplitterDistance = global::MonitorForms.Properties.Settings.Default.DataGridViewWidthPanel;
            this.dgvIptSplitContainer.TabIndex = 2;
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
            this.iptTableLayoutPanel.Size = new System.Drawing.Size(507, 242);
            this.iptTableLayoutPanel.TabIndex = 2;
            // 
            // iptListBox
            // 
            this.iptListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iptListBox.FormattingEnabled = true;
            this.iptListBox.Location = new System.Drawing.Point(3, 16);
            this.iptListBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.iptListBox.Name = "iptListBox";
            this.iptListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.iptListBox.Size = new System.Drawing.Size(501, 226);
            this.iptListBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(501, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "ИПТ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverMenuItem,
            this.viewMenuItem,
            this.settingsMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1159, 24);
            this.mainMenu.TabIndex = 5;
            this.mainMenu.Text = "menuStrip1";
            // 
            // serverMenuItem
            // 
            this.serverMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectMenuItem,
            this.disconnectToolStripMenuItem,
            this.toolStripSeparator3,
            this.startReadingMenuItem,
            this.toolStripSeparator4,
            this.runEmulatorMenuItem,
            this.toolStripSeparator2,
            this.mbcliVersionItem});
            this.serverMenuItem.Name = "serverMenuItem";
            this.serverMenuItem.Size = new System.Drawing.Size(59, 20);
            this.serverMenuItem.Text = "Сервер";
            this.serverMenuItem.DropDownOpening += new System.EventHandler(this.serverToolStripMenuItem_DropDownOpening);
            // 
            // connectMenuItem
            // 
            this.connectMenuItem.Image = global::MonitorForms.Properties.Resources.Connect;
            this.connectMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.connectMenuItem.Name = "connectMenuItem";
            this.connectMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.connectMenuItem.Size = new System.Drawing.Size(216, 22);
            this.connectMenuItem.Text = "Подключиться";
            this.connectMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Image = global::MonitorForms.Properties.Resources.Disconnect;
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.disconnectToolStripMenuItem.Text = "Отключиться";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(213, 6);
            // 
            // startReadingMenuItem
            // 
            this.startReadingMenuItem.Image = global::MonitorForms.Properties.Resources.Run;
            this.startReadingMenuItem.Name = "startReadingMenuItem";
            this.startReadingMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.startReadingMenuItem.Size = new System.Drawing.Size(216, 22);
            this.startReadingMenuItem.Text = "Начать чтение данных";
            this.startReadingMenuItem.Click += new System.EventHandler(this.startReadingtoolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(213, 6);
            // 
            // runEmulatorMenuItem
            // 
            this.runEmulatorMenuItem.Image = global::MonitorForms.Properties.Resources.Console;
            this.runEmulatorMenuItem.Name = "runEmulatorMenuItem";
            this.runEmulatorMenuItem.Size = new System.Drawing.Size(216, 22);
            this.runEmulatorMenuItem.Text = "Эмулятор ИПТ";
            this.runEmulatorMenuItem.Click += new System.EventHandler(this.runEmulatorToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(213, 6);
            // 
            // mbcliVersionItem
            // 
            this.mbcliVersionItem.Name = "mbcliVersionItem";
            this.mbcliVersionItem.Size = new System.Drawing.Size(216, 22);
            this.mbcliVersionItem.Text = "Версия mbcli.dll";
            this.mbcliVersionItem.Click += new System.EventHandler(this.mbcliVersionToolStripMenuItem_Click);
            // 
            // viewMenuItem
            // 
            this.viewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.errorLogMenuItem,
            this.scudMenuItem,
            this.iptMenuItem});
            this.viewMenuItem.Name = "viewMenuItem";
            this.viewMenuItem.Size = new System.Drawing.Size(39, 20);
            this.viewMenuItem.Text = "Вид";
            this.viewMenuItem.DropDownOpening += new System.EventHandler(this.viewToolStripMenuItem_DropDownOpening);
            // 
            // errorLogMenuItem
            // 
            this.errorLogMenuItem.CheckOnClick = true;
            this.errorLogMenuItem.Image = global::MonitorForms.Properties.Resources.ErrorLog;
            this.errorLogMenuItem.Name = "errorLogMenuItem";
            this.errorLogMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.errorLogMenuItem.Size = new System.Drawing.Size(182, 22);
            this.errorLogMenuItem.Text = "Лог ошибок";
            this.errorLogMenuItem.Click += new System.EventHandler(this.changeVisible_Click);
            // 
            // scudMenuItem
            // 
            this.scudMenuItem.CheckOnClick = true;
            this.scudMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("scudMenuItem.Image")));
            this.scudMenuItem.Name = "scudMenuItem";
            this.scudMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.scudMenuItem.Size = new System.Drawing.Size(182, 22);
            this.scudMenuItem.Text = "СКУД";
            this.scudMenuItem.Click += new System.EventHandler(this.changeVisible_Click);
            // 
            // iptMenuItem
            // 
            this.iptMenuItem.CheckOnClick = true;
            this.iptMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("iptMenuItem.Image")));
            this.iptMenuItem.Name = "iptMenuItem";
            this.iptMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.iptMenuItem.Size = new System.Drawing.Size(182, 22);
            this.iptMenuItem.Text = "ИПТ";
            this.iptMenuItem.Click += new System.EventHandler(this.changeVisible_Click);
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.Name = "settingsMenuItem";
            this.settingsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.settingsMenuItem.Size = new System.Drawing.Size(88, 20);
            this.settingsMenuItem.Text = "Настройки...";
            this.settingsMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 24);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.mainSplitContainer);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.errorLogTextBox);
            this.splitContainer4.Size = new System.Drawing.Size(1159, 673);
            this.splitContainer4.SplitterDistance = 491;
            this.splitContainer4.TabIndex = 6;
            // 
            // errorLogTextBox
            // 
            this.errorLogTextBox.ContextMenuStrip = this.errorLogcontextMenu;
            this.errorLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorLogTextBox.HideSelection = false;
            this.errorLogTextBox.Location = new System.Drawing.Point(0, 0);
            this.errorLogTextBox.Multiline = true;
            this.errorLogTextBox.Name = "errorLogTextBox";
            this.errorLogTextBox.ReadOnly = true;
            this.errorLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.errorLogTextBox.ShortcutsEnabled = false;
            this.errorLogTextBox.Size = new System.Drawing.Size(1159, 178);
            this.errorLogTextBox.TabIndex = 0;
            // 
            // errorLogcontextMenu
            // 
            this.errorLogcontextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearMenuItem,
            this.toolStripSeparator11,
            this.copyMenuItem});
            this.errorLogcontextMenu.Name = "errorLogcontextMenu";
            this.errorLogcontextMenu.Size = new System.Drawing.Size(182, 54);
            this.errorLogcontextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.errorLogcontextMenu_Opening);
            // 
            // clearMenuItem
            // 
            this.clearMenuItem.Image = global::MonitorForms.Properties.Resources.ClearContent;
            this.clearMenuItem.Name = "clearMenuItem";
            this.clearMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.clearMenuItem.Size = new System.Drawing.Size(181, 22);
            this.clearMenuItem.Text = "Очистить";
            this.clearMenuItem.Click += new System.EventHandler(this.clearMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(178, 6);
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
            this.graphChart1.Size = new System.Drawing.Size(934, 220);
            this.graphChart1.TabIndex = 1;
            this.graphChart1.SelectedPointChanged += new System.EventHandler(this.GraphChart1_SelectedPointChanged);
            this.graphChart1.AxisLimitsChanged += new System.EventHandler<GraphMonitor.AxisLimitsChangedEventArgs>(this.graphChart1_AxisLimitsChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 697);
            this.Controls.Add(this.splitContainer4);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graphValuesDataGridView)).EndInit();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel1.PerformLayout();
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.scudGraphSplitContainer.Panel1.ResumeLayout(false);
            this.scudGraphSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scudGraphSplitContainer)).EndInit();
            this.scudGraphSplitContainer.ResumeLayout(false);
            this.dgvIptSplitContainer.Panel1.ResumeLayout(false);
            this.dgvIptSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIptSplitContainer)).EndInit();
            this.dgvIptSplitContainer.ResumeLayout(false);
            this.iptTableLayoutPanel.ResumeLayout(false);
            this.iptTableLayoutPanel.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.errorLogcontextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton normalizeButton;
        private System.Windows.Forms.ToolStripButton startButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton addSeriesButton;
        private System.Windows.Forms.ToolStripButton removeSeriesButton;
        private System.Windows.Forms.DataGridView graphValuesDataGridView;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem serverMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runEmulatorMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mbcliVersionItem;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem startReadingMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.SplitContainer dgvIptSplitContainer;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TextBox errorLogTextBox;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorLogMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scudMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iptMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel iptFreqLabel;
        private System.Windows.Forms.ToolStripComboBox iptFreqComboBox;
        private System.Windows.Forms.ContextMenuStrip errorLogcontextMenu;
        private System.Windows.Forms.ToolStripMenuItem clearMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem copyMenuItem;
        private System.Windows.Forms.TableLayoutPanel iptTableLayoutPanel;
        private System.Windows.Forms.ListBox iptListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer scudGraphSplitContainer;
        private System.Windows.Forms.PropertyGrid scudPropertyGrid;
        private GraphMonitor.GraphChart graphChart1;
    }
}

