using System;

namespace Ipt
{
    public class DataReadEventArgs : EventArgs
    {
        #region Свойства

        public Buffer Buffer { get; set; }
        public Ipt4 Ipt4 { get; set; }

        #endregion

        public DataReadEventArgs(Buffer buffer, Ipt4 ipt4)
        {
            Buffer = buffer;
            Ipt4 = ipt4;
        }
    }
}