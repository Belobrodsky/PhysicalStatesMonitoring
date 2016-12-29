using System.Runtime.InteropServices;

namespace Ipt
{
    /// <summary>
    /// итого 1000 байт, которые размещены на просторах СКУДа начиная с 0 и по 500-ю ячейку памяти (ячейка=2 байтам).
    /// </summary>
    /// <remarks>В Delphi TBuf</remarks>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct Buffer
    {
        /// <summary>Время со СКУД в сек.</summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint ScudTimeSec;

        /// <summary>
        /// Время со СКУД в мсек. Оно нам не нужно но так уж случилось. Я его заменяю на время в сек.
        /// А время в сек. затем меняю на время из компьютера. Поэтому в записанном файле идет в первой
        /// колонке время компьютера, а во второй - время со СКУД.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint ScudTimeMsec;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 248, ArraySubType = UnmanagedType.R4)]
        public float[] Buff;
    }
}
