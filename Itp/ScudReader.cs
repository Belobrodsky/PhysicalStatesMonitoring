using System;
using System.Net;
using System.Runtime.InteropServices;

namespace Ipt
{
    /// <summary>Реализация чтения данных со СКУД.</summary>
    public class ScudReader : IReader<Buffer>
    {
        #region Свойства

        private static ScudReader _instance;
        private static readonly object _padlock = new object();
        private readonly IPAddress _address;
        private readonly int _port;

        #endregion

        private ScudReader(IPAddress address, int port)
        {
            _address = address;
            _port = port;
        }

        public void Connect()
        {
            MbCliWrapper.Connect(_address, _port);
        }

        /// <summary>Чтение данных со СКУД.</summary>
        /// <returns>Возвращает данные типа <see cref="Buffer" /></returns>
        public Buffer Read()
        {
            var size = Marshal.SizeOf(typeof(Buffer));
            //Выделение памяти под структуру
            var ptr = Marshal.AllocHGlobal(size);
            //Чтение данных
            MbCliWrapper.HoldRegisters(0, 1000, ptr);
            //Запись данных из памяти в структуру
            var buff = (Buffer) Marshal.PtrToStructure(ptr, typeof(Buffer));
            //Освобождение памяти
            Marshal.FreeHGlobal(ptr);
            return buff;
        }

        public void Disconnect()
        {
            MbCliWrapper.Disconnect();
        }

        /// <summary>Получение экземпляра класса <see cref="ScudReader" />.</summary>
        /// <param name="ipAddress">Ip-адрес, на котором расположен СКУД.</param>
        /// <param name="port">Порт, на котором расположен СКУД.</param>
        /// <returns>Возвращает новый экземпляр <see cref="ScudReader" /> или ранее созданный.</returns>
        public static ScudReader GetInstance(IPAddress ipAddress, int port)
        {
            lock (_padlock)
            {
                if (_instance != null)
                {
                    return _instance;
                }
                _instance = new ScudReader(ipAddress, port);
                return _instance;
            }
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        #endregion
    }
}