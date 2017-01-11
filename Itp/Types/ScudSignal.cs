using System.Xml.Serialization;

namespace Ipt
{
    /// <summary>
    ///     Свойства параметров СКУД
    /// </summary>
    /// <remarks>В Delphi это mysignal</remarks>
    public class ScudSignal
    {
        /// <summary>Краткое имя на латинице. Заполняется в формкреате</summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>Номер в регистре модбас</summary>
        [XmlAttribute]
        public int Number { get; set; }
    }
}