using System.ComponentModel;
using System.Xml.Serialization;

namespace Ipt
{
    public class SignalSettings : SignalBase
    {
        /// <summary>
        /// Ипользовать ли данный параметр для отображения и в отчёте.
        /// </summary>
        [XmlAttribute]
        [DisplayName("Использовать")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Максимум сигнала.
        /// </summary>
        [XmlAttribute]
        [DisplayName("Макс.")]
        public double Max { get; set; }

        /// <summary>
        /// Минимум сигнала.
        /// </summary>
        [XmlAttribute]
        [DisplayName("Мин.")]
        public double Min { get; set; }

        public SignalSettings(string name, bool isActive, double min, double max)
                : base(name)
        {
            IsActive = isActive;
            Max = max;
            Min = min;
        }

        public SignalSettings()
        {
            
        }
    }
}