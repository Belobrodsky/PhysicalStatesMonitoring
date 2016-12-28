namespace Itp
{
    internal interface IReader<T> where T : struct
    {
        void Connect();
        void Disconnect();
        T Read();
    }
}