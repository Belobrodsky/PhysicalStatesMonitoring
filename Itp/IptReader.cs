using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace Itp
{
    /// <summary>Реализация механизма чтения с ИПТ.</summary>
    public class IptReader : TcpClient, IReader<Ipt4>
    {
        private readonly IPAddress _address;
        private readonly int _port;


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
        /// <returns>Возвращает структуру типа <see cref="Ipt4"/></returns>
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
