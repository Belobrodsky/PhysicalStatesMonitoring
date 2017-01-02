using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
namespace Ipt
{
    /// <summary>Класс для упрощения работы с функциями библиотеки mbcli.dll.</summary>
    public static class MbCliWrapper
    {
        /// <summary>Событие при возникновении ошибки в работе с библиотекой mbcli.dll.</summary>
        public static event EventHandler<DataReaderErrorEventArgs> Error;

        /// <summary>Событие после установки соединения.</summary>
        public static event EventHandler Connected;

        /// <summary>Событие после закрытия соединения.</summary>
        public static event EventHandler Disconnected;

        /// <summary>Версия релиза библиотеки mbcli.dll.</summary>
        /// <returns>Возвращает число, указывающее версию релиза.</returns>
        /// <remarks>Обёртка над библиотечной функцией <see cref="UnsafeNativeMethods.mb_get_release_info"/></remarks>
        public static double GetReleaseInfo()
        {
            return UnsafeNativeMethods.mb_get_release_info();
        }

        /// <summary>Выполнить соединение.</summary>
        /// <param name="ip">IP-адрес. Указатель на строку, содержащую адрес в формате ###.###.###.###.</param>
        /// <param name="port">Номер порта.</param>
        /// <returns>Возвращает результат соединения. 0, если соединение удалось и код ошибки, если соединенеие не удалось.
        /// <para>Для получения описания ошибки следует обрабатывать событие <see cref="Error"/>.</para>.</returns>
        /// <remarks>Обёртка над библиотечной функцией <see cref="UnsafeNativeMethods.mb_connect"/></remarks>
        public static int Connect(IPAddress ip, int port)
        {
            var result = UnsafeNativeMethods.mb_connect(Marshal.StringToHGlobalAnsi(ip.IpToString()), port);
            if (result != 0)
                OnErrorOccured(result);
            else
                OnConnected();
            return result;
        }

        /// <summary>Закрыть соединение.</summary>
        /// <remarks>Обёртка над библиотечной функцией <see cref="UnsafeNativeMethods.mb_close"/></remarks>
        public static int Disconnect()
        {
            var result = UnsafeNativeMethods.mb_close();
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
        /// <remarks>Обёртка над библиотечной функцией <see cref="UnsafeNativeMethods.mb_rd_holding_registers"/></remarks>
        public static int HoldRegisters(int start, int count, IntPtr ptr)
        {
            var result = UnsafeNativeMethods.mb_rd_holding_registers(start, count, ptr);
            var err = UnsafeNativeMethods.mb_get_error();
            var message = Marshal.PtrToStringAnsi(err);
            if (!string.IsNullOrEmpty(message))
                OnErrorOccured(result);
            return result;
        }

        /// <summary>Вызов события <see cref="Error"/>.</summary>
        /// <param name="errCode">Код ошибки, возвращённый функцией библиотеки.</param>
        /// <remarks>Обёртка над библиотечной функцией <see cref="UnsafeNativeMethods.mb_get_error"/></remarks>
        private static void OnErrorOccured(int errCode)
        {
            var result = UnsafeNativeMethods.mb_get_error();
            var message = Marshal.PtrToStringAnsi(result);
            //TODO: Здесь не понятно, кто отвечает за удаление указателя на массив.
            //Marshal.ZeroFreeGlobalAllocAnsi(result);
            if (Error != null)
            {
                Error.Invoke(null, new DataReaderErrorEventArgs(errCode, message));
            }
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
