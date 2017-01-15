using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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
                    _dataReader.IptInterval = 1000 / _freqs[Program.ProgramSettings.IptFreqIndex];
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
                           Program.ProgramSettings.LogFile));
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
                Program.ProgramSettings.ErrorLogVisible = !Program.ProgramSettings.ErrorLogVisible;
            }
            else if (sender.Equals(scudMenuItem))
            {
                Program.ProgramSettings.ScudListVisible = !Program.ProgramSettings.ScudListVisible;
            }
            else if (sender.Equals(iptMenuItem))
            {
                Program.ProgramSettings.IptListVisible = !Program.ProgramSettings.IptListVisible;
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
            if (Program.ProgramSettings.LogFile.IsNullOrEmpty())
                SelectFile();
            if (Program.ProgramSettings.LogFile.IsNullOrEmpty())
                return;
            Reader.Connect(
                Program.ProgramSettings.ScudIpAddress, Program.ProgramSettings.ScudPort, Program.ProgramSettings.IptIpAddress,
                Program.ProgramSettings.IptPort);
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
            Program.ProgramSettings.IptFreqIndex = iptFreqComboBox.SelectedIndex;
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
            var psi = new ProcessStartInfo(Program.ProgramSettings.EmulPath)
            {
                Arguments = string.Format(
                              "-emul -ip {0} -p {1}",
                              Program.ProgramSettings.IptIp,
                              Program.ProgramSettings.IptPort),
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
                Program.ProgramSettings.LogFile = dialog.FileName;
            }
        }

        //Открытие меню «Сервер»
        [DebuggerStepThrough]
        private void serverToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            disconnectToolStripMenuItem.Enabled = Reader != null && Reader.ReaderState != ReaderStateEnum.Disconnected;
            startReadingMenuItem.Enabled = Reader != null && Reader.ReaderState == ReaderStateEnum.Connected;
            runEmulatorMenuItem.Enabled = !Program.ProgramSettings.EmulPath.IsNullOrEmpty();
            connectMenuItem.Enabled = !Program.ProgramSettings.IptIp.Equals(Program.ProgramSettings.ScudIp)
                                      || !Program.ProgramSettings.IptPort.Equals(Program.ProgramSettings.ScudPort);
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
            Writer.NewFile(Program.ProgramSettings.LogFile);
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
            if (Program.ProgramSettings.IptListVisible)
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
            foreach (var s in Program.ProgramSettings.SignalParameters)
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
            _selectedScud = Program.ProgramSettings.ScudSignals.Where(s => Program.ProgramSettings.SignalParameters.Any(ss => s.Name == ss.Name && ss.IsActive)).ToDictionary(s => s.Name);

            //Добавляем графики СКУД
            graphChart1.Count = 0;
            foreach (var signal in _selectedScud)
            {
                graphChart1.AddScudSeries(signal.Key);
            }
            //Добавление графиков реактивностей, если они отмечены
            var reacts = Program.ProgramSettings.SignalParameters.Where(sp => sp.Name.Equals(Program.R1) || sp.Name.Equals(Program.R2)).ToList();
            foreach (var sp in reacts)
            {
                graphChart1.AddReactSeries(sp.Name);
            }
            //Значения, отображаемые на графике в словарь, чтобы обращаться по имени.
            _graphSignals = Program.ProgramSettings.SignalParameters.Where(sp => sp.IsActive && !sp.Name.Equals(Program.I1) && !sp.Name.Equals(Program.I2)).ToDictionary(sp => sp.Name);
            //NOTE Пределы устанавливаются только по первой реактивности.
            if (_graphSignals.ContainsKey(Program.R1))
            {
                graphChart1.SetReactLimits(_graphSignals[Program.R1].Min, _graphSignals[Program.R1].Max);
            }
        }

        //Обновление вида
        private void UpdateView()
        {
            scudMenuItem.Checked = Program.ProgramSettings.ScudListVisible;
            iptMenuItem.Checked = Program.ProgramSettings.IptListVisible;
            errorLogMenuItem.Checked = Program.ProgramSettings.ErrorLogVisible;

            dgvIptSplitContainer.Panel2Collapsed = !(Program.ProgramSettings.IptListVisible || Program.ProgramSettings.ScudListVisible);
            dgvIptSplitContainer.Panel2Collapsed = !Program.ProgramSettings.IptListVisible;
            splitContainer4.Panel2Collapsed = !Program.ProgramSettings.ErrorLogVisible;
            scudGraphSplitContainer.Panel1Collapsed = !Program.ProgramSettings.ScudListVisible;
            iptFreqComboBox.SelectedIndex = Program.ProgramSettings.IptFreqIndex;
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
            _current.SearchReactivity(Program.ProgramSettings.Lambdas, Program.ProgramSettings.Alphas, args.Buffer, args.Ipt4);
            Writer.WriteData(args.Buffer.Buff, _current.Tok1New, _current.Tok2New, _current.Reactivity1, _current.Reactivity1, _current.ReactivityAverage);

            #endregion

            //Передача значений на график
            graphChart1.InvokeEx(() => AddToChart(_current.TimeOld));

        }

        private void graphValuesDataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (e.Column.ValueType == typeof(DateTime))
            {
                e.Column.DefaultCellStyle.Format = "HH:mm:ss.fff";
            }
        }

        private void graphChart1_AxisLimitsChanged(object sender, AxisLimitsChangedEventArgs e)
        {
            if (e.ChangedAxis.AxisName == AxisName.Y)
            {
                foreach (var sp in Program.ProgramSettings.SignalParameters.Where(sp => sp.Name == Program.R1 || sp.Name == Program.R2))
                {
                    sp.Max = (float) e.ChangedAxis.Maximum;
                    sp.Min = (float) e.ChangedAxis.Minimum;
                }
            }
        }
    }
}