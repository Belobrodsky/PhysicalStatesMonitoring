using System.Runtime.InteropServices;
using System.Text;

namespace Ipt
{
    /// <summary>Структура для хранения данных с ИПТ-4</summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Ipt4
    {
        [MarshalAs(UnmanagedType.U1)]
        public byte MagistralFailure;

        [MarshalAs(UnmanagedType.R4)]
        public float FCurrent1;

        [MarshalAs(UnmanagedType.R4)]
        public float Freactivity1;

        [MarshalAs(UnmanagedType.U1)]
        public byte Power1;

        [MarshalAs(UnmanagedType.U1)]
        public byte Filter1;

        [MarshalAs(UnmanagedType.U1)]
        public byte Lcounter1;

        [MarshalAs(UnmanagedType.U1)]
        public byte Status1;

        [MarshalAs(UnmanagedType.U1)]
        public byte GotHVP1;

        [MarshalAs(UnmanagedType.U1)]
        public byte GotHVN1;

        [MarshalAs(UnmanagedType.R4)]
        public float FCurrent2;

        [MarshalAs(UnmanagedType.R4)]
        public float Freactivity2;

        [MarshalAs(UnmanagedType.U1)]
        public byte Power2;

        [MarshalAs(UnmanagedType.U1)]
        public byte Filter2;

        [MarshalAs(UnmanagedType.U1)]
        public byte Lcounter2;

        [MarshalAs(UnmanagedType.U1)]
        public byte Status2;

        [MarshalAs(UnmanagedType.U1)]
        public byte GotHVP2;

        [MarshalAs(UnmanagedType.U1)]
        public byte GotHVN2;

        [MarshalAs(UnmanagedType.R4)]
        public float FCurrent3;

        [MarshalAs(UnmanagedType.R4)]
        public float Freactivity3;

        [MarshalAs(UnmanagedType.U1)]
        public byte Power3;

        [MarshalAs(UnmanagedType.U1)]
        public byte Filter3;

        [MarshalAs(UnmanagedType.U1)]
        public byte Lcounter3;

        [MarshalAs(UnmanagedType.U1)]
        public byte Status3;

        [MarshalAs(UnmanagedType.U1)]
        public byte GotHVP3;

        [MarshalAs(UnmanagedType.U1)]
        public byte GotHVN3;

        #region Overrides of ValueType

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormatLine("MagistralFailure = {0}", MagistralFailure);

            sb.AppendFormatLine("FCurrent1 = {0:E7}", FCurrent1);
            sb.AppendFormatLine("Freactivity1 = {0:E7}", Freactivity1);
            sb.AppendFormatLine("Power1 = {0}", Power1);
            sb.AppendFormatLine("Filter1 = {0}", Filter1);
            sb.AppendFormatLine("Lcounter1 = {0}", Lcounter1);
            sb.AppendFormatLine("Status1 = {0}", Status1);
            sb.AppendFormatLine("GotHVP1 = {0}", GotHVP1);
            sb.AppendFormatLine("GotHVN1 = {0}", GotHVN1);

            sb.AppendFormatLine("FCurrent2 = {0:E7}", FCurrent2);
            sb.AppendFormatLine("Freactivity2 = {0:E7}", Freactivity2);
            sb.AppendFormatLine("Power2 = {0}", Power2);
            sb.AppendFormatLine("Filter2 = {0}", Filter2);
            sb.AppendFormatLine("Lcounter2 = {0}", Lcounter2);
            sb.AppendFormatLine("Status2 = {0}", Status2);
            sb.AppendFormatLine("GotHVP2 = {0}", GotHVP2);
            sb.AppendFormatLine("GotHVN2 = {0}", GotHVN2);

            sb.AppendFormatLine("FCurrent3 = {0:E7}", FCurrent3);
            sb.AppendFormatLine("Freactivity3 = {0:E7}", Freactivity3);
            sb.AppendFormatLine("Power3 = {0}", Power3);
            sb.AppendFormatLine("Filter3 = {0}", Filter3);
            sb.AppendFormatLine("Lcounter3 = {0}", Lcounter3);
            sb.AppendFormatLine("Status3 = {0}", Status3);
            sb.AppendFormatLine("GotHVP3 = {0}", GotHVP3);
            sb.AppendFormatLine("GotHVN3 = {0}", GotHVN3);

            return sb.ToString();
        }

        #endregion
    }
}