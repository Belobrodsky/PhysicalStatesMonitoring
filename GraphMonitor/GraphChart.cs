using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
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
        /// <summary>Форма с информацией</summary>
        private SelPointInfoForm _infoForm = new SelPointInfoForm();
        /// <summary>Выбранный график</summary>
        public Series SelectedSeries { get; set; }

        private DataPoint _selectedPoint;

        /// <summary>Выбранная точка графика</summary>
        public DataPoint SelectedPoint
        {
            get { return _selectedPoint; }
            set
            {
                _selectedPoint = value;
                if (_selectedPoint == null || _selectedPoint.IsEmpty)
                {
                    if (_infoForm != null) _infoForm.Hide();
                    return;
                }
                //Помещаем курсор в точное положение на графике
                _area.CursorX.SetCursorPosition(_selectedPoint.XValue);
                var val = (MonitorValue)_selectedPoint.Tag;
                //Отображаем информацию курсора
                if (_infoForm == null)
                    _infoForm = new SelPointInfoForm(val, SelectedSeries.Name);
                else
                    _infoForm.SetInfo(val, SelectedSeries.Name);
                _infoForm.Show();
            }
        }

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
                throw new ArgumentException("График с таким именем уже существует");

            var s = chart.Series.Add(name);
            chart.ApplyPaletteColors();
            s.ChartType = SeriesChartType.FastLine;
            s.XValueType = ChartValueType.Time;
            var cbl = new CheckboxLegend(s);
            cbl.LegendSelected += LegendSelected;
            chart.Legends[0].CustomItems.Add(cbl);
        }

        /// <summary>Обработчик события при выборе легенды</summary>
        private void LegendSelected(object sender, LegendSelectedEventArgs e)
        {
            SelectedSeries = e.SelectedSeries;
            AdjustCursorPosition();
        }

        /// <summary>Метод для установки курсора в точное положение на графике</summary>
        private void AdjustCursorPosition()
        {
            if (SelectedSeries == null || !SelectedSeries.Enabled || _area.CursorX.Position.Equals(double.NaN))
            {
                SelectedPoint = null;
                return;
            }
            //Среди точек графика ищем точку максимально близкую к положению курсора
            var pos = SelectedSeries.Points.OrderBy(pt => Math.Abs(pt.XValue - _area.CursorX.Position)).FirstOrDefault();
            //Выбираем эту точку
            SelectedPoint = pos;
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
                },

            };

            //Ось Y
            _area.AxisY.Enabled = AxisEnabled.True;
            _area.AxisY.Name = "Ось значений";
            _area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            _area.AxisY.MajorGrid.LineColor = Color.Gray;
            //Ось X
            _area.AxisX.Name = "Ось времени";
            _area.AxisX.Enabled = AxisEnabled.True;
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
            _area.AxisX.ScaleView.Zoom(_area.AxisX.ScaleView.Position, _range);
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
            if (seriesIndex < 0 || seriesIndex > chart.Series.Count - 1) return;
            GetFrequency();
            //Добавление точки на график
            var index = chart.Series[seriesIndex].Points.AddXY(val.TimeStamp.ToOADate(), normalize ? val.NValue : val.Value);
            //В свойство Tag записываем пришедшее значение.
            chart.Series[seriesIndex].Points[index].Tag = val;
            //Удаление точек позже 10 минут
            var firstValue = DateTime.FromOADate(chart.Series[seriesIndex].Points[0].XValue);
            while ((val.TimeStamp - firstValue).TotalMinutes > MAX_MINUTES_DISPLAY)
            {
                chart.Series[seriesIndex].Points.RemoveAt(0);
                firstValue = DateTime.FromOADate(chart.Series[seriesIndex].Points[0].XValue);
            }
            SetYLimits(normalize);
            _area.AxisX.ScaleView.MinSize = _range;
            _area.AxisX.ScaleView.Zoom(val.TimeStamp.AddSeconds(0.5).ToOADate(), _range);
            //PerformanceMeter.Stop();
        }

        /// <summary>Определение частоты изменения значений</summary>
        private void GetFrequency()
        {
            var time = _stopwatch.Elapsed.TotalSeconds;
            _count++;
            if (!_stopwatch.IsRunning) _stopwatch.Start();
            if (_count < 10) return;
            freqLabel.Text = string.Format("{0:f1} Гц", 1 / (time / _count));
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

        private void chart_MouseDown(object sender, MouseEventArgs e)
        {
            var result = chart.HitTest(e.X, e.Y);
            if (result == null || result.Object == null) return;
            switch (result.ChartElementType)
            {
                case ChartElementType.PlottingArea:
                case ChartElementType.DataPoint:
                case ChartElementType.Gridlines:
                    AdjustCursorPosition();
                    break;
                case ChartElementType.LegendItem:
                    var legend = (CheckboxLegend)result.Object;
                    if (legend == null) return;
                    var item = legend;
                    item.Click(result.SubObject as LegendCell);
                    break;
                case ChartElementType.Axis:
                case ChartElementType.AxisLabelImage:
                case ChartElementType.AxisLabels:
                case ChartElementType.AxisTitle:
                    break;
            }
        }

        /// <summary>Двойной клик по графику</summary>
        private void chart_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var result = chart.HitTest(e.X, e.Y);
            if (result == null || result.Object == null) return;
            switch (result.ChartElementType)
            {
                //Клик на оси
                case ChartElementType.Axis:
                case ChartElementType.AxisLabelImage:
                case ChartElementType.AxisLabels:
                case ChartElementType.AxisTitle:
                    if (result.Axis.AxisName == AxisName.X) return;
                    var maxMinForm = new SetMaxMinForm(result.Axis.Maximum, result.Axis.Minimum, result.Axis.Title, PointToScreen(e.Location));
                    maxMinForm.FormClosing += (s, args) =>
                    {
                        result.Axis.Maximum = maxMinForm.Max;
                        result.Axis.Minimum = maxMinForm.Min;
                    };
                    maxMinForm.Show(this);
                    break;
                case ChartElementType.ScrollBarThumbTracker:
                    if (SelectedPoint != null)
                        _area.AxisX.ScaleView.Position = SelectedPoint.XValue;
                    break;
            }
        }
    }
}
