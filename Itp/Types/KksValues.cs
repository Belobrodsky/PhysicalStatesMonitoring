using System.ComponentModel;

namespace Ipt
{
    /// <summary>Значения со СКУД.</summary>
    public class KksValues
    {
        #region Свойства

        [DisplayName("P_CORE")]
        public float PCore { get; set; }

        [DisplayName("T_COLD")]
        public float TCold { get; set; }

        [DisplayName("T_HOT")]
        public float THot { get; set; }

        [DisplayName("P_SG")]
        public float PSg { get; set; }

        [DisplayName("H_12")]
        public float H12 { get; set; }

        [DisplayName("H_11")]
        public float H11 { get; set; }

        [DisplayName("H_10")]
        public float H10 { get; set; }

        [DisplayName("L_pres")]
        public float LPres { get; set; }

        [DisplayName("L_sg")]
        public float LSg { get; set; }

        [DisplayName("C_bor")]
        public float Cbor { get; set; }

        [DisplayName("C_bor_f")]
        public float Cborf { get; set; }

        [DisplayName("F_makeup")]
        public float Fmakeup { get; set; }

        [DisplayName("N_akz")]
        public float Nakz { get; set; }

        [DisplayName("N_tg")]
        public float Ntg { get; set; }

        [DisplayName("AO")]
        public float Ao { get; set; }

        #endregion

        public KksValues(Buffer buff, Kks kks)
        {
            PCore = buff[kks.PCore];
            TCold = buff[kks.TCold];
            THot = buff[kks.THot];
            PSg = buff[kks.PSg];
            H12 = buff[kks.H12];
            H11 = buff[kks.H11];
            H10 = buff[kks.H10];
            LPres = buff[kks.LPres];
            LSg = buff[kks.LSg];
            Cbor = buff[kks.Cbor];
            Cborf = buff[kks.Cborf];
            Fmakeup = buff[kks.Fmakeup];
            Nakz = buff[kks.Nakz];
            Ntg = buff[kks.Ntg];
            Ao = buff[kks.Ao];
        }
    }
}