using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }

}
