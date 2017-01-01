using System;
using System.Net;
using System.Windows.Forms;

namespace MonitorForms
{
    /// <summary>Форма настроек приложения.</summary>
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            scudIpAddressMaskTextBox.ValidatingType = typeof(IPAddress);
            iptIpAddressMaskTextBox.ValidatingType = typeof(IPAddress);

            scudIpAddressMaskTextBox.Text = Program.Settings.ScudIp;
            scudPortNumericUpDown.Value = Program.Settings.ScudPort;
            iptIpAddressMaskTextBox.Text = Program.Settings.IptIp;
            iptPortNumericUpDown.Value = Program.Settings.IptPort;
            logFilePathSelector.FilePath = Program.Settings.LogFile;
            emulFilePathSelector.FilePath = Program.Settings.EmulPath;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Program.Settings.ScudIp = scudIpAddressMaskTextBox.Text;
            Program.Settings.ScudPort = (int)scudPortNumericUpDown.Value;
            Program.Settings.IptIp = iptIpAddressMaskTextBox.Text;
            Program.Settings.IptPort = (int)iptPortNumericUpDown.Value;
            Program.Settings.LogFile = logFilePathSelector.FilePath;
            Program.Settings.EmulPath = emulFilePathSelector.FilePath;
            Close();
        }

        private void MaskTextBox_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            okButton.Enabled = e.IsValidInput;
        }
    }
}
