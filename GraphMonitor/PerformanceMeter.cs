using System.Diagnostics;

namespace GraphMonitor
{
    public static class PerformanceMeter
    {
        private static readonly Stopwatch Watch = new Stopwatch();
        private static string _message;
        public static void Start(string message)
        {
            _message = message;
            Watch.Reset();
            Watch.Start();
        }

        public static void Stop()
        {
            Debug.WriteLine("{2} Выполнено за {0}мс, {1} тиков.", Watch.ElapsedMilliseconds, Watch.ElapsedTicks, _message);
        }
    }
}
