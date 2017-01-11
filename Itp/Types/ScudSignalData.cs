using System;
using System.Xml.Serialization;

namespace Ipt
{
    /// <summary>
    /// Данные со СКУД
    /// </summary>
    public class ScudSignalData : ScudSignal
    {
        [XmlAttribute]
        public double Max { get; set; }
        [XmlAttribute]
        public double Min { get; set; }

        [XmlAttribute]
        public double Value { get; set; }
        
        [XmlIgnore]
        public double Rate
        {
            get
            {
                return Max - Min;
            }
        }
        [XmlIgnore]
        public double Normal
        {
            get
            {
                return Math.Abs(Max - Min) < double.Epsilon ? Value : (Value - Min) / (Max - Min);
            }
        }
    }
}