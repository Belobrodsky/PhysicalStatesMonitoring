using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Itp
{
    /// <summary>Класс для записи данных в файл.</summary>
    public class DataWriter
    {
        private readonly StreamWriter _writer;
        /// <summary>Заголовки столбцов.</summary>
        public IList<string> Headers { get; set; }
        /// <summary>Время в формате Unix.</summary>
        private static long UnixTime
        {
            get
            { return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000; }
        }

        public DataWriter(string path, IList<string> headers)
        {
            Headers = headers;
            _writer = new StreamWriter(path);
            WriteHeaders();
        }

        /// <summary>Запись строки заголовков.</summary>
        public void WriteHeaders()
        {
            _writer.WriteLine(string.Join("\t", Headers));
            _writer.Flush();
        }

        //TODO:Менять на вычисленные значения токов. Передавать их в метод
        /// <summary>Запись данных.</summary>
        /// <param name="buffer">Данные со СКУД.</param>
        /// <param name="J1"></param>
        /// <param name="J2"></param>
        /// <param name="R1"></param>
        /// <param name="R2"></param>
        /// <param name="Rc"></param>
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
    }
}
