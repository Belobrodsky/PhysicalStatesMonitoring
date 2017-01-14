using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace GraphMonitor
{
    public partial class GraphChart
    {

        public void AddScudSeries(string name)
        {
            AddNewSeries(name);
            chart.Series.FindByName(name).YAxisType = AxisType.Primary;
        }

        public void AddReactSeries(string name)
        {
            AddNewSeries(name);
            chart.Series.FindByName(name).YAxisType = AxisType.Secondary;
        }
    }
}