using System;
using System.Net;
using System.Net.Sockets;
using Ipt;

namespace IptEmul
{
    /// <summary>���������� �����-������.</summary>
    internal class SyncListener
    {
        private static byte[] _buffer;

        public static void StartListening()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1952);
            if (Program.EndPoint == null)
            {
                if (Program.GetIpEndPoint())
                {
                    localEndPoint = Program.EndPoint;
                }
            }

            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    Console.WriteLine("�������� �������: {0}", localEndPoint);

                    var handler = listener.Accept();
                    DateTime time;
                    // ��� �� ������� ������ �� ������. ������ ������ ���� 0xE0
                    while (true)
                    {
                        _buffer = new byte[1];
                        handler.Receive(_buffer);
                        if (_buffer[0] == 0xE0)
                        {
                            time = DateTime.Now;
                            //������ �������
                            break;
                        }
                    }
                    Console.WriteLine("{1:HH:mm:ss.ff}\t������ ������: {0:X}", _buffer[0], time);
                    Random rnd = new Random(DateTime.Now.Millisecond);
                    var bytes = new byte[43];
                    rnd.NextBytes(bytes);
                    handler.Send(bytes);
                    Console.WriteLine("����� ��������� ����� {0:N1} ����:", (DateTime.Now - time).TotalMilliseconds);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(bytes.ToStruct<Ipt4>());
                    Console.ResetColor();
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine(e.ToString());
                Console.ResetColor();
            }
            Console.Read();
        }
    }
}