using System;
using System.Linq;
using System.Windows.Forms;
using Ipt;

namespace MonitorForms
{
    /// <summary>Форма настроек приложения.</summary>
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            objectsInfoDataGridView.AutoGenerateColumns = true;

            scudIpEndPoint.Address = Program.Settings.ScudIp;
            scudIpEndPoint.Port = Program.Settings.ScudPort;
            iptIpEndPoint.Address = Program.Settings.IptIp;
            iptIpEndPoint.Port = Program.Settings.IptPort;
            logFilePathSelector.FilePath = Program.Settings.LogFile;
            emulFilePathSelector.FilePath = Program.Settings.EmulPath;
            propertyGrid1.SelectedObject = Program.Settings.Kks.Clone();
            constArrayEditor.Add("Лямбда", Program.Settings.Lambdas);
            constArrayEditor.Add("Альфа", Program.Settings.Alphas);
            //objectsInfoDataGridView.DataSource = Program.Settings.ScudValues;
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
            Program.Settings.Kks = (Kks)propertyGrid1.SelectedObject;
            Program.Settings.Lambdas = constArrayEditor[0].Cast<double>().ToArray();
            Program.Settings.Alphas = constArrayEditor[1].Cast<double>().ToArray();
            Settings.Save(Program.Settings);
            Close();
        }
    }
}