using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Ipt
{
    /// <summary>Реализация механизма чтения с ИПТ.</summary>
    public class IptReader : TcpClient, IReader<Ipt4>
    {
        #region Свойства

        private readonly IPAddress _address;
        private readonly int _port;

        #endregion

        //TODO:Попробовать singleton. Через интерфейс
        public IptReader(IPAddress address, int port)
        {
            _address = address;
            _port = port;
        }

        public void Connect()
        {
            Connect(_address, _port);
        }

        /// <summary>Чтение данных с ИПТ.</summary>
        /// <returns>Возвращает структуру типа <see cref="Ipt4" /></returns>
        public Ipt4 Read()
        {
            NetworkStream stream = GetStream();
            //Отправка запроса в ИПТ
            stream.WriteByte(0xE0);
            byte[] data = new byte[Marshal.SizeOf(typeof(Ipt4))];
            //Получение ответа
            stream.Read(data, 0, data.Length);
            return data.ToStruct<Ipt4>();
        }

        public void Disconnect()
        {
            var stream = GetStream();
            stream.Close();
            Close();
        }
    }
}