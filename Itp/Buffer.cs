namespace Itp
{
    /// <summary>
    /// итого 1000 байт, которые размещены на просторах СКУДа начиная с 0 и по 500-ю ячейку памяти (ячейка=2 байтам).
    /// </summary>
    /// <remarks>В Delphi TBuf</remarks>
    public struct Buffer
    {
        /// <summary>
        /// Время со СКУД в сек.
        /// </summary>
        public uint ScudTimeSec { get; set; }
        /// <summary>
        /// Время со СКУД в мсек. Оно нам не нужно но так уж случилось. Я его заменяю на время в сек.
        /// А время в сек. затем меняю на время из компьютера. Поэтому в записанном файле идет в первой
        /// колонке время компьютера, а во второй - время со СКУД.
        /// </summary>
        public uint ScudTimeMsec { get; set; }
        public float[] Buff { get; set; }

        public Buffer(uint scudTimeSec, uint scudTimeMsec)
            : this()
        {
            ScudTimeSec = scudTimeSec;
            ScudTimeMsec = scudTimeMsec;
            Buff = new float[347];
        }
    }
}
