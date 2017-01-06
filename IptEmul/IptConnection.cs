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
            try
            {
                using (var ipt = IptReader.GetInstance(Program.Address, Program.Port))
                {
                    ipt.Connect();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Соединение с ИПТ установлено ({0}).", Program.Address);
                    Console.ResetColor();
                    Console.WriteLine(ipt.Read());
                }
            }
            catch (SocketException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка связи с ИПТ.");
                Console.WriteLine("Номер ошибки: {0}.", e.ErrorCode);
                Console.WriteLine("Сообщение: {0}.", e.Message);
                Console.ResetColor();
            }
        }
    }
}