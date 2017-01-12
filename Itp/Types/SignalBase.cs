using System.ComponentModel;
using System.Xml.Serialization;

namespace Ipt
{
    /// <summary>
    /// Базовый класс для информации о сигнале.
    /// </summary>
    public class SignalBase
    {
        /// <summary>
        /// Имя сигнала. Может быть именем регистра в СКУД или произвольным другим.
        /// </summary>
        [XmlAttribute]
        [DisplayName("Параметр")]
        public string Name { get; set; }

        public SignalBase(string name)
        {
            Name = name;
        }

        public SignalBase()
        {
        }
    }
}
