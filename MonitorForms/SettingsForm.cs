using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Ipt;
// ReSharper disable AssignNullToNotNullAttribute

namespace MonitorForms
{
    /// <summary>Форма настроек приложения.</summary>
    public partial class SettingsForm : Form
    {
        //TODO Проверка уникальности имён переменных СКУД
        public SettingsForm()
        {
            InitializeComponent();

            signalSettingsDataGridView.AutoGenerateColumns = true;
            signalSettingsDataGridView.ColumnAdded += DgvOnColumnAdded;

            scudSignalDataGridView.AutoGenerateColumns = true;
            scudSignalDataGridView.ColumnAdded += DgvOnColumnAdded;

            ReadSettings();
        }

        private void ReadSettings()
        {
            scudIpEndPoint.Address = Program.Settings.ScudIp;
            scudIpEndPoint.Port = Program.Settings.ScudPort;
            iptIpEndPoint.Address = Program.Settings.IptIp;
            iptIpEndPoint.Port = Program.Settings.IptPort;
            logFilePathSelector.FilePath = Program.Settings.LogFile;
            emulFilePathSelector.FilePath = Program.Settings.EmulPath;

            constArrayEditor.Clear();
            constArrayEditor.Add("Лямбда", Program.Settings.Lambdas);
            constArrayEditor.Add("Альфа", Program.Settings.Alphas);

            scudSignalBindingSource.Clear();
            scudSignalBindingSource.DataSource = typeof(ScudSignal);
            Program.Settings.ScudSignals.ForEach(ss => scudSignalBindingSource.Add(new ScudSignal(ss.Name, ss.Index)));

            signalParamsBindingSource.Clear();
            signalParamsBindingSource.DataSource = typeof(SignalParams);
            Program.Settings.SignalParameters.ForEach(ss => signalParamsBindingSource.Add(new SignalParams(ss.Name, ss.IsActive, ss.Min, ss.Max)));
        }

        private void DgvOnColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (e.Column.DataPropertyName == "Name")
            {
                e.Column.DisplayIndex = 0;
            }
            if (e.Column.ValueType==typeof(float) || e.Column.ValueType==typeof(int))
            {
                e.Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        //Проверка валидности введённого адреса
        private void ipEndPointEditor_IsAddressValidChanged(object sender, EventArgs e)
        {
            okButton.Enabled = ((IpEndPointEditor)sender).IsAddressValid;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            WriteSettings();
            Settings.Save(Program.Settings);
            Close();
        }

        private void WriteSettings()
        {
            Program.Settings.ScudIp = scudIpEndPoint.Address;
            Program.Settings.ScudPort = scudIpEndPoint.Port;
            Program.Settings.IptIp = iptIpEndPoint.Address;
            Program.Settings.IptPort = iptIpEndPoint.Port;
            Program.Settings.LogFile = logFilePathSelector.FilePath;
            Program.Settings.EmulPath = emulFilePathSelector.FilePath;
            Program.Settings.Lambdas = constArrayEditor[0].Cast<double>().ToArray();
            Program.Settings.Alphas = constArrayEditor[1].Cast<double>().ToArray();
            //Имена и индексы переменных СКУД
            Program.Settings.ScudSignals = (scudSignalBindingSource.List as BindingList<ScudSignal>).ToList();
            //Параметры отображения значений
            Program.Settings.SignalParameters = (signalParamsBindingSource.List as BindingList<SignalParams>).ToList();
        }

        private void syncButton_Click(object sender, EventArgs e)
        {
            var par = signalParamsBindingSource.List.Cast<SignalParams>().ToList();
            var scuds = scudSignalBindingSource.List.Cast<SignalBase>().ToList();
            //Добавляем в настройки отображения из списка СКУД отсутствующие параметры
            foreach (var signal in scuds)
            {
                if (par.Count(s => signal.Name.Equals(s.Name)) == 0)
                {
                    signalParamsBindingSource.Add(new SignalParams(signal.Name, true, float.NaN, float.NaN));
                }
            }

            par = signalParamsBindingSource.List.Cast<SignalParams>().ToList();
            //Удаляем из отображения то, чего нет в списке СКУД
            for (var i = par.Count - 1; i >= 0; i--)
            {
                SignalBase item = par[i];//Элемент списка настроек отображения
                if (scuds.Count(s => s.Name.Equals(item.Name)) == 0
                          && !item.Name.Equals(Program.I1)
                          && !item.Name.Equals(Program.I2)
                          && !item.Name.Equals(Program.R1)
                          && !item.Name.Equals(Program.R2)
                          )
                {
                    signalParamsBindingSource.RemoveAt(i);
                }
            }
        }

        private void resetSettingsButton_Click(object sender, EventArgs e)
        {
            Program.Settings.Reset();
            ReadSettings();
        }

        private bool CheckScudIndex(int value)
        {
            if (value < 0)
            {
                return false;
            }
            return scudSignalBindingSource.List.Cast<ScudSignal>().Count(s => s.Index.Equals(value)) <= 1;
        }

        //Переход к редактированию ячейки при добавлении нового элемента
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            scudSignalDataGridView.BeginEdit(true);
        }

        //Проверка, что СКУД-параметр с таким именем не существует.
        private bool CheckScudName(string name)
        {
            if (name.IsNullOrEmpty())
                return false;
            return scudSignalBindingSource.List.Cast<ScudSignal>().Count(s => s.Name.Equals(name)) <= 1;
        }

        private void scudSignalBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            var curr = (ScudSignal) scudSignalBindingSource.Current;
            if (curr == null)
            {
                return;
            }
            var result = CheckScudName(curr.Name) && CheckScudIndex(curr.Index);
            okButton.Enabled = result;
            syncButton.Enabled = result;
        }
    }
}