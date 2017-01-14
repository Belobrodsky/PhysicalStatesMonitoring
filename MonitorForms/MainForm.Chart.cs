using System;
using System.Collections.Generic;
using GraphMonitor;
using Ipt;

namespace MonitorForms
{
    public partial class MainForm
    {
        /// <summary>
        /// ������� ��� ����������, ������������ �� �������. ������ �� �����.
        /// </summary>
        private Dictionary<string, SignalParams> _graphSignals;
        //TODO ����������� ������ �� �������.
        private void AddToChart(DateTime time)
        {
            var rnd = new Random();
            foreach (var pair in _graphSignals)
            {
                //���� �������� ��������� �� �������
                pair.Value.Value = rnd.Next((int)pair.Value.Min, (int)pair.Value.Max);
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
    }
}