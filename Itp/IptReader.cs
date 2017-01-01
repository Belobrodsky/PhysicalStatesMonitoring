using System;
using System.Net;
using System.Net.Sockets;

namespace Ipt
{
    /// <summary>Реализация механизма чтения с ИПТ.</summary>
    public class IptReader : IReader<Ipt4>
    {
        #region Свойства
        private Socket _client;
        private readonly IPEndPoint _remoteEp;

        private static IptReader _instance;
        private static readonly object _padlock = new object();
        #endregion

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

        private IptReader(IPAddress ipAddress, int port)
        {
            _remoteEp = new IPEndPoint(ipAddress, port);
        }

        /// <summary>Чтение данных с ИПТ.</summary>
        /// <returns>Возвращает структуру типа <see cref="Ipt4"/></returns>
        public Ipt4 Read()
        {
            var received = new byte[43];
            // Создание сокета TCP/IP.
            _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Соединение с удалённой конечной точкой.
            _client.Connect(_remoteEp);
            // Отправка запроса на данные.
            _client.Send(new byte[] { 0xE0 });
            // Получение ответа.
            _client.Receive(received);
            //Закрываем клиент
            _client.Shutdown(SocketShutdown.Both);
            _client.Disconnect(true);
            _client.Close();
            return received.ToStruct<Ipt4>();
        }

        public void Connect()
        {
        }

        public void Disconnect()
        {

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
            if (_client != null) _client.Dispose();
        }

        #endregion
    }
}