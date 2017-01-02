using System;
using Ipt;

namespace IptEmul
{
    /// <summary>Класс для проверки соединения со СКУД в консольном режиме.</summary>
    internal class ScudConnection
    {
        /// <summary>Проверить соединение.</summary>
        public static void Check()
        {
            Console.WriteLine("Проверка связи со СКУД.");
            if (Program.EndPoint == null)
                if (!Program.GetIpEndPoint()) return;

            ScudReader scud = null;
            try
            {
                MbCliWrapper.Error += MbCliWrapper_Error;
                MbCliWrapper.Connected += MbCliWrapper_Connected;
                scud = ScudReader.GetInstance(Program.EndPoint.Address, Program.EndPoint.Port);
                scud.Connect();
                scud.Disconnect();
            }
            catch (DllNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Не удалось найти библиотеку mbcli.dll.");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            finally
            {
                if (scud != null) scud.Dispose();
            }
        }

        private static void MbCliWrapper_Connected(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Соединение со СКУД установлено ({0}).", Program.EndPoint);
            Console.ResetColor();
        }

        private static void MbCliWrapper_Error(object sender, DataReaderErrorEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка связи со СКУД.");
            Console.WriteLine("Номер ошибки: {0}.", e.ErrorCode);
            Console.WriteLine("Сообщение: {0}.", e.ErrorText);
            Console.ResetColor();
        }
    }
}
