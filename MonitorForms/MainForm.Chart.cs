using System;
using System.Collections.Generic;
using GraphMonitor;
using Ipt;

namespace MonitorForms
{
    public partial class MainForm
    {
        /// <summary>
        /// Словарь для параметров, отображаемых на графике. Доступ по имени.
        /// </summary>
        private Dictionary<string, SignalParams> _graphSignals;
        //TODO Отображение данных на графике.
        private void AddToChart(DateTime time)
        {
            var rnd = new Random();
            foreach (var pair in _graphSignals)
            {
                //Берём значение параметра из таблицы
                pair.Value.Value = rnd.Next((int)pair.Value.Min, (int)pair.Value.Max);
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
    }
}