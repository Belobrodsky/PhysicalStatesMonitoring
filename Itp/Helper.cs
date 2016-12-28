using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Itp
{
    internal static class Helper
    {
        /// <summary>
        /// Преобразование массива байт в струтуру
        /// </summary>
        /// <typeparam name="T">Тип структуры, в которую нужно преобразовать.</typeparam>
        /// <param name="bytes">Массив <see cref="byte"/>.</param>
        /// <returns>Возвращает структуру, заполненную из переданного массива <see cref="byte"/></returns>
        public static T ToStruct<T>(this byte[] bytes) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T stuff = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return stuff;
        }
        
        public static StringBuilder AppendFormatLine(this StringBuilder sb, string format, params object[] args)
        {
            return sb.AppendFormat(format, args).AppendLine();
        }
    }
}
