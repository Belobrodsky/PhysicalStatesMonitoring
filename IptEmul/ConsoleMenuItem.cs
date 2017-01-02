using System;

namespace IptEmul
{
    internal class ConsoleMenuItem
    {
        public Action Method { get; private set; }
        public string Title { get; private set; }

        public ConsoleMenuItem(string title, Action method)
        {
            Method = method;
            Title = title;
        }

        public void Execute()
        {
            Method.Invoke();
        }
    }
}
