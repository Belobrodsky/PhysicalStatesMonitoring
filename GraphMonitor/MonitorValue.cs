using System;
using System.ComponentModel;

namespace GraphMonitor
{
    /// <summary>
    /// 
    /// </summary>
    public struct MonitorValue
    {
        /// <summary>Время получения данных</summary>
        [DisplayName("Время"), ReadOnly(true)]
        public DateTime TimeStamp { get; set; }
        /// <summary>Величина</summary>
        [DisplayName("Значение"), ReadOnly(true)]
        public double Value { get; set; }
        /// <summary>Максимально возможное значение</summary>
        [DisplayName("Максимум"), ReadOnly(true)]
        public double Max { get; set; }
        /// <summary>Минимально возможное значение</summary>
        [DisplayName("Минимум"), ReadOnly(true)]
        public double Min { get; set; }
        /// <summary>Нормированное значение</summary>
        [DisplayName("Нормированное значение"), ReadOnly(true)]
        public double NValue
        {
            get { return Math.Abs(Max - Min) < double.Epsilon ? Value : (Value - Min) / (Max - Min); }
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
