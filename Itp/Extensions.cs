using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Ipt
{
    public static class Extensions
    {
        public static StringBuilder AppendFormatLine(this StringBuilder sb, string format, params object[] args)
        {
            return sb.AppendFormat(format, args).AppendLine();
        }

        /// <summary>Удаление из IP-адреса незначащих нулей.</summary>
        /// <param name="address">Строка с IP-адресом.</param>
        /// <returns>Возвращает строку с IP-адресом без лишних нулей.</returns>
        /// <remarks>
        ///     Некоторые адреса вида 192.168.000.001, где есть незначащие нули в триадах,
        ///     <see cref="IPAddress.Parse" /> обрабатывает неверно. Поэтому такой адрес будет преобразован в 192.168.0.1
        /// </remarks>
        public static string CleanIp(this string address)
        {
            if (address.IsNullOrEmpty())
            {
                return address;
            }
            if (address.IndexOf(".0.", StringComparison.Ordinal) != -1)
            {
                return address;
            }
            while (address.IndexOf(".0", StringComparison.Ordinal) != -1
                   && address.IndexOf(".0.", StringComparison.Ordinal) == -1)
            {
                address = address.Replace(".0", ".");
            }
            return address;
        }

        /// <summary>Проверка, что строка не пустая.</summary>
        /// <param name="value">Строковая переменная</param>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        ///     Преобразование структуры в массив байт.
        /// </summary>
        /// <typeparam name="T">Тип структуры, в которую нужно преобразовать.</typeparam>
        /// <param name="value">Объект типа <typeparamref name="T" /></param>
        /// <returns>Возвращает массив байт.</returns>
        public static byte[] ToBytes<T>(this T value) where T : struct
        {
            int size = Marshal.SizeOf(value);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(value, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }

        /// <summary>
        ///     Преобразование массива байт в струтуру
        /// </summary>
        /// <typeparam name="T">Тип структуры, в которую нужно преобразовать.</typeparam>
        /// <param name="bytes">Массив <see cref="byte" />.</param>
        /// <returns>Возвращает структуру, заполненную из переданного массива <see cref="byte" /></returns>
        public static T ToStruct<T>(this byte[] bytes) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T stuff = (T) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return stuff;
        }
    }
}