using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;

namespace MonitorForms
{
    /// <summary>Настройки приложения.</summary>
    public class Settings
    {
        public string ScudIp { get; set; }
        public int ScudPort { get; set; }
        public string IptIp { get; set; }
        public int IptPort { get; set; }
        public string LogFile { get; set; }
        public string EmulPath { get; set; }
        public bool ErrorLogVisible { get; set; }
        public bool ScudListVisible { get; set; }
        public bool IptListVisible { get; set; }
        public int IptFreqIndex { get; set; }

        [XmlIgnore]
        public IPAddress IptIpAddress
        {
            get { return IPAddress.Parse(IptIp); }
        }

        [XmlIgnore]
        public IPAddress ScudIpAddress
        {
            get { return IPAddress.Parse(ScudIp); }
        }

        static Settings()
        {
            Serializer = new XmlSerializer(typeof(Settings));
        }

        private static XmlSerializer Serializer { get; set; }

        private static string SettingsPath
        {
            get
            {
                return "settings.xml";
            }
        }

        public static void Save(Settings settings)
        {
            using (var writer = new StreamWriter(SettingsPath))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                Serializer.Serialize(writer, settings, ns);
            }
        }

        public static Settings Read()
        {
            if (!File.Exists(SettingsPath))
            {
                return GetDefaultSettings();
            }
            using (var reader = new StreamReader(SettingsPath))
            {
                try
                {
                    return (Settings)Serializer.Deserialize(reader);
                }
                catch (Exception)
                {
                    return GetDefaultSettings();
                }
            }
        }

        private static Settings GetDefaultSettings()
        {
            return new Settings()
            {
                IptIp = "127.000.000.001",
                IptPort = 1952,
                ScudIp = "127.000.000.001",
                ScudPort = 1952,
                LogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logfile.txt"),
                ErrorLogVisible = false,
                ScudListVisible = true,
                IptListVisible = true,
                IptFreqIndex = 0
            };
        }
    }
}
