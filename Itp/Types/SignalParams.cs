using System.ComponentModel;
using System.Xml.Serialization;

namespace Ipt
{
    public class SignalParams : SignalBase
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
        public float Max { get; set; }

        /// <summary>
        /// Минимум сигнала.
        /// </summary>
        [XmlAttribute]
        [DisplayName("Мин.")]
        public float Min { get; set; }

        public SignalParams(string name, bool isActive, float min, float max)
                : base(name)
        {
            IsActive = isActive;
            Max = max;
            Min = min;
        }

        public SignalParams()
        {
            
        }

        #region Overrides of SignalBase

        public override string ToString()
        {
            return string.Format("Name = {0}, IsActive = {1}, Max = {2}, Min - {3}", Name, IsActive, Max, Min);
        }

        #endregion
    }
}