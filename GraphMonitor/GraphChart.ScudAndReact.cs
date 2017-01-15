using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace GraphMonitor
{
    public partial class GraphChart
    {

        public void AddScudSeries(string name)
        {
            AddNewSeries(name);
            chart.Series.FindByName(name).YAxisType = AxisType.Secondary;
        }

        public void AddReactSeries(string name)
        {
            AddNewSeries(name);
            chart.Series.FindByName(name).YAxisType = AxisType.Primary;
        }

        public void SetReactLimits(double min, double max)
        {
            _area.AxisY.Maximum = max;
            _area.AxisY.Minimum = min;
            _area.AxisY.LabelStyle.Format = "f3";
            _area.AxisY.MajorGrid.Interval = Math.Abs(max - min) / 20;
        }
    }
}