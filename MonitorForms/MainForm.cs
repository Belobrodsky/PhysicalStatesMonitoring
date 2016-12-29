using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using GraphMonitor;
using Ipt;

//TODO: Чтение и сохранение настроек приложения: адрес, номер порта и т.п.

namespace MonitorForms
{
    public partial class MainForm : Form
    {
        #region Свойства

        private readonly Random _rnd = new Random(DateTime.Now.Millisecond);
        private IPAddress _iptIp;
        private int _iptPort;
        private bool _normalize;
        private string _path;
        private IPAddress _scudIp;
        private int _scudPort;

        private DataReader Reader
        {
            get
            {
                return _path == null ? null : DataReader.GetInstance();
            }
        }

        private DataWriter Writer
        {
            get
            {
                if (_path.IsNullOrEmpty())
                    return null;
                return DataWriter.GetInstance(
                    _path,
                    new[]
                    {
                        "Время", "J1", "J2", "R1", "R2", "Rc", "P1k", "Tcold", "Thot", "Ppg", "H10", "H9", "H8", "Lkd",
                        "Lpg", "C", "Cp", "F", "N1", "Ntg", "AO"
                    });
            }
        }

        #endregion

        public MainForm()
        {
            InitializeComponent();
            graphChart1.Count = 2;
            normalizeButton.Checked = _normalize;
            dataGridView1.AutoGenerateColumns = true;
            graphChart1.SelectedPointChanged += GraphChart1_SelectedPointChanged;

            _scudIp = IPAddress.Parse("127.0.0.1");
            _scudPort = 1952;
            _iptIp = IPAddress.Parse("127.0.0.1");
            _iptPort = 1952;
        }

        private void _dataReader_ErrorOccured(object sender, DataReaderErrorEventArgs e)
        {
            MessageBox.Show(
                string.Format("Код ошибки: {0}\n{1}", e.ErrorCode, e.ErrorText), "Ошибка связи", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void addSeriesButton_Click(object sender, EventArgs e)
        {
            graphChart1.AddNewSeries();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_path.IsNullOrEmpty() || !File.Exists(_path))
                SelectFile();
            if (_path.IsNullOrEmpty()) return;
            Reader.Connect(_scudIp, _scudPort, _iptIp, _iptPort);
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reader.Disconnect();
        }

        private void GraphChart1_SelectedPointChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = graphChart1.MonitorValues.Select(
                mv => new
                {
                    Время = mv.TimeStamp,
                    Макс = mv.Max,
                    Мин = mv.Min,
                    Норм = mv.NValue,
                    Значение = mv.Value
                }).ToList();
        }

        private void mbcliVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                MbCliWrapper.GetReleaseInfo().ToString(CultureInfo.InvariantCulture), "Версия библиотеки",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void normalizeButton_CheckedChanged(object sender, EventArgs e)
        {
            _normalize = !_normalize;
        }

        private void OnDataRead(object o, DataReadEventArgs args)
        {
            if (args.Buffer.Buff == null) return;
            float[] ar = new float[15];
            Array.Copy(args.Buffer.Buff, ar, ar.Length);
            //Поскольку таймер опроса СКУД и ИПТ работает в отдельном потоке, то
            //вывод данных на форму выполняется с проверкой
            scudListBox.InvokeEx(
                () => { scudListBox.DataSource = ar; });
            iptListBox.InvokeEx(
                () => { iptListBox.DataSource = args.Ipt4.ToString().Split('\r'); });
            //TODO:Добавить вычисление токов перед записью в файл
            //NOTE:Writer создаётся в потоке формы, а файл пишется в потоке таймера. Выяснить возможные уязвимости
            Writer.WriteData(args.Buffer, args.Ipt4.FCurrent1, args.Ipt4.FCurrent2);
            //Debug.WriteLine(string.Join(", ", args.Buffer.Buff));
        }

        private void removeSeriesButton_Click(object sender, EventArgs e)
        {
            graphChart1.RemoveLastSeries();
        }

        private void runEmulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var p = Process.Start("IptServer.exe");
            Closing += (o, args) =>
            {
                if (p != null) p.Kill();
            };
        }

        private void SelectFile()
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Title = "Выберите файл для записи результата";
                dialog.Filter = "Текстовые файлы|*.txt";
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;
                _path = dialog.FileName;
            }
        }

        [DebuggerStepThrough]
        private void serverToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            disconnectToolStripMenuItem.Enabled = Reader != null && Reader.ReaderState != ReaderStateEnum.Disconnected;
            startReadingtoolStripMenuItem.Enabled = Reader != null && Reader.ReaderState == ReaderStateEnum.Connected;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (
                var sf = _scudIp == null ? new SettingsForm() : new SettingsForm(_scudIp, _scudPort, _iptIp, _iptPort))
            {
                if (sf.ShowDialog(this) == DialogResult.Cancel) return;
                _scudIp = sf.ScudIpAddress;
                _scudPort = sf.ScudPort;
                _iptIp = sf.IptIpAddress;
                _iptPort = sf.IptPort;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            startButton.Text = timer1.Enabled ? "Остановить" : "Начать";
        }

        private void startReadingtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reader.ErrorOccured += _dataReader_ErrorOccured;
            Reader.DataRead += OnDataRead;
            Reader.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PerformanceMeter.Start(string.Format("Графиков {0}.", graphChart1.Count));
            for (int i = 0; i < graphChart1.Count; i++)
                graphChart1.AddValue(new MonitorValue(DateTime.Now, _rnd.Next(-10, 11), 10, -10), i, _normalize);
            PerformanceMeter.Stop();
        }
    }
}