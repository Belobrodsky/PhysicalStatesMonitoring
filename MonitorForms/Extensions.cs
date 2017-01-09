using System;
using System.Net;
using System.Windows.Forms;

namespace MonitorForms
{
    //[DebuggerStepThrough]
    public static class Extensions
    {
        /// <summary>Удаление из IP-адреса незначащих нулей.</summary>
        /// <param name="address">Строка с IP-адресом.</param>
        /// <returns>Возвращает строку с IP-адресом.</returns>
        /// <remarks>
        ///     Некоторые адреса вида 192.168.000.001, где есть незначащие нули в триадах,
        ///     <see cref="IPAddress.Parse" /> обрабатывает неверно. Поэтому такой адрес будет преобразован в 192.168.0.1
        /// </remarks>
        public static string CleanIp(this string address)
        {
            if (address.IsNullOrEmpty())
                return address;
            if (address.IndexOf(".0.", StringComparison.Ordinal) != -1)
                return address;
            while (address.IndexOf(".0", StringComparison.Ordinal) != -1
                   && address.IndexOf(".0.", StringComparison.Ordinal) == -1)
            {
                address = address.Replace(".0", ".");
            }
            return address;
        }

        /// <summary>Вспомогательный метод для работы с контролом из другого потока.</summary>
        /// <param name="control">Контрол, к которому нужен доступ из другого потока.</param>
        /// <param name="action">Метод, который будет работать с контролом.</param>
        public static void InvokeEx(this Control control, Action action)
        {
            if (control.InvokeRequired)
                control.Invoke(action);
            else
                action.Invoke();
        }

        /// <summary>Проверка, что строка не пустая.</summary>
        /// <param name="value">Строковая переменная</param>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}