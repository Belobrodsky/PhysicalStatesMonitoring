using System;
using System.Collections.Generic;
using System.Linq;
using GraphMonitor;
using Ipt;

namespace MonitorForms
{
    public partial class MainForm
    {
        #region Свойства

        /// <summary>
        ///     Словарь для параметров, отображаемых на графике. Доступ по имени.
        /// </summary>
        private Dictionary<string, SignalParams> _graphSignals;

        #endregion

        //TODO Отображение данных на графике.
        private void AddToChart(DateTime time)
        {
            var rnd = new Random();
            foreach (var pair in _graphSignals)
            {
                //Берём значение параметра из таблицы
                pair.Value.Value = rnd.NextDouble(pair.Value.Min, pair.Value.Max);
                //pair.Value.Value = _values[pair.Key];
                //На график передаём нормированное.
                double value = pair.Value.Normal;
                //Если это реактивность
                if (pair.Key.Equals(Program.R1)
                    || pair.Key.Equals(Program.R2))
                    //То передаём абсолютное значение.
                    value = pair.Value.Value;
                var mv = new MonitorValue(time, value, pair.Value.Max, pair.Value.Min);
                graphChart1.AddValue(mv, pair.Key);
            }
        }

        //На графике выбрана точка
        private void GraphChart1_SelectedPointChanged(object sender, EventArgs e)
        {
            graphValuesDataGridView.DataSource = graphChart1.MonitorValues.Select(
                mv => new
                      {
                          Имя = mv.Name,
                          Значение = mv.Value,
                          Время = mv.TimeStamp,
                          Макс = mv.Max,
                          Мин = mv.Min,
                          Норм = mv.NValue
                      }).ToList();
        }

        //Кнопка «Нормализовать»
        private void normalizeButton_CheckedChanged(object sender, EventArgs e)
        {
            _normalize = !_normalize;
        }

        //Кнопка «Удалить график»
        private void removeSeriesButton_Click(object sender, EventArgs e)
        {
            graphChart1.RemoveLastSeries();
        }

        //Таймер для анимации данных на графике
        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < graphChart1.Count; i++)
                graphChart1.AddValue(new MonitorValue(DateTime.Now, _rnd.Next(-10, 11), 10, -10), i, _normalize);
        }
    }
}