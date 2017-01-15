using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace GraphMonitor
{
    public class AxisLimitsChangedEventArgs:EventArgs
    {
        public Axis ChangedAxis { get; set; }
    }
}