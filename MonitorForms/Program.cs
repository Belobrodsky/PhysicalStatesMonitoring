using System;
using System.Windows.Forms;

namespace MonitorForms
{
    static class Program
    {
        #region Свойства

        internal static Settings Settings { get; private set; }
        #endregion
        /// <summary>Обозначение первого тока.</summary>
        public const string I1 = "I_1";
        /// <summary>Обозначение второго тока.</summary>
        public const string I2 = "I_2";
        /// <summary>Обозначение первой реактивности.</summary>
        public const string R1 = "R_1";
        /// <summary>Обозначение второй реактивности.</summary>
        public const string R2 = "R_2";

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Чтение настроек приложения
            Settings = Settings.Read();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            //Сохранение настроек
            Settings.Save(Settings);
        }
    }
}