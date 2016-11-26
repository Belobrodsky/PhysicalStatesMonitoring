using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GraphMonitor;

namespace MonitorForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            graphChart1.Count = 2;
            normalizeButton.Checked = _normalize;
        }

        private readonly Random _rnd = new Random(DateTime.Now.Millisecond);
        private bool _normalize;

        private void timer1_Tick(object sender, EventArgs e)
        {
            PerformanceMeter.Start(String.Format("Графиков {0}.", graphChart1.Count));
            for (int i = 0; i < graphChart1.Count; i++)
                graphChart1.AddValue(new MonitorValue(DateTime.Now, _rnd.Next(-10, 11), 10, -10), i, _normalize);
            PerformanceMeter.Stop();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            startButton.Text = timer1.Enabled ? "Остановить" : "Начать";
        }

        private void normalizeButton_CheckedChanged(object sender, EventArgs e)
        {
            _normalize = !_normalize;
        }

        private void addSeriesButton_Click(object sender, EventArgs e)
        {
            graphChart1.Count++;
        }

        private void removeSeriesButton_Click(object sender, EventArgs e)
        {
            graphChart1.Count--;
        }
    }
}
