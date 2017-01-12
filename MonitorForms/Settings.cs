using System;
using System.Collections.Generic;
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

        //public List<ObjectInfo> ScudValues;

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

        [XmlArrayItem("sig")]
        public List<ScudSignal> ScudSignals { get; set; }

        [XmlArray("SignalSettings")]
        [XmlArrayItem("set")]
        public List<SignalSettings> SignalSettingses { get; set; }

        [XmlArrayItem("l")]
        public double[] Lambdas { get; set; }

        [XmlArrayItem("a")]
        public double[] Alphas { get; set; }

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
                          ScudSignals = new List<ScudSignal>
                                        {
                                            new ScudSignal("PCore", 60),
                                            new ScudSignal("TCold", 82),
                                            new ScudSignal("THot", 77),
                                            new ScudSignal("PSg", 232),
                                            new ScudSignal("H12", 102),
                                            new ScudSignal("H11", 101),
                                            new ScudSignal("H10", 100),
                                            new ScudSignal("LPres", 241),
                                            new ScudSignal("LSg", 237),
                                            new ScudSignal("Cbor", 51),
                                            new ScudSignal("Cborf", 53),
                                            new ScudSignal("Fmakeup", 63),
                                            new ScudSignal("Nakz", 54),
                                            new ScudSignal("Ntg", 59),
                                            new ScudSignal("Ao", 243)
                                        },
                          SignalSettingses = new List<SignalSettings>
                                             {
                                                 new SignalSettings("R_1", true, double.NaN, double.NaN),
                                                 new SignalSettings("R_2", true, double.NaN, double.NaN),
                                                 new SignalSettings("PCore", true, 15, 17),
                                                 new SignalSettings("TCold", true, 280, 320),
                                                 new SignalSettings("THot", true, 280, 320),
                                                 new SignalSettings("PSg", true, 6, 8),
                                                 new SignalSettings("H12", true, 0, 100),
                                                 new SignalSettings("H11", true, 0, 100),
                                                 new SignalSettings("H10", true, 0, 100),
                                                 new SignalSettings("LPres", true, 4, 9),
                                                 new SignalSettings("LSg", true, 0, 10),
                                                 new SignalSettings("Cbor", true, 0, 10),
                                                 new SignalSettings("Cborf", true, 0, 40),
                                                 new SignalSettings("Fmakeup", true, 10000, 90000),
                                                 new SignalSettings("Nakz", true, 1200, 3200),
                                                 new SignalSettings("Ntg", true, 0, 1200),
                                                 new SignalSettings("Ao", true, -50, 10),
                                                 new SignalSettings("I_1", true, double.NaN, double.NaN),
                                                 new SignalSettings("I_2", true, double.NaN, double.NaN)
                                             },
                          Lambdas = new[] {0.0127, 0.0317, 0.1180, 0.3170, 1.4000, 3.9200},
                          Alphas = new[] {0.0340, 0.2020, 0.1840, 0.4030, 0.1430, 0.0340}
                      };
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
                    return (Settings) Serializer.Deserialize(reader);
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