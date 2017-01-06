using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Ipt;

namespace IptEmul
{
    /// <summary>���������� �����-������.</summary>
    internal class IptServer
    {
        public static void StartListening()
        {
            Console.WriteLine("�������� ���.");
            IPEndPoint localEndPoint = new IPEndPoint(Program.Address, Program.Port);
            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(0);

                using (Socket handler = listener.Accept())
                {
                    Console.WriteLine("��������� {0}", handler.RemoteEndPoint);
                    int rec = -1;
                    while (rec != 0)
                    {
                        Console.WriteLine("�������� ������� �� {0}", handler.RemoteEndPoint);
                        byte[] buffer = new byte[1];
                        rec = handler.Receive(buffer);
                        if (buffer[0] != 0xE0) continue;
                        var time = DateTime.Now;
                        Console.WriteLine("{1:HH:mm:ss.ff}\t������ ������: {0:X}", buffer[0], time);
                        //��� ��������� ������� 0xE0 ������������ �����
                        Random rnd = new Random(DateTime.Now.Millisecond);
                        var bytes = new byte[Marshal.SizeOf(typeof(Ipt4))];
                        rnd.NextBytes(bytes);
                        handler.Send(bytes);
                        Console.WriteLine(
                            "����� ��������� ����� {0:N1} ����:", ( DateTime.Now - time ).TotalMilliseconds);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(bytes.ToStruct<Ipt4>());
                        Console.ResetColor();
                    }
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.ToString());
                Console.ResetColor();
            }
        }
    }
}