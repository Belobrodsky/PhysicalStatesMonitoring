using System.Diagnostics;

namespace GraphMonitor
{
    public static class PerformanceMeter
    {
        private static readonly Stopwatch _stopwatch = new Stopwatch();
        private static string _message;
        public static void Start(string message)
        {
            _message = message;
            _stopwatch.Reset();
            _stopwatch.Start();
        }

        public static void Stop()
        {
            Debug.WriteLine("{2} Выполнено за {0}мс, {1} тиков.", _stopwatch.ElapsedMilliseconds, _stopwatch.ElapsedTicks, _message);
        }
    }
}
