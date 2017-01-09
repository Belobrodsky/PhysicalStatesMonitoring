using System.Diagnostics;

namespace GraphMonitor
{
    public static class PerformanceMeter
    {
        #region Свойства

        private static readonly Stopwatch _watch = new Stopwatch();
        private static string _message;

        #endregion

        public static void Start(string message)
        {
            _message = message;
            _watch.Reset();
            _watch.Start();
        }

        public static void Stop()
        {
            Debug.WriteLine(
                "{2} Выполнено за {0}мс, {1} тиков.", _watch.ElapsedMilliseconds, _watch.ElapsedTicks, _message);
        }
    }
}