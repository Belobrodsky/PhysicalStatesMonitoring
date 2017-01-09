using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace Ipt
{
    /// <summary>Реализация механизма чтения с ИПТ.</summary>
    public class IptReader : IReader<Ipt4>
    {
        #region Свойства

        private static IptReader _instance;
        private static readonly object _padlock = new object();
        //Запрос к серверу
        readonly byte[] _bytesToSend = {0xE0};

        //Буфер для приёма данных
        readonly byte[] _received = new byte[Marshal.SizeOf(typeof(Ipt4))];
        private readonly IPEndPoint _remoteEp;
        private TcpClient _client;
        private NetworkStream _reader;
        private NetworkStream _writer;

        #endregion

        private IptReader(IPAddress ipAddress, int port)
        {
            _remoteEp = new IPEndPoint(ipAddress, port);
        }

        /// <summary>Чтение данных с ИПТ.</summary>
        /// <returns>Возвращает структуру типа <see cref="Ipt4" /></returns>
        public Ipt4 Read()
        {
            //Отправляем серверу запрос на данные
            _writer.Write(_bytesToSend, 0, _bytesToSend.Length);
            //Ждём пока сервер вернёт ответ
            while (!_reader.DataAvailable)
            {
                Thread.Sleep(1);
            }
            _reader.Read(_received, 0, _received.Length);
            return _received.ToStruct<Ipt4>();
        }

        /// <summary>Соединение с ИПТ.</summary>
        public void Connect()
        {
            _client = new TcpClient();
            _client.Connect(_remoteEp);
            _reader = _client.GetStream();
            _writer = _client.GetStream();
        }

        /// <summary>Отключение от ИПТ.</summary>
        public void Disconnect()
        {
            _client.Close();
        }

        /// <summary>Получение экземпляра класса <see cref="IptReader" />.</summary>
        /// <param name="ipAddress">Ip-адрес, на котором расположен ИПТ.</param>
        /// <param name="port">Порт, на котором расположен ИПТ.</param>
        /// <returns>Возвращает новый экземпляр <see cref="IptReader" /> или ранее созданный.</returns>
        public static IptReader GetInstance(IPAddress ipAddress, int port)
        {
            lock (_padlock)
            {
                if (_instance != null)
                {
                    return _instance;
                }
                _instance = new IptReader(ipAddress, port);
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
            if (!disposing)
            {
                return;
            }

            if (_client != null)
            {
                _client.Close();
            }
            if (_reader != null)
            {
                _reader.Close();
                _reader.Dispose();
            }
            if (_writer != null)
            {
                _writer.Close();
                _writer.Dispose();
            }
        }

        #endregion
    }
}