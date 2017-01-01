using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
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
        private bool _normalize;
        private DataReader _dataReader;
        private DataReader Reader
        {
            get
            {
                if (_dataReader == null)
                {
                    _dataReader = DataReader.GetInstance();
                    _dataReader.ErrorOccured -= _dataReader_ErrorOccured;
                    _dataReader.ErrorOccured += _dataReader_ErrorOccured;
                    _dataReader.DataRead -= _dataReader_DataRead;
                    _dataReader.DataRead += _dataReader_DataRead;
                }
                return _dataReader;
            }
        }

        private DataWriter Writer
        {
            get
            {
                if (Program.Settings.LogFile.IsNullOrEmpty())
                {
                    return null;
                }
                return DataWriter.GetInstance(
                    Program.Settings.LogFile,
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
        }

        private void _dataReader_ErrorOccured(object sender, DataReaderErrorEventArgs e)
        {
            var message = string.Format(
                "{0:T}\tКод ошибки: {1}{3}\t{2}{3}", DateTime.Now, e.ErrorCode, e.ErrorText, Environment.NewLine);
            if (logTextBox.Text.Length + message.Length > logTextBox.MaxLength)
            {
                logTextBox.InvokeEx(() => logTextBox.Clear());
            }
            logTextBox.InvokeEx(() => logTextBox.AppendText(message)); 
        }

        private void addSeriesButton_Click(object sender, EventArgs e)
        {
            graphChart1.AddNewSeries();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.Settings.LogFile.IsNullOrEmpty())
                SelectFile();
            if (Program.Settings.LogFile.IsNullOrEmpty()) return;
            Reader.Connect(Program.Settings.ScudIpAddress, Program.Settings.ScudPort, Program.Settings.IptIpAddress, Program.Settings.IptPort);
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

        private void _dataReader_DataRead(object sender, DataReadEventArgs e)
        {
            if (e.Buffer.Buff != null)
            {
                float[] ar = new float[15];
                Array.Copy(e.Buffer.Buff, ar, ar.Length);
                //Поскольку таймер опроса СКУД и ИПТ работает в отдельном потоке, то
                //вывод данных на форму выполняется с проверкой
                scudListBox.InvokeEx(
                    () => { scudListBox.DataSource = ar; });
            }
            iptListBox.InvokeEx(
                () => { iptListBox.DataSource = e.Ipt4.ToString().Split('\r'); });
            //TODO:Добавить вычисление токов перед записью в файл
            //NOTE:Writer создаётся в потоке формы, а файл пишется в потоке таймера. Выяснить возможные уязвимости
            Writer.WriteData(e.Buffer, e.Ipt4.FCurrent1, e.Ipt4.FCurrent2);
            //Debug.WriteLine(string.Join(", ", e.Buffer.Buff));
        }

        private void removeSeriesButton_Click(object sender, EventArgs e)
        {
            graphChart1.RemoveLastSeries();
        }

        private void runEmulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var p = Process.Start(Program.Settings.EmulPath);
            Closing += (o, args) =>
            {
                if (p != null && !p.HasExited) p.Kill();
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
                Program.Settings.LogFile = dialog.FileName;
            }
        }

        [DebuggerStepThrough]
        private void serverToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            disconnectToolStripMenuItem.Enabled = Reader != null && Reader.ReaderState != ReaderStateEnum.Disconnected;
            startReadingtoolStripMenuItem.Enabled = Reader != null && Reader.ReaderState == ReaderStateEnum.Connected;
            runEmulatorToolStripMenuItem.Enabled = !Program.Settings.EmulPath.IsNullOrEmpty();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog(this);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            startButton.Text = timer1.Enabled ? "Остановить" : "Начать";
        }

        private void startReadingtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reader.Start();
            //Reader.Read();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PerformanceMeter.Start(string.Format("Графиков {0}.", graphChart1.Count));
            for (int i = 0; i < graphChart1.Count; i++)
                graphChart1.AddValue(new MonitorValue(DateTime.Now, _rnd.Next(-10, 11), 10, -10), i, _normalize);
            PerformanceMeter.Stop();
        }

        private void logVisibleButton_Click(object sender, EventArgs e)
        {
            splitContainer4.Panel2Collapsed = !logVisibleButton.Checked;
        }
    }
}