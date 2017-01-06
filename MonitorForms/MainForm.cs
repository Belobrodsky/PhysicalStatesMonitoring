using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using GraphMonitor;
using Ipt;

//TODO: Чтение и сохранение настроек приложения: адрес, номер порта и т.п.

namespace MonitorForms
{
    public partial class MainForm : Form
    {
        #region Свойства

        //Генератор случайных чисел для эмуляции графика
        private readonly Random _rnd = new Random(DateTime.Now.Millisecond);
        private DataReader _dataReader;
        private DataWriter _dataWriter;

        private List<int> _freqs = new List<int>(new[] {1, 10, 20, 30, 40});
        private bool _normalize;

        //DataReader для чтения данных с устройств
        private DataReader Reader
        {
            get
            {
                if (_dataReader == null)
                {
                    _dataReader = DataReader.GetInstance();
                    _dataReader.IptInterval = 1000d / _freqs[Program.Settings.IptFreqIndex];
                    _dataReader.Error -= _dataReader_Error;
                    _dataReader.Error += _dataReader_Error;
                    _dataReader.IptDataRead -= _dataReader_IptDataRead;
                    _dataReader.IptDataRead += _dataReader_IptDataRead;
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
                           Program.Settings.LogFile,
                           new[]
                           {
                               "Время", "J1", "J2", "R1", "R2", "Rc", "P_CORE", "T_COLD", "T_HOT", "P_SG", "H_12",
                               "H_11", "H_10", "L_pres",
                               "L_sg", "C_bor", "C_bor_f", "F_makeup", "N_akz", "N_tg", "AO"
                           }));
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

        private void _dataReader_IptDataRead(object sender, DataReadEventArgs e)
        {
            KksValues values = null;
            if (e.Buffer.Buff != null)
            {
                values = new KksValues(e.Buffer, Program.Settings.Kks);
                //Поскольку таймер опроса СКУД и ИПТ работает в отдельном потоке, то
                //вывод данных на форму выполняется с проверкой
                if (Program.Settings.ScudListVisible)
                {
                    scudValuesGrid.InvokeEx(
                        () =>
                        {
                            scudValuesGrid.SelectedObject = values;
                        });
                }
            }
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
            //TODO:Добавить вычисление токов перед записью в файл
            //NOTE:Writer создаётся в потоке формы, а файл пишется в потоке таймера. Выяснить возможные уязвимости
            //Для записи передавать KksValues
            Writer.WriteData(values, e.Ipt4.FCurrent1, e.Ipt4.FCurrent2);
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

        private void errorLogcontextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            clearMenuItem.Enabled = errorLogTextBox.Lines.Length > 0;
            copyMenuItem.Enabled = errorLogTextBox.SelectionLength > 0;
        }

        //На графике выбрана точка
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

        //Смена частоты
        private void iptFreqComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dataReader != null)
            {
                _dataReader.IptInterval = 1000d / _freqs[iptFreqComboBox.SelectedIndex];
            }
            Program.Settings.IptFreqIndex = iptFreqComboBox.SelectedIndex;
        }

        //Загрузка формы
        private void MainForm_Load(object sender, EventArgs e)
        {
            graphChart1.Count = 2;
            normalizeButton.Checked = _normalize;
            dataGridView1.AutoGenerateColumns = true;
            //scudValuesGrid= "E7";
            UpdateView();
        }

        //Меню «Версия библиотеки»
        private void mbcliVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                MbCliWrapper.GetReleaseInfo().ToString(CultureInfo.InvariantCulture), "Версия библиотеки",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Кнопка «Нормализовать»
        private void normalizeButton_CheckedChanged(object sender, EventArgs e)
        {
            _normalize = !_normalize;
        }

        //Кнопка «Удалить график»
        private void removeSeriesButton_Click(object sender, EventArgs e)
        {
            graphChart1.RemoveLastSeries();
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
            new SettingsForm().ShowDialog(this);
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
            Reader.Start();
            //Reader.ReadScud();
            //Reader.ReadIpt();
        }

        //Таймер для анимации данных на графике
        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < graphChart1.Count; i++)
                graphChart1.AddValue(new MonitorValue(DateTime.Now, _rnd.Next(-10, 11), 10, -10), i, _normalize);
        }

        //Обновление вида
        private void UpdateView()
        {
            scudMenuItem.Checked = Program.Settings.ScudListVisible;
            iptMenuItem.Checked = Program.Settings.IptListVisible;
            errorLogMenuItem.Checked = Program.Settings.ErrorLogVisible;

            splitContainer2.Panel2Collapsed = !(Program.Settings.IptListVisible || Program.Settings.ScudListVisible);
            scudIptSplitContainer.Panel1Collapsed = !Program.Settings.ScudListVisible;
            scudIptSplitContainer.Panel2Collapsed = !Program.Settings.IptListVisible;
            splitContainer4.Panel2Collapsed = !Program.Settings.ErrorLogVisible;

            iptFreqComboBox.SelectedIndex = Program.Settings.IptFreqIndex;
        }

        //Открытие меню «Вид»
        private void viewToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            UpdateView();
        }
    }
}