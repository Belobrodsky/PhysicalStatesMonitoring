using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphMonitor
{
    /// <summary>
    ///     Форма для установки максимального и минимального значений на осях
    /// </summary>
    public partial class SetMaxMinForm : Form
    {
        #region Свойства

        private Point _location;

        /// <summary>Максимальное значение</summary>
        public double Max { get; set; }

        /// <summary>Минимальное значение</summary>
        public double Min { get; set; }

        #endregion

        public SetMaxMinForm()
        {
            InitializeComponent();
            maxNumericUpDown.Minimum = decimal.MinValue;
            maxNumericUpDown.Maximum = decimal.MaxValue;
            minNumericUpDown.Minimum = decimal.MinValue;
            minNumericUpDown.Maximum = decimal.MaxValue;
        }

        public SetMaxMinForm(double max, double min, string axisName, Point location)
            : this()
        {
            Max = max;
            Min = min;
            if (!double.IsNaN(Max))
                maxNumericUpDown.Value = (decimal) Max;
            if (!double.IsNaN(Min))
                minNumericUpDown.Value = (decimal) Min;
            titleLabel.Text = axisName;
            _location = location;
        }

        private void SetMaxMinForm_Deactivate(object sender, EventArgs e)
        {
            Close();
        }

        private void SetMaxMinForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Max = (double) maxNumericUpDown.Value;
                    Min = (double) minNumericUpDown.Value;
                    Close();
                    break;
            }
        }

        private void SetMaxMinForm_Load(object sender, EventArgs e)
        {
            Top = _location.Y - Height / 2;
            Left = _location.X - Width / 2;
        }
    }
}