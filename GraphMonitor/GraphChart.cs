using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GraphMonitor
{
    public partial class GraphChart : UserControl
    {
        //Счётчик значений для определения частоты
        private byte _count;
        //секундомер для определения частоты
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private ChartArea _area;
        //Отображаемы диапазон
        private double _range;
        private bool _saveState;

        /// <summary>
        /// Количество графиков
        /// </summary>
        public int Count
        {
            get
            {
                return chart.Series.Count;
            }
            set
            {
                if (chart.Series.Count == value)
                    return;
                chart.Series.Clear();
                chart.Series.SuspendUpdates();
                for (int i = 0; i < value; i++)
                {
                    var s = chart.Series.Add((i + 1).ToString());
                    s.ChartType = SeriesChartType.FastLine;
                    s.XValueType = ChartValueType.Time;
                }
                chart.Series.ResumeUpdates();
            }
        }


        public GraphChart()
        {
            InitializeComponent();
            InitChart();
            _range = (double)rangeNumericUpDown.Value;
        }

        private void InitChart()
        {
            chart.ChartAreas.Clear();
            chart.Series.Clear();
            _area = new ChartArea
            {
                CursorX =
                {
                    IsUserEnabled = true,
                    IntervalType = DateTimeIntervalType.Milliseconds,
                    Interval = 5
                }
            };
            //_area.CursorY.IsUserEnabled = true;
            //Ось Y
            _area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            _area.AxisY.MajorGrid.LineColor = Color.Gray;
            //Ось X
            //Основные линии сетки
            _area.AxisX.MajorTickMark.Enabled = false;
            _area.AxisX.MajorGrid.IntervalType = DateTimeIntervalType.Seconds;
            _area.AxisX.MajorGrid.Interval = 1;
            _area.AxisX.MajorGrid.LineColor = Color.Gray;
            _area.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            //Вспомогательные линии
            _area.AxisX.MinorTickMark.IntervalType = DateTimeIntervalType.Seconds;
            _area.AxisX.MinorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
            _area.AxisX.MinorTickMark.Enabled = true;
            _area.AxisX.MinorTickMark.Interval = 0.5;
            //Метки на оси
            _area.AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Seconds;
            _area.AxisX.LabelStyle.Interval = 1;
            _area.AxisX.LabelStyle.IntervalOffset = 1;
            _area.AxisX.LabelStyle.IntervalOffsetType = DateTimeIntervalType.Seconds;
            _area.AxisX.LabelStyle.Format = "HH:mm:ss";
            _area.AxisX.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.LabelsAngleStep90;
            //Укрупнённый вид на график
            _area.AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Seconds;
            _area.AxisX.ScaleView.SmallScrollMinSize = 1;
            _area.AxisX.ScaleView.SmallScrollSizeType = DateTimeIntervalType.Milliseconds;
            _area.AxisX.ScaleView.SmallScrollSize = 0.5;
            _area.AxisX.ScaleView.MinSizeType = DateTimeIntervalType.Seconds;
            chart.ChartAreas.Add(_area);
        }

        private void rangeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _range = (double)rangeNumericUpDown.Value;
            //var date = DateTime.FromOADate(chart.ChartAreas[0].AxisX.ScaleView.Position);
            //_area.AxisX.ScaleView.Zoom(date.AddSeconds(-_range / 2).ToOADate(), date.AddSeconds(_range / 2).ToOADate());
        }

        /// <summary>
        /// Добавление значения в заданный график
        /// </summary>
        /// <param name="val">Значение</param>
        /// <param name="seriesIndex">Номер графика</param>
        /// <param name="normalize">Нормировать значение</param>
        public void AddValue(MonitorValue val, int seriesIndex, bool normalize = true)
        {
            //PerformanceMeter.Start($"Добавление значения. Всего {chart.Series[seriesIndex].Points.Count}.");
            if (seriesIndex < 0 || seriesIndex > chart.Series.Count - 1)
            {
                return;
            }
            GetFrequency();
            chart.Series[seriesIndex].Points.AddXY(val.TimeStamp, normalize ? val.NValue : val.Value);
            SetYLimits(normalize);
            _area.AxisX.ScaleView.MinSize = _range;
            _area.AxisX.ScaleView.Zoom(val.TimeStamp.ToOADate(), _range, _area.AxisX.ScaleView.MinSizeType, _saveState);
            //PerformanceMeter.Stop();
        }

        /// <summary>Определение частоты изменения значений</summary>
        private void GetFrequency()
        {
            _count++;
            if (!_stopwatch.IsRunning) _stopwatch.Start();
            if (_count < 10) return;
            freqLabel.Text = $"{(double)_count / (_stopwatch.ElapsedMilliseconds / 1000):f2} Гц";
            _count = 0;
            _stopwatch.Restart();
        }

        /// <summary>Пересчёт пределов по оси Y</summary>
        /// <param name="normalize">Нормализовать шкалу</param>
        private void SetYLimits(bool normalize)
        {
            if (normalize)
            {
                _area.AxisY.Minimum = -1;
                _area.AxisY.Maximum = 1;
                _area.AxisY.LabelStyle.Format = "p";
            }
            else
            {
                _area.AxisY.Minimum = double.NaN;
                _area.AxisY.Maximum = double.NaN;
                _area.AxisY.LabelStyle.Format = string.Empty;
            }
        }

        private void chart_AxisScrollBarClicked(object sender, ScrollBarEventArgs e)
        {
            if (e.ButtonType ==ScrollBarButtonType.ZoomReset)
            {
                e.IsHandled = true;
                _saveState = false;
                return;
            }
            _saveState = true;
        }
    }
}
