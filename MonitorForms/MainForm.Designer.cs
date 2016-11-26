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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.normalizeButton = new System.Windows.Forms.ToolStripButton();
            this.startButton = new System.Windows.Forms.ToolStripButton();
            this.removeSeriesButton = new System.Windows.Forms.ToolStripButton();
            this.addSeriesButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.graphChart1 = new GraphMonitor.GraphChart();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 25;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.graphChart1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(611, 335);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalizeButton,
            this.startButton,
            this.toolStripSeparator1,
            this.addSeriesButton,
            this.removeSeriesButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(611, 27);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // graphChart1
            // 
            this.graphChart1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphChart1.Count = 1;
            this.graphChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphChart1.Location = new System.Drawing.Point(3, 28);
            this.graphChart1.Name = "graphChart1";
            this.graphChart1.Size = new System.Drawing.Size(605, 304);
            this.graphChart1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 335);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GraphMonitor.GraphChart graphChart1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton normalizeButton;
        private System.Windows.Forms.ToolStripButton startButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton addSeriesButton;
        private System.Windows.Forms.ToolStripButton removeSeriesButton;
    }
}

