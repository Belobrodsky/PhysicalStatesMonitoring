using System;

namespace Itp
{
    public class ReaderStateChangedEventArgs : EventArgs
    {
        public ReaderStateEnum ReaderState { get; set; }

        public ReaderStateChangedEventArgs(ReaderStateEnum readerState)
        {
            ReaderState = readerState;
        }
    }
}