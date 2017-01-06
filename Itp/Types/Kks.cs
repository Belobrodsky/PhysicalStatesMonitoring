using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Ipt
{
    /// <summary>Индексы СКУД.</summary>
    public class Kks : ICloneable
    {
        #region Свойства

        /*
        P_CORE=60
        T_COLD=82
        T_HOT=77
        P_SG=232
        H_12=102
        H_11=101
        H_10=100
        L_pres=241
        L_sg=237
        C_bor=51
        C_bor_f=53
        F_makeup=63
        N_akz=54
        N_tg=59
        AO=243
        */

        [XmlAttribute("P_CORE")]
        [DisplayName("P_CORE")]
        public int PCore { get; set; }

        [XmlAttribute("T_COLD")]
        [DisplayName("T_COLD")]
        public int TCold { get; set; }

        [XmlAttribute("T_HOT")]
        [DisplayName("T_HOT")]
        public int THot { get; set; }

        [XmlAttribute("P_SG")]
        [DisplayName("P_SG")]
        public int PSg { get; set; }

        [XmlAttribute("H_12")]
        [DisplayName("H_12")]
        public int H12 { get; set; }

        [XmlAttribute("H_11")]
        [DisplayName("H_11")]
        public int H11 { get; set; }

        [XmlAttribute("H_10")]
        [DisplayName("H_10")]
        public int H10 { get; set; }

        [XmlAttribute("L_pres")]
        [DisplayName("L_pres")]
        public int LPres { get; set; }

        [XmlAttribute("L_sg")]
        [DisplayName("L_sg")]
        public int LSg { get; set; }

        [XmlAttribute("C_bor")]
        [DisplayName("C_bor")]
        public int Cbor { get; set; }

        [XmlAttribute("C_bor_f")]
        [DisplayName("C_bor_f")]
        public int Cborf { get; set; }

        [XmlAttribute("F_makeup")]
        [DisplayName("F_makeup")]
        public int Fmakeup { get; set; }

        [XmlAttribute("N_akz")]
        [DisplayName("N_akz")]
        public int Nakz { get; set; }

        [XmlAttribute("N_tg")]
        [DisplayName("N_tg")]
        public int Ntg { get; set; }

        [XmlAttribute("AO")]
        [DisplayName("AO")]
        public int Ao { get; set; }

        #endregion

        public Kks()
        {
            PCore = 60;
            TCold = 82;
            THot = 77;
            PSg = 232;
            H12 = 102;
            H11 = 101;
            H10 = 100;
            LPres = 241;
            LSg = 237;
            Cbor = 51;
            Cborf = 53;
            Fmakeup = 63;
            Nakz = 54;
            Ntg = 59;
            Ao = 243;
        }

        #region Implementation of ICloneable

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}