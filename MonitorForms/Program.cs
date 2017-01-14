using System;
using System.Windows.Forms;

namespace MonitorForms
{
    static class Program
    {
        #region Свойства

        internal static Settings Settings { get; private set; }
        #endregion

        public const string I1 = "I_1";
        public const string I2 = "I_2";
        public const string R1 = "R_1";
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