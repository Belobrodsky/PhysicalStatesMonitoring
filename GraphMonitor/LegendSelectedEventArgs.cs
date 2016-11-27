using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace GraphMonitor
{
    /// <summary>Аргументы события при выборе легенды</summary>
    internal class LegendSelectedEventArgs : EventArgs
    {
        /// <summary>График, ассоциированный с выбранной легендой</summary>
        public Series SelectedSeries { get; private set; }

        public LegendSelectedEventArgs(Series series)
        {
            SelectedSeries = series;
        }
    }
}