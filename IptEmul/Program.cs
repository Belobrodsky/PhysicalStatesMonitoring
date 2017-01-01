using System;

namespace IptEmul
{
    class Program
    {
        static void Main(string[] args)
        {
            SyncListener.StartListening();
            Console.Read();
        }
    }
}
