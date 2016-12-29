using System.Net;
using System.Runtime.InteropServices;

namespace Ipt
{
    /// <summary>Реализация чтения данных со СКУД.</summary>
    internal class ScudReader : IReader<Buffer>
    {
        private readonly IPAddress _address;
        private readonly int _port;

        public ScudReader(IPAddress address, int port)
        {
            _address = address;
            _port = port;
        }

        public void Connect()
        {
            MbCliWrapper.Connect(_address, _port);
        }

        /// <summary>
        /// Чтение данных со СКУД
        /// </summary>
        /// <returns>Возвращает данные типа <see cref="Buffer"/></returns>
        public Buffer Read()
        {
            var size = Marshal.SizeOf(typeof(Buffer));
            //Debug.WriteLine("Размер структуры: {0}", size);
            //Выделение памяти под структуру
            var ptr = Marshal.AllocHGlobal(size);
            //Чтение данных
            MbCliWrapper.HoldRegisters(0, 1000, ptr);
            //Запись данных из памяти в структуру
            var buff = (Buffer)Marshal.PtrToStructure(ptr, typeof(Buffer));
            //Освобождение памяти
            Marshal.FreeHGlobal(ptr);
            return buff;
        }

        public void Disconnect()
        {
            MbCliWrapper.Disconnect();
        }
    }
}
