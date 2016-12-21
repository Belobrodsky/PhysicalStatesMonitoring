using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace Itp
{
    /// <summary>Класс для упрощения работы с функциями библиотеки mbcli.dll.</summary>
    public static class MbCliWrapper
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

        /// <summary>Событие при возникновении ошибки в работе с библиотекой mbcli.dll.</summary>
        public static event EventHandler<ItpErrorEventArgs> ErrorOccured;

        /// <summary>Событие после установки соединения.</summary>
        public static event EventHandler Connected;

        /// <summary>Событие после закрытия соединения.</summary>
        public static event EventHandler Disconnected;

        /// <summary>Версия релиза библиотеки mbcli.dll.</summary>
        /// <returns>Возвращает число, указывающее версию релиза.</returns>
        /// <remarks>Обёртка над библиотечной функцией <see cref="mb_get_release_info"/></remarks>
        public static double GetReleaseInfo()
        {
            return mb_get_release_info();
        }

        /// <summary>Выполнить соединение.</summary>
        /// <param name="ip">IP-адрес. Указатель на строку, содержащую адрес в формате ###.###.###.###.</param>
        /// <param name="port">Номер порта.</param>
        /// <returns>Возвращает результат соединения. 0, если соединение удалось и код ошибки, если соединенеие не удалось.
        /// <para>Для получения описания ошибки следует обрабатывать событие <see cref="ErrorOccured"/>.</para>.</returns>
        /// <remarks>Обёртка над библиотечной функцией <see cref="mb_connect"/></remarks>
        public static int Connect(IPAddress ip, int port)
        {
            var result = mb_connect(Marshal.StringToHGlobalAnsi(IpToString(ip)), port);
            if (result != 0)
                OnErrorOccured(result);
            else
                OnConnected();
            return result;
        }

        /// <summary>Закрыть соединение.</summary>
        /// <remarks>Обёртка над библиотечной функцией <see cref="mb_close"/></remarks>
        public static int Disconnect()
        {
            var result = mb_close();
            if (result != 0)
                OnErrorOccured(result);
            else
                OnDisconnected();
            return result;
        }

        /// <summary>Чтение регистров СКУД.</summary>
        /// <param name="start">Начальный регистр.</param>
        /// <param name="count">Количество ячеек.</param>
        /// <param name="ptr">Указатель на структуру, куда записываются данные.</param>
        /// <returns>Возвращает число.</returns>
        /// <remarks>Обёртка над библиотечной функцией <see cref="mb_rd_holding_registers"/></remarks>
        public static int HoldRegisters(int start, int count, IntPtr ptr)
        {
            var result = mb_rd_holding_registers(start, count, ptr);
            var err = mb_get_error();
            var intMessage = Marshal.PtrToStringAnsi(err);
            if (!string.IsNullOrEmpty(intMessage))
                Debug.WriteLine("Ошибка {0}. {1}", err, intMessage);
            //if (result != 0)
            //    OnErrorOccured(result);
            return result;
        }

        /// <summary>Вызов события <see cref="ErrorOccured"/>.</summary>
        /// <param name="errCode">Код ошибки, возвращённый функцией библиотеки.</param>
        /// <remarks>Обёртка над библиотечной функцией <see cref="mb_get_error"/></remarks>
        private static void OnErrorOccured(int errCode)
        {
            var result = mb_get_error();
            var intMessage = Marshal.PtrToStringAnsi(result);
            //TODO: Здесь не понятно, кто отвечает за удаление указателя на массив.
            //Marshal.ZeroFreeGlobalAllocAnsi(result);
            if (ErrorOccured != null) ErrorOccured.Invoke(null, new ItpErrorEventArgs(errCode, intMessage));
        }

        /// <summary>Преобразование экземпляра <see cref="IPAddress"/> в строку ###.###.###.###.</summary>
        /// <param name="ipAddress">Экземпляр <see cref="IPAddress"/></param>
        /// <returns>Возвращает IP-адрес в виде строки ###.###.###.###.</returns>
        public static string IpToString(IPAddress ipAddress)
        {
            var b = ipAddress.GetAddressBytes();
            return string.Format("{0:000}.{1:000}.{2:000}.{3:000}", b[0], b[1], b[2], b[3]);
        }

        private static void OnDisconnected()
        {
            var handler = Disconnected;
            if (handler != null) handler(null, EventArgs.Empty);
        }

        private static void OnConnected()
        {
            var handler = Connected;
            if (handler != null) handler(null, EventArgs.Empty);
        }
    }
}
