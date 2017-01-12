using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ipt
{
    /// <summary>Класс для записи данных в файл.</summary>
    public class DataWriter : IDisposable
    {
        #region Свойства

        private static DataWriter _instance;
        private static readonly object _padlock = new object();
        //Словарь с именами и индексами переменных СКУД
        private readonly Dictionary<string, int> _scud;
        private readonly StreamWriter _writer;

        /// <summary>Заголовки столбцов.</summary>
        public IList<string> Headers { get; set; }

        /// <summary>Время в формате Unix.</summary>
        private static long UnixTime
        {
            get
            {
                return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            }
        }

        #endregion

        private DataWriter(string path)
        {
            Headers = new[]
                      {
                          "Время", "J1", "J2", "R1", "R2", "Rc",
                          "P_CORE", "T_COLD", "T_HOT", "P_SG", "H_12",
                          "H_11", "H_10", "L_pres", "L_sg", "C_bor",
                          "C_bor_f", "F_makeup", "N_akz", "N_tg", "AO"
                      };
            _writer = new StreamWriter(path);
            _scud = new Dictionary<string, int>
                    {
                        {"PCore", 60},
                        {"TCold", 82},
                        {"THot", 77},
                        {"PSg", 232},
                        {"H12", 102},
                        {"H11", 101},
                        {"H10", 100},
                        {"LPres", 241},
                        {"LSg", 237},
                        {"Cbor", 51},
                        {"Cborf", 53},
                        {"Fmakeup", 63},
                        {"Nakz", 54},
                        {"Ntg", 59},
                        {"Ao", 243}
                    };
            WriteHeaders();
        }

        public static DataWriter GetInstance(string path)
        {
            lock (_padlock)
            {
                if (_instance != null)
                {
                    return _instance;
                }
                _instance = new DataWriter(path);
                return _instance;
            }
        }

        //TODO:Менять на вычисленные значения токов. Передавать их в метод
        /// <summary>Запись данных.</summary>
        /// <param name="values">Данные со СКУД.</param>
        /// <param name="J1">Ток с ИПТ</param>
        /// <param name="J2">Ток с ИПТ</param>
        /// <param name="R1">Рассчитанная реактивность.</param>
        /// <param name="R2">Рассчитанная реактивность.</param>
        /// <param name="Rc">Средняя реактивность</param>
        public void WriteData(float[] values, double J1 = 0, double J2 = 0, double R1 = 0, double R2 = 0,
                              double Rc = 0)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}", UnixTime);
            sb.AppendFormat("\t{0:e7}\t{1:e7}\t{2:e15}\t{3:e15}\t{4:e15}", J1, J2, R1, R2, Rc);
            sb.AppendFormat("{0:E7}\t{1:E7}\t{2:E7}\t{3:E7}\t{4:E7}\t{5:E7}\t{6:E7}\t{7:E7}\t{8:E7}\t{9:E7}\t{10:E7}\t{11:E7}\t{12:E7}\t{13:E7}\t{14:E7}",
                _scud["PCore"],
                _scud["TCold"],
                _scud["THot"],
                _scud["PSg"],
                _scud["H12"],
                _scud["H11"],
                _scud["H10"],
                _scud["LPres"],
                _scud["LSg"],
                _scud["Cbor"],
                _scud["Cborf"],
                _scud["Fmakeup"],
                _scud["Nakz"],
                _scud["Ntg"],
                _scud["Ao"]);
            _writer.WriteLine(sb.ToString());
            _writer.Flush();
        }

        /// <summary>Запись строки заголовков.</summary>
        public void WriteHeaders()
        {
            _writer.WriteLine(string.Join("\t", Headers));
            _writer.Flush();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;
            if (_writer != null)
                _writer.Dispose();
        }

        #endregion
    }
}