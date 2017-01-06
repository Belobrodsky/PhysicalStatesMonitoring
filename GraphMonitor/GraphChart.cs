using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GraphMonitor
{
    [DefaultEvent("SelectedPointChanged")]
    public partial class GraphChart : UserControl
    {
        #region Свойства

        /// <summary>Количество минут для хранения данных.</summary>
        private const int MAX_MINUTES_DISPLAY = 10;

        /// <summary>Секундомер для определения частоты.</summary>
        private readonly Stopwatch _stopwatch = new Stopwatch();

        private ChartArea _area;

        /// <summary>Счётчик значений для определения частоты.</summary>
        private byte _count;

        /// <summary>Форма с информацией.</summary>
        private SelPointInfoForm _infoForm = new SelPointInfoForm();

        /// <summary>Отображаемый диапазон.</summary>
        private double _range;

        private DataPoint _selectedPoint;

        /// <summary>Выбранный график.</summary>
        public Series SelectedSeries { get; set; }

        /// <summary>Выбранная точка графика.</summary>
        [Browsable(false)]
        public DataPoint SelectedPoint
        {
            get { return _selectedPoint; }
            set
            {
                _selectedPoint = value;
                if (_selectedPoint != null)
                {
                    //Помещаем курсор в точное положение на графике
                    _area.CursorX.SetCursorPosition(_selectedPoint.XValue);
                }
                OnSelectedPointChanged();
            }
        }

        /// <summary>Значения графиков под курсором.</summary>
        [Browsable(false)]
        public IEnumerable<MonitorValue> MonitorValues { get; set; }

        /// <summary>Количество графиков.</summary>
        [Description("Количество отображаемых графиков")]
        public int Count
        {
            get { return chart.Series.Count; }
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

        #endregion

        public GraphChart()
        {
            InitializeComponent();
            InitChart();
            _range = (double)rangeNumericUpDown.Value;
        }

        /// <summary>Событие при выборе точки курсором.</summary>
        [Description("Событие при выборе точки курсором")]
        public event EventHandler SelectedPointChanged;

        /// <summary>Добавление графика с указанным именем.</summary>
        /// <param name="name">Имя добавляемого графика.</param>
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

        /// <summary>Добавление графика с именем по умолчанию.</summary>
        public void AddNewSeries()
        {
            AddNewSeries((chart.Series.Count + 1).ToString());
        }

        /// <summary>Добавление значения в заданный график.</summary>
        /// <param name="val">Значение.</param>
        /// <param name="seriesIndex">Номер графика.</param>
        /// <param name="normalize">Нормировать значение.</param>
        public void AddValue(MonitorValue val, int seriesIndex, bool normalize = true)
        {
            if (seriesIndex < 0 || seriesIndex > chart.Series.Count - 1) return;
            GetFrequency();
            //Добавление точки на график
            var index = chart.Series[seriesIndex].Points.AddXY(
                val.TimeStamp.ToOADate(), normalize ? val.NValue : val.Value);
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
        }

        /// <summary>Метод для установки курсора в точное положение на графике.</summary>
        private void AdjustCursorPosition()
        {
            if (_area.CursorX.Position.Equals(double.NaN))
            {
                SelectedPoint = null;
                return;
            }
            var x = _area.CursorX.Position;
            //TODO: Избавиться от try. Правильно обрабатывать случай, когда в графиках нет точек
            try
            {
                //Отбор графиков, пересекаемых курсором
                var series = chart.Series.Where(s => s.Points.First().XValue <= x && s.Points.Last().XValue >= x);
                //из этих графиков выбираем точки, лежащие максимально близко к положению курсора
                var points =
                    series.Select(s => s.Points.OrderBy(pt => Math.Abs(pt.XValue - _area.CursorX.Position)).First())
                          .ToList();
                //
                MonitorValues = points.Select(pt => (MonitorValue)pt.Tag);
                //Точка графика ближайшая к курсору
                SelectedPoint = points.OrderBy(pt => Math.Abs(pt.XValue - _area.CursorX.Position)).First();
            }
            catch
            {
            }
        }

        /// <summary>Двойной клик по графику.</summary>
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
                    var maxMinForm = new SetMaxMinForm(
                        result.Axis.Maximum, result.Axis.Minimum, result.Axis.Title, PointToScreen(e.Location));
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

        /// <summary>Определение частоты изменения значений.</summary>
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

        /// <summary>Инициализация графика.</summary>
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
                //Ось Y
                AxisY =
                        {
                            Enabled = AxisEnabled.True,
                            Name = "Ось значений",
                            MajorGrid =
                            {
                                LineDashStyle = ChartDashStyle.Dash,
                                LineColor = Color.Gray
                            }
                        },
                //Ось X
                AxisX =
                        {
                            Name = "Ось времени",
                            Enabled = AxisEnabled.True,
                            MajorTickMark = {Enabled = false},
                            //Основные линии сетки
                            MajorGrid =
                            {
                                IntervalType = DateTimeIntervalType.Seconds,
                                Interval = 1,
                                LineColor = Color.Gray,
                                LineDashStyle = ChartDashStyle.Dash
                            },
                            //Вспомогательные линии
                            MinorTickMark =
                            {
                                IntervalType = DateTimeIntervalType.Seconds,
                                TickMarkStyle = TickMarkStyle.InsideArea,
                                Enabled = true,
                                Interval = 0.5
                            },
                            //Метки на оси
                            LabelStyle =
                            {
                                IntervalType = DateTimeIntervalType.Seconds,
                                Interval = 1,
                                IntervalOffset = 1,
                                IntervalOffsetType = DateTimeIntervalType.Seconds,
                                Format = "HH:mm:ss"
                            },
                            LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.LabelsAngleStep90,
                            //Укрупнённый вид на график
                            ScaleView =
                            {
                                SmallScrollMinSizeType = DateTimeIntervalType.Seconds,
                                SmallScrollMinSize = 1,
                                SmallScrollSizeType = DateTimeIntervalType.Milliseconds,
                                SmallScrollSize = 0.2,
                                MinSizeType = DateTimeIntervalType.Seconds
                            }
                        }
            };
            chart.ChartAreas.Add(_area);
        }

        /// <summary>Обработчик события при выборе легенды.</summary>
        private void LegendSelected(object sender, LegendSelectedEventArgs e)
        {
            SelectedSeries = e.SelectedSeries;
            AdjustCursorPosition();
        }

        protected virtual void OnSelectedPointChanged()
        {
            var handler = SelectedPointChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void rangeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _range = (double)rangeNumericUpDown.Value;
            _area.AxisX.ScaleView.MinSize = _range;
            _area.AxisX.ScaleView.Zoom(_area.AxisX.ScaleView.Position, _range);
        }

        /// <summary>Удаление последнего графика.</summary>
        public void RemoveLastSeries()
        {
            if (chart.Series.Count == 0) return;
            chart.Series.RemoveAt(chart.Series.Count - 1);
            chart.Legends[0].CustomItems.RemoveAt(chart.Legends[0].CustomItems.Count - 1);
        }

        /// <summary>Пересчёт пределов по оси Y.</summary>
        /// <param name="normalize">Нормализовать шкалу.</param>
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
    }
}