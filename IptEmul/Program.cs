using System;
using System.Net;
using Ipt;

namespace IptEmul
{
    class Program
    {
        #region Свойства

        //Меню программы
        private static ConsoleMenu _menu;
        public static IPAddress Address { get; set; }
        public static int Port { get; set; }

        #endregion

        /// <summary>Диалог для получения ip-адреса и номера порта.</summary>
        /// <returns>Возвращает true, если удалось получить ip-адрес и порт.</returns>
        public static bool GetIpEndPoint()
        {
            if (Address == null)
            {
                //Запрос адреса
                Console.Write("Адрес: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                var address = Console.ReadLine();
                Console.ResetColor();
                if (!IsValidIpAddress(address))
                {
                    return false;
                }
                Address = IPAddress.Parse(address.CleanIp());
            }
            if (Port == -1)
            {
                //Запрос порта
                Console.Write("Порт: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                var portInput = Console.ReadLine();
                Console.ResetColor();
                int port;
                if (!int.TryParse(portInput, out port))
                {
                    return false;
                }
                Port = port;
            }
            return true;
        }

        /// <summary>Проверка валидности строки для ip-адреса.</summary>
        private static bool IsValidIpAddress(string address)
        {
            try
            {
                IPAddress.Parse(address.CleanIp());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка при вводе адреса [{0}].", address);
                Console.WriteLine(ex);
                Console.ResetColor();
                return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            Port = -1;
            if (args.Length == 0)
            {
                ShowMenu();
                return;
            }
            //Получаем адрес и порт из командной строки и возвращаем идентификатор операции.
            var param = ParseArgs(args);
            if (Address == null)
            {
                ShowMenu();
                return;
            }
            //Что запускать
            switch (param)
            {
                case "-emul":
                    IptServer.StartListening();
                    break;
                case "-ipt":
                    IptConnection.Check();
                    break;
                case "-scud":
                    ScudConnection.Check();
                    break;
                default:
                    ShowMenu();
                    return;
            }
            Console.Read();
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
            if (IsValidIpAddress(address.CleanIp()))
            {
                Address = IPAddress.Parse(address.CleanIp());
            }
            int p;
            if (int.TryParse(port, out p))
            {
                Port = p;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Неправильно указан порт.");
                Console.ResetColor();
            }
            return param;
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

        private static void ShowMenu()
        {
            _menu = new ConsoleMenu(
                new ConsoleMenuItem(
                    "Эмулятор ИПТ", () =>
                    {
                        if (GetIpEndPoint())
                        {
                            Console.Title = string.Format("{0}:{1}", Address, Port);
                            IptServer.StartListening();
                        }
                    }),
                new ConsoleMenuItem(
                    "Проверка связи с ИПТ", () =>
                    {
                        if (GetIpEndPoint())
                        {
                            Console.Title = string.Format("{0}:{1}", Address, Port);
                            IptConnection.Check();
                        }
                    }),
                new ConsoleMenuItem(
                    "Проверка связи со СКУД", () =>
                    {
                        if (GetIpEndPoint())
                        {
                            Console.Title = string.Format("{0}:{1}", Address, Port);
                            ScudConnection.Check();
                        }
                    }),
                new ConsoleMenuItem("Справка", ShowHelp)
            );
            _menu.Show(false);
        }
    }
}