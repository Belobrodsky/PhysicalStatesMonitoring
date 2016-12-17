using System;

namespace Itp
{
    public class DataReadEventArgs : EventArgs
    {
        public Buffer Buffer { get; set; }

        public DataReadEventArgs(Buffer buffer)
        {
            Buffer = buffer;
        }
    }
}