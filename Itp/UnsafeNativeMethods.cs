using System;
using System.Runtime.InteropServices;

namespace Ipt
{
    internal static class UnsafeNativeMethods
    {
        /// <summary>Соединение по указанному IP-адресу и порту.</summary>
        /// <param name="addr">Адресс соединения. Указатель на строку, содержащую адрес.</param>
        /// <param name="port">Порт.</param>
        /// <returns>Возвращает 0, если соединение удалось и код ошибки в противном случае.</returns>
        /// <remarks>Код в делфи: <code>function mb_connect(addr: PChar; outp: Integer): Integer; stdcall; external CWRAP;</code></remarks>
        [DllImport("mbcli.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int mb_connect(IntPtr addr, int port);

        /// <summary>Закрытие соединения.</summary>
        /// <returns>Возвращает число.</returns>
        /// <remarks>Код в делфи: <code>function mb_close(): Integer; stdcall; external CWRAP;</code></remarks>
        [DllImport("mbcli.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        internal static extern int mb_close();

        /// <summary>Версия релиза библиотеки mbcli.dll.</summary>
        /// <returns>Возвращает число, указывающее версию релиза.</returns>
        /// <remarks>Код в делфи: <code>function mb_get_release_info(): extended; stdcall; external CWRAP;</code></remarks>
        [DllImport("mbcli.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        internal static extern double mb_get_release_info();

        /// <summary>Чтение данных.</summary>
        /// <param name="mb_saddr">Номер начальной ячейки.</param>
        /// <param name="mb_qregs">Количество ячеек.</param>
        /// <param name="charPtr">Указатель на структуру данных.</param>
        /// <returns>Возвращает число.</returns>
        /// <remarks>Код в делфи: <code>function mb_rd_holding_registers(mb_saddr: Integer; mb_qregs: Integer; p: PChar): Integer; stdcall; external CWRAP;</code></remarks>
        [DllImport("mbcli.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int mb_rd_holding_registers(int mb_saddr, int mb_qregs, IntPtr charPtr);

        /// <summary>Получение описания ошибки.</summary>
        /// <returns>Возвращает указатель на строку с описанием ошибки.</returns>
        /// <remarks>Код в делфи: <code>function mb_get_error(): PChar; stdcall; external CWRAP;</code></remarks>
        [DllImport("mbcli.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mb_get_error();
    }
}
