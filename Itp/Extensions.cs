using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Ipt
{
    public static class Extensions
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

        /// <summary>
        /// Преобразование структуры в массив байт.
        /// </summary>
        /// <typeparam name="T">Тип структуры, в которую нужно преобразовать.</typeparam>
        /// <param name="value">Объект типа <typeparamref name="T"/></param>
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

        public static StringBuilder AppendFormatLine(this StringBuilder sb, string format, params object[] args)
        {
            return sb.AppendFormat(format, args).AppendLine();
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
