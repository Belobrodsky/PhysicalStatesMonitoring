using System;
using System.Net;
using System.Windows.Forms;

namespace MonitorForms
{
    /// <summary>Форма настроек приложения.</summary>
    public partial class SettingsForm : Form
    {
        /// <summary>Конструктор по умолчанию.</summary>
        /// <remarks>Адрес СКУД 127.0.0.1, порт 1952, адрес ИПТ 127.0.0.1, порт 1952</remarks>
        public SettingsForm()
            : this(IPAddress.Parse("127.0.0.1"), 1952, IPAddress.Parse("127.0.0.1"), 1952)
        {
        }

        /// <summary>IP-адрес СКУД.</summary>
        public IPAddress ScudIpAddress { get; set; }
        /// <summary>Номер порта СКУД.</summary>
        public int ScudPort { get; set; }
        /// <summary>IP-адрес ИПТ.</summary>
        public IPAddress IptIpAddress { get; set; }
        /// <summary>Номер порта ИПТ.</summary>
        public int IptPort { get; set; }

        /// <summary>Конструктор с указанием адреса и номера порта.</summary>
        /// <param name="scudIpAddress">IP-адрес СКУД типа <see cref="IPAddress"/>.</param>
        /// <param name="scudPort">Номер порта СКУД.</param>
        /// <param name="iptIpAddress">IP-адрес ИПТ типа <see cref="IPAddress"/>.</param>
        /// <param name="iptPort">Номер порта ИПТ.</param>
        public SettingsForm(IPAddress scudIpAddress, int scudPort, IPAddress iptIpAddress, int iptPort)
        {
            InitializeComponent();

            scudIpAddressMaskTextBox.ValidatingType = typeof(IPAddress);
            iptIpAddressMaskTextBox.ValidatingType = typeof(IPAddress);

            scudIpAddressMaskTextBox.Text = scudIpAddress.IpToString();
            iptIpAddressMaskTextBox.Text = iptIpAddress.IpToString();

            scudPortNumericUpDown.Value = scudPort;
            iptPortNumericUpDown.Value = scudPort;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            ScudIpAddress = (IPAddress)scudIpAddressMaskTextBox.ValidateText();
            ScudPort = (int)scudPortNumericUpDown.Value;
            IptIpAddress = (IPAddress)iptIpAddressMaskTextBox.ValidateText();
            IptPort = (int)iptPortNumericUpDown.Value;
            Close();
        }

        private void MaskTextBox_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            okButton.Enabled = e.IsValidInput;
        }
    }
}
