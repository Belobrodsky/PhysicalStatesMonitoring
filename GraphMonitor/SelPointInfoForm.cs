using System.Windows.Forms;

namespace GraphMonitor
{
    /// <summary>Форма для показа информации о выбранной точке графика</summary>
    public sealed partial class SelPointInfoForm : Form
    {
        public SelPointInfoForm()
        {
            InitializeComponent();
        }

        public SelPointInfoForm(MonitorValue val, string title)
            : this()
        {
            SetInfo(val, title);
        }

        /// <summary>
        /// Передача объекта с информацией в форму
        /// </summary>
        /// <param name="val">Значение</param>
        /// <param name="title">Заголовок</param>
        public void SetInfo(MonitorValue val, string title)
        {
            selInfoGrid.SelectedObject = val;
            Text = title;
            WindowState = FormWindowState.Normal;
        }

        /// <summary>Скрытие формы при её закрытии пользователем</summary>
        private void SelPointInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;
            e.Cancel = true;
            Hide();
        }
    }
}
