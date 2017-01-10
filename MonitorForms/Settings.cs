using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using Ipt;

namespace MonitorForms
{
    /// <summary>Настройки приложения.</summary>
    public class Settings
    {
        #region Свойства

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
        public Kks Kks { get; set; }
        public XmlSerializableDictionary<string, int> ScudValues { get; set; }

        [XmlIgnore]
        public IPAddress IptIpAddress
        {
            get
            {
                return IPAddress.Parse(IptIp.CleanIp());
            }
        }

        [XmlIgnore]
        public IPAddress ScudIpAddress
        {
            get
            {
                return IPAddress.Parse(ScudIp.CleanIp());
            }
        }

        private static XmlSerializer Serializer { get; set; }

        private static string SettingsPath
        {
            get
            {
                return "settings.xml";
            }
        }

        #endregion

        static Settings()
        {
            Serializer = new XmlSerializer(typeof(Settings));
        }

        public Settings()
        {
            ScudValues = new XmlSerializableDictionary<string, int>();
        }

        private static Settings GetDefaultSettings()
        {
            var set = new Settings
            {
                IptIp = "192.168.008.002",
                IptPort = 2040,
                ScudIp = "192.168.008.001",
                ScudPort = 1024,
                LogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logfile.txt"),
                ErrorLogVisible = false,
                ScudListVisible = true,
                IptListVisible = true,
                IptFreqIndex = 0,
                Kks = new Kks(),
                ScudValues = new XmlSerializableDictionary<string, int>()
            };
            set.ScudValues.Add("PCore", 60);
            set.ScudValues.Add("TCold", 82);
            set.ScudValues.Add("THot", 77);
            set.ScudValues.Add("PSg", 232);
            set.ScudValues.Add("H12", 102);
            set.ScudValues.Add("H11", 101);
            set.ScudValues.Add("H10", 100);
            set.ScudValues.Add("LPres", 241);
            set.ScudValues.Add("LSg", 237);
            set.ScudValues.Add("Cbor", 51);
            set.ScudValues.Add("Cborf", 53);
            set.ScudValues.Add("Fmakeup", 63);
            set.ScudValues.Add("Nakz", 54);
            set.ScudValues.Add("Ntg", 59);
            set.ScudValues.Add("Ao", 243);
            return set;
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

        public static void Save(Settings settings)
        {
            using (var writer = new StreamWriter(SettingsPath))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                Serializer.Serialize(writer, settings, ns);
            }
        }
    }
}