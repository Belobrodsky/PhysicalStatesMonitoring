namespace Ipt
{
    /// <summary>
    /// Структура для хранения скудовских переменных
    /// </summary>
    /// <remarks>В Delphi это mysignal</remarks>
    public struct ScudSignal
    {
        /// <summary>Краткое имя на латинице. Заполняется в формкреате</summary>
        public string Name { get; set; }
        /// <summary>Номер в регистре модбас</summary>
        public int Number { get; set; }
        /// <summary>То, что 0% на графике</summary>
        public double Min { get; set; }
        /// <summary>То, что 100% на графике</summary>
        public double Max { get; set; }
        /// <summary>Размах значения</summary>
        public double Rate
        {
            get { return Max - Min; }
        }

        /// <summary>Отображать ли на графике</summary>
        public bool Visible { get; set; }
    }
}
