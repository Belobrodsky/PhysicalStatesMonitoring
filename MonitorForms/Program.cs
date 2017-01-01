using System;
using System.Windows.Forms;

namespace MonitorForms
{
    static class Program
    {
        internal static Settings Settings { get; private set; }
        /// <summary>
        /// The main entry point for the application.
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
