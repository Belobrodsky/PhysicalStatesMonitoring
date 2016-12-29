using System;
using System.Net;
using System.Windows.Forms;

namespace MonitorForms
{
    public static class Extensions
    {
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

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>Преобразование экземпляра <see cref="IPAddress"/> в строку ###.###.###.###.</summary>
        /// <param name="address">Экземпляр <see cref="IPAddress"/></param>
        /// <returns>Возвращает IP-адрес в виде строки ###.###.###.###.</returns>
        public static string IpToString(this IPAddress address)
        {
            var b = address.GetAddressBytes();
            return string.Format("{0:000}.{1:000}.{2:000}.{3:000}", b[0], b[1], b[2], b[3]);
        }
    }

}
