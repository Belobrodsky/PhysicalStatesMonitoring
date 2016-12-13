using System;
using System.Net;
using System.Windows.Forms;
using Itp;

namespace MonitorForms
{
    /// <summary>Форма настроек приложения.</summary>
    public partial class SettingsForm : Form
    {
        /// <summary>Конструктор по умолчанию.</summary>
        /// <remarks>Адрес 127.0.0.1, порт 1952.</remarks>
        public SettingsForm()
            : this(IPAddress.Parse("127.0.0.1"), 1952)
        {
        }

        /// <summary>IP-адрес.</summary>
        public IPAddress IpAddress { get; set; }
        /// <summary>Номер порта.</summary>
        public int Port { get; set; }

        /// <summary>Конструктор с указанием адреса и номера порта.</summary>
        /// <param name="ipAddress">Адрес типа <see cref="IPAddress"/>.</param>
        /// <param name="port">Номер порта.</param>
        public SettingsForm(IPAddress ipAddress, int port)
        {
            InitializeComponent();

            ipAddressMaskTextBox.ValidatingType = typeof(IPAddress);
            ipAddressMaskTextBox.Text = MbCliWrapper.IpToString(ipAddress);
            portNumericUpDown.Value = port;
        }

        /// <summary>Конструктор с указанием адреса и номера порта.</summary>
        /// <param name="ipAddress">Адрес типа <see cref="string"/>.</param>
        /// <param name="port">Номер порта.</param>
        public SettingsForm(string ipAddress, int port)
            : this(IPAddress.Parse(ipAddress), port)
        {
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            IpAddress = IPAddress.Parse(ipAddressMaskTextBox.Text);
            Port = (int)portNumericUpDown.Value;
            Close();
        }

        private void ipAddressMaskTextBox_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            okButton.Enabled = e.IsValidInput;
        }
    }
}
