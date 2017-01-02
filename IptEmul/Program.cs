using System;
using System.Net;

namespace IptEmul
{
    class Program
    {
        public static IPEndPoint EndPoint { get; set; }
            //Меню программы
        private static ConsoleMenu _menu;

        static void Main(string[] args)
        {
            //Из командной строки берём адрес и порт
            var param = ParseArgs(args);
            if (EndPoint != null)
            {
                Console.WriteLine(EndPoint);
            }
            //Что запускать
            switch (param)
            {
                case "-emul":
                    SyncListener.StartListening();
                    break;
                case "-ipt":
                    IptConnection.Check();
                    break;
                case "-scud":
                    ScudConnection.Check();
                    break;
            }
            _menu = new ConsoleMenu(
                new ConsoleMenuItem("Эмулятор ИПТ", SyncListener.StartListening),
                new ConsoleMenuItem("Проверка связи с ИПТ", IptConnection.Check),
                new ConsoleMenuItem("Проверка связи со СКУД", ScudConnection.Check),
                new ConsoleMenuItem("Справка", ShowHelp)
            );
            _menu.Show(false);
        }

        private static void ShowHelp()
        {
            Console.WriteLine(
                "Использование: iptemul [-ip IP] [-p PORT] [-emul|-ipt|-scud]\r\n" +
                "Ключи:\r\n\t-ip IP\tIp-адрес, по которому соединяться с заданным устройством.\r\n\t" +
                "\tРаботает в паре с -p.\r\n\t" +
                "-p PORT\tНомер порта. Работает в паре с -ip.\r\n\t" +
                "-emul\tЗапуск в режиме эмулятора сокет-сервера.\r\n\t" +
                "-ipt\tЗапуск для проверки соединения с ИПТ.\r\n\t" +
                "-scud\tЗапуск для проверки соединения со СКУД.\r\n\t");
        }

        private static string ParseArgs(string[] args)
        {
            string param = string.Empty;
            string address = string.Empty;
            string port = string.Empty;
            for (var i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-ip":
                        address = args[i + 1];
                        ++i;
                        break;
                    case "-p":
                        port = args[i + 1];
                        ++i;
                        break;
                    case "-emul":
                    case "-ipt":
                    case "-scud":
                        param = args[i];
                        break;
                }
            }
            if (address.Length > 0 && port.Length > 0)
            {
                EndPoint = new IPEndPoint(IPAddress.Parse(address), int.Parse(port));
            }
            return param;
        }

        /// <summary>Проверка валидности строки для ip-адреса.</summary>
        private static bool IsValidIpAddress(string address)
        {
            try
            {
                IPAddress.Parse(address);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка при вводе адреса {0}.", address);
                Console.ResetColor();
                return false;
            }
            return true;
        }

        /// <summary>Диалог для получения ip-адреса и номера порта.</summary>
        /// <returns>Возвращает true, если удалось получить ip-адрес и порт.</returns>
        public static bool GetIpEndPoint()
        {
            //Запрос адреса
            Console.Write("Адрес: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            var address = Console.ReadLine();
            Console.ResetColor();
            if (!IsValidIpAddress(address))
                return false;

            //Запрос порта
            Console.Write("Порт: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            var portInput = Console.ReadLine();
            Console.ResetColor();
            int port;
            if (int.TryParse(portInput, out port))
            {
                EndPoint = new IPEndPoint(IPAddress.Parse(address), port);
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
