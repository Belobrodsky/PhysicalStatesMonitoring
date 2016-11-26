using System;

namespace GraphMonitor
{
    /// <summary>
    /// 
    /// </summary>
    public struct MonitorValue
    {
        public DateTime TimeStamp { get; set; }
        public double Value { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }

        public double NValue
        {
            get { return Math.Abs(Max - Min) < double.Epsilon ? Value : ( Value - Min )/( Max - Min ); }
        }

        public MonitorValue(DateTime timestamp, double value, double max, double min)
            : this()
        {
            TimeStamp = timestamp;
            Value = value;
            Max = max;
            Min = min;
        }
    }
}
