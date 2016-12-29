using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ipt
{
    /// <summary>Класс для записи данных в файл.</summary>
    public class DataWriter
    {
        #region Свойства

        private static DataWriter _instance;
        private static readonly object _padlock = new object();
        private readonly StreamWriter _writer;

        /// <summary>Заголовки столбцов.</summary>
        public IList<string> Headers { get; set; }

        /// <summary>Время в формате Unix.</summary>
        private static long UnixTime
        {
            get { return ( DateTime.Now.ToUniversalTime().Ticks - 621355968000000000 ) / 10000000; }
        }

        #endregion

        private DataWriter(string path, IList<string> headers)
        {
            Headers = headers;
            _writer = new StreamWriter(path);
            WriteHeaders();
        }

        public static DataWriter GetInstance(string path, IList<string> headers)
        {
            lock (_padlock)
            {
                if (_instance != null)
                {
                    return _instance;
                }
                _instance = new DataWriter(path, headers);
                return _instance;
            }
        }

        //TODO:Менять на вычисленные значения токов. Передавать их в метод
        /// <summary>Запись данных.</summary>
        /// <param name="buffer">Данные со СКУД.</param>
        /// <param name="J1">Ток с ИПТ</param>
        /// <param name="J2">Ток с ИПТ</param>
        /// <param name="R1">Рассчитанная реактивность.</param>
        /// <param name="R2">Рассчитанная реактивность.</param>
        /// <param name="Rc">Средняя реактивность</param>
        public void WriteData(Buffer buffer, double J1 = 0, double J2 = 0, double R1 = 0, double R2 = 0, double Rc = 0)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}", UnixTime);
            sb.AppendFormat("\t{0:e15}\t{1:e15}\t{2:e15}\t{3:e15}\t{4:e15}", J1, J2, R1, R2, Rc);
            //Пятнадцать значений со СКУД
            for (var i = 0; i < 15; i++)
                sb.AppendFormat("\t{0:e15}", buffer.Buff[i]);

            _writer.WriteLine(sb.ToString());
            _writer.Flush();
        }

        /// <summary>Запись строки заголовков.</summary>
        public void WriteHeaders()
        {
            _writer.WriteLine(string.Join("\t", Headers));
            _writer.Flush();
        }
    }
}