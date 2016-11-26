using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GraphMonitor
{
    public partial class GraphChart : UserControl
    {
        /// <summary>Количество минут для хранения данных</summary>
        private const int MAX_MINUTES_DISPLAY = 10;
        /// <summary>Счётчик значений для определения частоты</summary>
        private byte _count;
        /// <summary>Секундомер для определения частоты</summary>
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private ChartArea _area;
        /// <summary>Отображаемый диапазон</summary>
        private double _range;
        private bool _saveState;

        /// <summary>Количество графиков</summary>
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
                chart.Series.SuspendUpdates();
                chart.Legends.Clear();
                chart.Legends.Add(new Legend());
                chart.Series.Clear();
                for (int i = 0; i < value; i++)
                    AddNewSeries((i + 1).ToString());
                chart.Series.ResumeUpdates();
            }
        }

        /// <summary>Добавление графика с указанным именем</summary>
        /// <param name="name">Имя добавляемого графика</param>
        public void AddNewSeries(string name)
        {
            if (chart.Series.FindByName(name) != null)
            {
                throw new ArgumentException("График с таким именем уже существует");
            }
            var s = chart.Series.Add(name);
            chart.ApplyPaletteColors();
            s.ChartType = SeriesChartType.FastLine;
            s.XValueType = ChartValueType.Time;
            chart.Legends[0].CustomItems.Add(new CheckboxLegend(s));
        }

        /// <summary>Добавление графика с именем по умолчанию</summary>
        public void AddNewSeries()
        {
            AddNewSeries((chart.Series.Count + 1).ToString());
        }

        /// <summary>Удаление последнего графика</summary>
        public void RemoveLastSeries()
        {
            if (chart.Series.Count == 0) return;
            chart.Series.RemoveAt(chart.Series.Count - 1);
            chart.Legends[0].CustomItems.RemoveAt(chart.Legends[0].CustomItems.Count - 1);
        }

        public GraphChart()
        {
            InitializeComponent();
            InitChart();
            _range = (double)rangeNumericUpDown.Value;
        }

        /// <summary>Инициализация графика</summary>
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
            _area.AxisX.ScaleView.SmallScrollSize = 0.2;
            _area.AxisX.ScaleView.MinSizeType = DateTimeIntervalType.Seconds;

            chart.ChartAreas.Add(_area);
        }

        private void rangeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _range = (double)rangeNumericUpDown.Value;
            _area.AxisX.ScaleView.MinSize = _range;
            _area.AxisX.ScaleView.Zoom(_area.AxisX.ScaleView.Position, _range, _area.AxisX.ScaleView.MinSizeType, _saveState);
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
            chart.Series[seriesIndex].Points.AddXY(val.TimeStamp.ToOADate(), normalize ? val.NValue : val.Value);
            //Удаление точек позже 10 минут
            var firstValue = DateTime.FromOADate(chart.Series[seriesIndex].Points[0].XValue);
            while ((val.TimeStamp - firstValue).TotalMinutes > MAX_MINUTES_DISPLAY)
            {
                chart.Series[seriesIndex].Points.RemoveAt(0);
                firstValue = DateTime.FromOADate(chart.Series[seriesIndex].Points[0].XValue);
            }
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
            freqLabel.Text = string.Format("{0:f2} Гц", (double)_count / (_stopwatch.ElapsedMilliseconds / 1000));
            _count = 0;
            _stopwatch.Restart();
        }

        /// <summary>Пересчёт пределов по оси Y</summary>
        /// <param name="normalize">Нормализовать шкалу</param>
        private void SetYLimits(bool normalize)
        {
            if (normalize)
            {
                _area.AxisY.Minimum = 0;
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
            if (e.ButtonType == ScrollBarButtonType.ZoomReset)
            {
                e.IsHandled = true;
                _saveState = false;
                return;
            }
            _saveState = true;
        }

        private void chart_MouseDown(object sender, MouseEventArgs e)
        {
            var result = chart.HitTest(e.X, e.Y);
            if (result == null || result.Object == null) return;
            var legend = result.Object as CheckboxLegend;
            if (legend == null) return;
            var item = legend;
            item.Click();
        }
    }
}
