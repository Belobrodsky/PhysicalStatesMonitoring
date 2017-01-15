using System;
using System.Collections.Generic;
using System.Linq;
using GraphMonitor;
using Ipt;

namespace MonitorForms
{
    public partial class MainForm
    {
        #region ��������

        /// <summary>
        ///     ������� ��� ����������, ������������ �� �������. ������ �� �����.
        /// </summary>
        private Dictionary<string, SignalParams> _graphSignals;

        #endregion

        //TODO ����������� ������ �� �������.
        private void AddToChart(DateTime time)
        {
            var rnd = new Random();
            foreach (var pair in _graphSignals)
            {
                //���� �������� ��������� �� �������
                pair.Value.Value = rnd.NextDouble(pair.Value.Min, pair.Value.Max);
                //pair.Value.Value = _values[pair.Key];
                //�� ������ ������� �������������.
                double value = pair.Value.Normal;
                //���� ��� ������������
                if (pair.Key.Equals(Program.R1)
                    || pair.Key.Equals(Program.R2))
                    //�� ������� ���������� ��������.
                    value = pair.Value.Value;
                var mv = new MonitorValue(time, value, pair.Value.Max, pair.Value.Min);
                graphChart1.AddValue(mv, pair.Key);
            }
        }

        //�� ������� ������� �����
        private void GraphChart1_SelectedPointChanged(object sender, EventArgs e)
        {
            graphValuesDataGridView.DataSource = graphChart1.MonitorValues.Select(
                mv => new
                      {
                          ��� = mv.Name,
                          �������� = mv.Value,
                          ����� = mv.TimeStamp,
                          ���� = mv.Max,
                          ��� = mv.Min,
                          ���� = mv.NValue
                      }).ToList();
        }

        //������ ���������������
        private void normalizeButton_CheckedChanged(object sender, EventArgs e)
        {
            _normalize = !_normalize;
        }

        //������ �������� ������
        private void removeSeriesButton_Click(object sender, EventArgs e)
        {
            graphChart1.RemoveLastSeries();
        }

        //������ ��� �������� ������ �� �������
        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < graphChart1.Count; i++)
                graphChart1.AddValue(new MonitorValue(DateTime.Now, _rnd.Next(-10, 11), 10, -10), i, _normalize);
        }
    }
}