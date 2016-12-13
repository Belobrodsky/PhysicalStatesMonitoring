using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using GraphMonitor;
using Itp;
//TODO: Чтение и сохранение настроек приложения: адрес, номер порта и т.п.
namespace MonitorForms
{
    public partial class MainForm : Form
    {
        private readonly Random _rnd = new Random(DateTime.Now.Millisecond);
        private bool _normalize;
        private int _port;
        private IPAddress _ip;

        public MainForm()
        {
            InitializeComponent();
            graphChart1.Count = 2;
            normalizeButton.Checked = _normalize;
            dataGridView1.AutoGenerateColumns = true;
            graphChart1.SelectedPointChanged += GraphChart1_SelectedPointChanged;

            _ip = IPAddress.Parse("127.0.0.1");
            _port = 1952;

            MbCliWrapper.ErrorOccured += (s, e) =>
            {
                MessageBox.Show(string.Format("Код ошибки: {0}\nСообщение об ошибке: {1}", e.ErrorCode, e.InternalMessage), "Ошибка в mbcli.dll", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
        }

        private void GraphChart1_SelectedPointChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = graphChart1.MonitorValues.Select(mv => new { Время = mv.TimeStamp, Макс = mv.Max, Мин = mv.Min, Норм = mv.NValue, Значение = mv.Value }).ToList();
        }


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
            MessageBox.Show(MbCliWrapper.GetReleaseInfo().ToString(CultureInfo.InvariantCulture), "Версия библиотеки", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            MbCliWrapper.Connect(_ip, _port);
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            using (var sf = _ip == null ? new SettingsForm() : new SettingsForm(_ip, _port))
            {
                if (sf.ShowDialog(this) == DialogResult.Cancel) return;
                _ip = sf.IpAddress;
                _port = sf.Port;
            }
        }
    }
}
