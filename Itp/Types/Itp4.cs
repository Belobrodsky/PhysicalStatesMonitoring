namespace Itp
{
    /// <summary>Структура для хранения данных с ИПТ-4</summary>
    public struct Itp4
    {
        public byte MagistralFailure { get; set; }
        public int FCurrent1 { get; set; }
        public int Freactivity1 { get; set; }
        public byte Power1 { get; set; }
        public byte Filter1 { get; set; }
        public byte Lcounter1 { get; set; }
        public byte Status1 { get; set; }
        public byte GotHVP1 { get; set; }
        public byte GotHVN1 { get; set; }

        public int FCurrent2 { get; set; }
        public int Freactivity2 { get; set; }
        public byte Power2 { get; set; }
        public byte Filter2 { get; set; }
        public byte Lcounter2 { get; set; }
        public byte Status2 { get; set; }
        public byte GotHVP2 { get; set; }
        public byte GotHVN2 { get; set; }

        public int FCurrent3 { get; set; }
        public int Freactivity3 { get; set; }
        public byte Power3 { get; set; }
        public byte Filter3 { get; set; }
        public byte Lcounter3 { get; set; }
        public byte Status3 { get; set; }
        public byte GotHVP3 { get; set; }
        public byte GotHVN3 { get; set; }
    }
}
