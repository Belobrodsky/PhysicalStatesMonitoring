using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;

namespace MonitorForms
{
    [Description("Контрол для редактирования ip-адреса и порта")]
    [DefaultEvent("IsAddressValidChanged")]
    public partial class IpEndPointEditor : UserControl
    {
        #region Свойства

        [Description("Адрес.")]
        public string Address
        {
            get
            {
                return ipAddressMaskTextBox.Text;
            }
            set
            {
                ipAddressMaskTextBox.Text = value;
            }
        }

        [Description("Номер порта.")]
        public int Port
        {
            get
            {
                return (int) portNumericUpDown.Value;
            }
            set
            {
                portNumericUpDown.Value = value;
            }
        }

        [Description("Является ли введённый адрес валидным.")]
        [Browsable(false)]
        public bool IsAddressValid { get; private set; }

        #endregion

        public IpEndPointEditor()
        {
            InitializeComponent();
            ipAddressMaskTextBox.ValidatingType = typeof(IPAddress);
        }

        [Description("Событие при смене валидности адреса.")]
        public event EventHandler IsAddressValidChanged;

        private void ipAddressMaskTextBox_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            IsAddressValid = e.IsValidInput;
            OnIsAddressValidChanged();
        }

        protected virtual void OnIsAddressValidChanged()
        {
            var handler = IsAddressValidChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}