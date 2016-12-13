using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using GraphMonitor;
using Itp;

namespace MonitorForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            graphChart1.Count = 2;
            normalizeButton.Checked = _normalize;
            dataGridView1.AutoGenerateColumns = true;
            graphChart1.SelectedPointChanged += GraphChart1_SelectedPointChanged;
            MbCliWrapper.ErrorOccured += (s, e) =>
            {
                MessageBox.Show(string.Format("Код ошибки: {0}\nСообщение об ошибке: {1}", e.ErrorCode, e.InternalMessage), "Ошибка в mbcli.dll", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
        }

        private void GraphChart1_SelectedPointChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = graphChart1.MonitorValues.Select(mv => new { Время = mv.TimeStamp, Макс = mv.Max, Мин = mv.Min, Норм = mv.NValue, Значение = mv.Value }).ToList();
        }

        private readonly Random _rnd = new Random(DateTime.Now.Millisecond);
        private bool _normalize;

        private void timer1_Tick(object sender, EventArgs e)
        {
            PerformanceMeter.Start(string.Format("Графиков {0}.", graphChart1.Count));
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
            graphChart1.AddNewSeries();
        }

        private void removeSeriesButton_Click(object sender, EventArgs e)
        {
            graphChart1.RemoveLastSeries();
        }

        private void mbcliVersionButton_Click(object sender, EventArgs e)
        {
            Text = MbCliWrapper.GetReleaseInfo().ToString();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            MbCliWrapper.Connect(IPAddress.Parse("127.0.0.1"), 0);
        }
    }
}
