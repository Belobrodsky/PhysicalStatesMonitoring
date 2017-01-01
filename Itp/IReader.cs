using System;

namespace Ipt
{
    internal interface IReader<out T> : IDisposable where T : struct
    {
        void Connect();
        void Disconnect();
        T Read();
    }
}