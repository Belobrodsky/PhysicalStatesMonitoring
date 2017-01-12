using System.Xml.Serialization;

namespace Ipt
{
    /// <summary>
    ///     Свойства параметров СКУД
    /// </summary>
    /// <remarks>В Delphi это mysignal</remarks>
    public class ScudSignal : SignalBase
    {
        /// <summary>Номер в регистре модбас</summary>
        [XmlAttribute]
        public int Index { get; set; }

        public ScudSignal(string name, int index)
            : base(name)
        {
            Index = index;
        }

        public ScudSignal()
        {
            
        }
    }
}