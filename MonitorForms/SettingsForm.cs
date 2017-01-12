using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using Ipt;

namespace MonitorForms
{
    /// <summary>Форма настроек приложения.</summary>
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            signalSettingsDataGridView.AutoGenerateColumns = true;
            signalSettingsDataGridView.ColumnAdded += DgvOnColumnAdded;

            scudSignalDataGridView.AutoGenerateColumns = true;
            scudSignalDataGridView.ColumnAdded += DgvOnColumnAdded;

            scudIpEndPoint.Address = Program.Settings.ScudIp;
            scudIpEndPoint.Port = Program.Settings.ScudPort;
            iptIpEndPoint.Address = Program.Settings.IptIp;
            iptIpEndPoint.Port = Program.Settings.IptPort;
            logFilePathSelector.FilePath = Program.Settings.LogFile;
            emulFilePathSelector.FilePath = Program.Settings.EmulPath;

            constArrayEditor.Add("Лямбда", Program.Settings.Lambdas);
            constArrayEditor.Add("Альфа", Program.Settings.Alphas);

            scudSignalBindingSource.DataSource = typeof(ScudSignal);
            Program.Settings.ScudSignals.ForEach(ss => scudSignalBindingSource.Add(new ScudSignal(ss.Name, ss.Index)));

            signalSettingsBindingSource.DataSource = typeof(SignalSettings);
            Program.Settings.SignalSettingses.ForEach(ss => signalSettingsBindingSource.Add(new SignalSettings(ss.Name, ss.IsActive, ss.Min, ss.Max)));
        }

        private void DgvOnColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (e.Column.DataPropertyName == "Name")
            {
                e.Column.DisplayIndex = 0;
            }
        }

        private void ipEndPointEditor_IsAddressValidChanged(object sender, EventArgs e)
        {
            okButton.Enabled = ((IpEndPointEditor)sender).IsAddressValid;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Program.Settings.ScudIp = scudIpEndPoint.Address;
            Program.Settings.ScudPort = scudIpEndPoint.Port;
            Program.Settings.IptIp = iptIpEndPoint.Address;
            Program.Settings.IptPort = iptIpEndPoint.Port;
            Program.Settings.LogFile = logFilePathSelector.FilePath;
            Program.Settings.EmulPath = emulFilePathSelector.FilePath;
            Program.Settings.Lambdas = constArrayEditor[0].Cast<double>().ToArray();
            Program.Settings.Alphas = constArrayEditor[1].Cast<double>().ToArray();
            Program.Settings.ScudSignals = (scudSignalBindingSource.List as BindingList<ScudSignal>).ToList();
            Program.Settings.SignalSettingses = (signalSettingsBindingSource.List as BindingList<SignalSettings>).ToList();
            Settings.Save(Program.Settings);
            Close();
        }

    }
}