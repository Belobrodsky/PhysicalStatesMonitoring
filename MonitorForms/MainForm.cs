using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using GraphMonitor;
using Ipt;

//TODO: Чтение и сохранение настроек приложения: адрес, номер порта и т.п.

namespace MonitorForms
{
    public partial class MainForm : Form
    {
        #region Свойства

        private readonly Current _current;
        //Частоты опроса ИПТ в Гц
        private readonly List<int> _freqs = new List<int>(new[] { 1, 10, 20, 30, 40 });

        //Генератор случайных чисел для эмуляции графика
        private readonly Random _rnd = new Random(DateTime.Now.Millisecond);
        //Эти данные отображаются в таблице слева от графика
        private readonly DictionaryPropertyAdapter<string, double> _values;
        private DataReader _dataReader;
        private DataWriter _dataWriter;
        private bool _normalize;
        //Вспомогательный список, чтобы отбирать выбранные значения из настроек
        private Dictionary<string, ScudSignal> _selectedScud;

        //DataReader для чтения данных с устройств
        private DataReader Reader
        {
            get
            {
                if (_dataReader == null)
                {
                    _dataReader = DataReader.GetInstance();
                    _dataReader.IptInterval = 1000 / _freqs[Program.Settings.IptFreqIndex];
                    _dataReader.Error -= _dataReader_Error;
                    _dataReader.Error += _dataReader_Error;
                    _dataReader.IptDataRead -= _dataReader_IptDataRead;
                    _dataReader.IptDataRead += _dataReader_IptDataRead;
                    _dataReader.ScudDataRead -= _dataReader_ScudDataRead;
                    _dataReader.ScudDataRead += _dataReader_ScudDataRead;
                }
                return _dataReader;
            }
        }

        //DataWriter для записи данных в файл
        private DataWriter Writer
        {
            get
            {
                return _dataWriter ?? (_dataWriter = DataWriter.GetInstance(
                           Program.Settings.LogFile));
            }
        }

        protected override bool DoubleBuffered
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        #endregion

        public MainForm()
        {
            InitializeComponent();
            _current = new Current();
            _values = new DictionaryPropertyAdapter<string, double>();
        }

        //Ошибка при чтении данных
        private void _dataReader_Error(object sender, DataReaderErrorEventArgs e)
        {
            var message = string.Format(
                "{0:T}\tКод ошибки: {1}{3}\t{2}{3}", DateTime.Now, e.ErrorCode, e.ErrorText, Environment.NewLine);
            if (errorLogTextBox.Text.Length + message.Length > errorLogTextBox.MaxLength)
            {
                errorLogTextBox.InvokeEx(() => errorLogTextBox.Clear());
            }
            errorLogTextBox.InvokeEx(() => errorLogTextBox.AppendText(message));
        }

        //При поступлении новых данных
        private void _dataReader_IptDataRead(object sender, DataReadEventArgs e)
        {
            var writeThread = new Thread(WriteData);
            writeThread.Start(e);
            //Обновляем значения выбранных переменных СКУД
        }

        private void _dataReader_ScudDataRead(object sender, DataReadEventArgs e)
        {
            UpdateUi(e);
        }

        //Добавить график
        private void addSeriesButton_Click(object sender, EventArgs e)
        {
            graphChart1.AddNewSeries();
        }

        //Изменение видимости панелей
        private void changeVisible_Click(object sender, EventArgs e)
        {
            if (sender.Equals(errorLogMenuItem))
            {
                Program.Settings.ErrorLogVisible = !Program.Settings.ErrorLogVisible;
            }
            else if (sender.Equals(scudMenuItem))
            {
                Program.Settings.ScudListVisible = !Program.Settings.ScudListVisible;
            }
            else if (sender.Equals(iptMenuItem))
            {
                Program.Settings.IptListVisible = !Program.Settings.IptListVisible;
            }
            UpdateView();
        }

        //Очистка лога ошибок
        private void clearMenuItem_Click(object sender, EventArgs e)
        {
            errorLogTextBox.Clear();
        }

        //Меню «Подключиться»
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.Settings.LogFile.IsNullOrEmpty())
                SelectFile();
            if (Program.Settings.LogFile.IsNullOrEmpty())
                return;
            Reader.Connect(
                Program.Settings.ScudIpAddress, Program.Settings.ScudPort, Program.Settings.IptIpAddress,
                Program.Settings.IptPort);
        }

        //Копирование в буфер из окна ошибок
        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(errorLogTextBox.SelectedText);
        }

        //Меню «Отключиться»
        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reader.Disconnect();
        }

        //Контекстное меню лога ошибок
        private void errorLogcontextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            clearMenuItem.Enabled = errorLogTextBox.Lines.Length > 0;
            copyMenuItem.Enabled = errorLogTextBox.SelectionLength > 0;
        }

        //Смена частоты
        private void iptFreqComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dataReader != null)
            {
                _dataReader.IptInterval = 1000 / _freqs[iptFreqComboBox.SelectedIndex];
            }
            Program.Settings.IptFreqIndex = iptFreqComboBox.SelectedIndex;
        }

        //Загрузка формы
        private void MainForm_Load(object sender, EventArgs e)
        {
            graphValuesDataGridView.AutoGenerateColumns = true;

            graphChart1.Count = 2;
            normalizeButton.Checked = _normalize;
            UpdateValuesSet();
            UpdateView();
        }

        //Меню «Версия библиотеки»
        private void mbcliVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                MbCliWrapper.GetReleaseInfo().ToString(CultureInfo.InvariantCulture), "Версия библиотеки",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Меню «Запустить эмулятор»
        private void runEmulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var psi = new ProcessStartInfo(Program.Settings.EmulPath)
            {
                Arguments = string.Format(
                              "-emul -ip {0} -p {1}",
                              Program.Settings.IptIp,
                              Program.Settings.IptPort),
                WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
            };
            var p = Process.Start(psi);
            Closing += (o, args) =>
            {
                if (p != null
                    && !p.HasExited)
                    p.Kill();
            };
        }

        //Диалог выбора файла
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

        //Открытие меню «Сервер»
        [DebuggerStepThrough]
        private void serverToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            disconnectToolStripMenuItem.Enabled = Reader != null && Reader.ReaderState != ReaderStateEnum.Disconnected;
            startReadingMenuItem.Enabled = Reader != null && Reader.ReaderState == ReaderStateEnum.Connected;
            runEmulatorMenuItem.Enabled = !Program.Settings.EmulPath.IsNullOrEmpty();
            connectMenuItem.Enabled = !Program.Settings.IptIp.Equals(Program.Settings.ScudIp)
                                      || !Program.Settings.IptPort.Equals(Program.Settings.ScudPort);
        }

        //Меню настроек
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new SettingsForm().ShowDialog(this) != DialogResult.OK)
                return;
            UpdateValuesSet();
        }

        //Кнопка «Остановить/Начать» показ графика
        private void startButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            startButton.Text = timer1.Enabled ? "Остановить" : "Начать";
        }

        //Меню «Начать чтение данных»
        private void startReadingtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Создание нового файла.
            Writer.NewFile(Program.Settings.LogFile);
            Reader.Start();
        }

        //Обновление интерфейса
        private void UpdateUi(DataReadEventArgs e)
        {
            foreach (var signal in _selectedScud)
            {
                var val = e.Buffer.Buff[signal.Value.Index];
                _values[signal.Key] = val;
            }
            _values[Program.I1] = _current.Tok1New;
            _values[Program.I2] = _current.Tok2New;
            _values[Program.R1] = _current.Reactivity1;
            _values[Program.R2] = _current.Reactivity2;

            //Обновление списка ИПТ
            if (Program.Settings.IptListVisible)
            {
                iptListBox.InvokeEx(
                    () =>
                    {
                        iptListBox.BeginUpdate();
                        iptListBox.DataSource = e.Ipt4.ToString().Split('\r');
                        iptListBox.EndUpdate();
                    });
            }
            //Обновляем таблицу 30 мс
            scudPropertyGrid.InvokeEx(() =>
            {
                scudPropertyGrid.Refresh();
            });
        }

        //Обновление отображаемых значений.
        private void UpdateValuesSet()
        {
            _values.Clear();
            //Отображаемые значения. Передаём их в словарь _values
            foreach (var s in Program.Settings.SignalParameters)
            {
                if (s.IsActive)
                {
                    _values.Add(s.Name, s.Value);
                }
            }
            //Токи добавляем вручную, т.к. на графике они не отображаются.
            _values.Add(Program.I1, 0.0);
            _values.Add(Program.I2, 0.0);
            scudPropertyGrid.SelectedObject = _values;

            //Выбранные значения СКУД, которые будут отображаться на графике и в таблице
            _selectedScud = Program.Settings.ScudSignals.Where(s => Program.Settings.SignalParameters.Any(ss => s.Name == ss.Name && ss.IsActive)).ToDictionary(s => s.Name);

            //Добавляем графики СКУД
            graphChart1.Count = 0;
            foreach (var signal in _selectedScud)
            {
                graphChart1.AddScudSeries(signal.Key);
            }
            //Добавление графиков реактивностей, если они отмечены
            var reacts = Program.Settings.SignalParameters.Where(sp => sp.Name.Equals(Program.R1) || sp.Name.Equals(Program.R2)).ToList();
            foreach (var sp in reacts)
            {
                graphChart1.AddReactSeries(sp.Name);
            }
            //Значения, отображаемые на графике в словарь, чтобы обращаться по имени.
            _graphSignals = Program.Settings.SignalParameters.Where(sp => sp.IsActive && !sp.Name.Equals(Program.I1) && !sp.Name.Equals(Program.I2)).ToDictionary(sp => sp.Name);
            //NOTE Пределы устанавливаются только по первой реактивности.
            graphChart1.SetReactLimits(_graphSignals[Program.R1].Min, _graphSignals[Program.R1].Max);
        }

        //Обновление вида
        private void UpdateView()
        {
            scudMenuItem.Checked = Program.Settings.ScudListVisible;
            iptMenuItem.Checked = Program.Settings.IptListVisible;
            errorLogMenuItem.Checked = Program.Settings.ErrorLogVisible;

            dgvIptSplitContainer.Panel2Collapsed = !(Program.Settings.IptListVisible || Program.Settings.ScudListVisible);
            dgvIptSplitContainer.Panel2Collapsed = !Program.Settings.IptListVisible;
            splitContainer4.Panel2Collapsed = !Program.Settings.ErrorLogVisible;

            iptFreqComboBox.SelectedIndex = Program.Settings.IptFreqIndex;
        }

        //Открытие меню «Вид»
        private void viewToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            UpdateView();
        }

        //Запись в файл. Выполняется в отдельном потоке.
        private void WriteData(object o)
        {
            var args = (DataReadEventArgs)o;

            #region Вычисления и запись в файл 0.7 — 1 мс

            //Вычисление значений токов и реактивностей.
            _current.SearchReactivity(Program.Settings.Lambdas, Program.Settings.Alphas, args.Buffer, args.Ipt4);
            Writer.WriteData(args.Buffer.Buff, _current.Tok1New, _current.Tok2New, _current.Reactivity1, _current.Reactivity1, _current.ReactivityAverage);

            #endregion

            //Передача значений на график
            graphChart1.InvokeEx(() => AddToChart(_current.TimeOld));

        }
    }
}