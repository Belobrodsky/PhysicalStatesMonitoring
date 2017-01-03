using System;
using System.Net.Sockets;
using Ipt;

namespace IptEmul
{
    /// <summary>Класс для проверки соединения с ИПТ в консольном режиме.</summary>
    internal class IptConnection
    {
        /// <summary>Проверить соединение.</summary>
        public static void Check()
        {
            Console.WriteLine("Проверка связи с ИПТ.");
            if (Program.EndPoint == null)
                if (!Program.GetIpEndPoint()) return;

            IptReader ipt = null;
            try
            {
                ipt = IptReader.GetInstance(Program.EndPoint.Address, Program.EndPoint.Port);
                ipt.Connect();
                Console.WriteLine(ipt.Read());
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Соединение с ИПТ установлено ({0}).", Program.EndPoint);
                Console.ResetColor();
            }
            catch (SocketException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка связи с ИПТ.");
                Console.WriteLine("Номер ошибки: {0}.", e.ErrorCode);
                Console.WriteLine("Сообщение: {0}.", e.Message);
                Console.ResetColor();
            }
            finally
            {
                if (ipt != null) ipt.Dispose();
            }
        }
    }
}
