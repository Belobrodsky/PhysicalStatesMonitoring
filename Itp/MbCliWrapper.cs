using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Itp
{
    /// <summary>Класс для упрощения работы с функциями библиотеки mbcli.dll.</summary>
    public static class MbCliWrapper
    {
        /// <summary>Соединение по указанному IP-адресу и порту.</summary>
        /// <param name="addr">Адресс соединения.</param>
        /// <param name="port">Порт.</param>
        /// <returns>Возвращает 0, если соединение удалось и код ошибки в противном случае.</returns>
        //function mb_connect(addr: PChar; outp: Integer): Integer; stdcall; external CWRAP;
        [DllImport("mbcli.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int mb_connect(char[] addr, int port);

        /// <summary>Версия релиза библиотеки mbcli.dll.</summary>
        /// <returns>Возвращает число, указывающее версию релиза.</returns>
        //  function mb_get_release_info(): extended; stdcall; external CWRAP;
        [DllImport("mbcli.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        internal static extern double mb_get_release_info();

        /// <summary>Чтение данных.</summary>
        /// <returns>Возвращает число.</returns>
        //  function mb_get_release_info(): extended; stdcall; external CWRAP;
        //function mb_rd_holding_registers(mb_saddr: Integer; mb_qregs: Integer; p: PChar): Integer; stdcall; external CWRAP;
        [DllImport("mbcli.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        internal static extern int mb_rd_holding_registers(int mb_saddr, int mb_qregs, IntPtr charPtr);

        /// <summary>Получение описания ошибки.</summary>
        /// <returns>Возвращает указатель на строку с описанием ошибки.</returns>
        //function mb_get_error(): PChar; stdcall; external CWRAP;
        [DllImport("mbcli.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mb_get_error();

        /// <summary>Событие при возникновении ошибки в работе с библиотекой mbcli.dll.</summary>
        public static event EventHandler<ItpErrorEventArgs> ErrorOccured;

        /// <summary>Версия релиза библиотеки mbcli.dll.</summary>
        /// <returns>Возвращает число, указывающее версию релиза.</returns>
        public static double GetReleaseInfo()
        {
            return mb_get_release_info();
        }

        /// <summary>Выполнить соединение.</summary>
        /// <param name="ip">IP-адресс.</param>
        /// <param name="port">Номер порта.</param>
        /// <returns>Возвращает результат соединения. 0, если соединение удалось и код ошибки, если соединенеие удалось.
        /// <para>Для получения описания ошибки следует обрабатывать событие <see cref="ErrorOccured"/>.</para>.</returns>
        public static int Connect(IPAddress ip, int port)
        {
            var result = mb_connect(ip.ToString().ToCharArray(), 32);
            if (result != 0)
            {
                OnErrorOccured(result);
            }
            return result;
        }

        /// <summary>Вызов события <see cref="ErrorOccured"/>.</summary>
        /// <param name="errCode">Код ошибки, возвращённый функцией библиотеки.</param>
        private static void OnErrorOccured(int errCode)
        {
            var result = mb_get_error();
            string intMessage = Marshal.PtrToStringAnsi(result);
            //Здесь не понятно, кто отвечает за удаление указателя на массив.
            //Marshal.ZeroFreeGlobalAllocAnsi(result);
            ErrorOccured?.Invoke(null, new ItpErrorEventArgs(errCode, intMessage));
        }
    }
}
