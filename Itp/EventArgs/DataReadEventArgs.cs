using System;

namespace Itp
{
    public class DataReadEventArgs : EventArgs
    {
        public Buffer Buffer { get; set; }
        public Ipt4 Ipt4 { get; set; }

        public DataReadEventArgs(Buffer buffer, Ipt4 ipt4)
        {
            Buffer = buffer;
            Ipt4 = ipt4;
        }
    }
}