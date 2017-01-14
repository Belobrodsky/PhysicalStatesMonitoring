using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

//Основа таймера взята с http://stackoverflow.com/questions/24839105/high-resolution-timer-in-c-sharp

namespace Ipt.Timer
{
    public class MultimediaTimer : IDisposable
    {
        #region Свойства

        // Hold the timer callback to prevent garbage collection.
        private readonly NativeMethods.MultimediaTimerCallback _callback;
        private bool _disposed;
        private int _interval;
        private int _resolution;
        private uint _timerId;

        public int Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                CheckDisposed();

                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");

                _interval = value;
                if (Resolution > Interval)
                    Resolution = value;
                if(IsRunning)
                {
                    StopInternal();
                    Start();
                }
            }
        }

        // Note minimum resolution is 0, meaning highest possible resolution.
        private int Resolution
        {
            get
            {
                return _resolution;
            }
            set
            {
                CheckDisposed();

                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");

                _resolution = value;
            }
        }

        public bool IsRunning
        {
            get
            {
                return _timerId != 0;
            }
        }

        #endregion

        public MultimediaTimer()
            :this(25)
        {
        }

        public MultimediaTimer(int interval)
        {
            _callback = TimerCallbackMethod;
            Resolution = 15;
            Interval = interval;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        ~MultimediaTimer()
        {
            Dispose(false);
        }

        public event EventHandler Elapsed;

        private void CheckDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException("MultimediaTimer");
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _disposed = true;
            if (IsRunning)
            {
                StopInternal();
            }

            if (!disposing)
                return;
            Elapsed = null;
            GC.SuppressFinalize(this);
        }

        public void Start()
        {
            CheckDisposed();

            if (IsRunning)
                throw new InvalidOperationException("Timer is already running");

            // Event type = 0, one off event
            // Event type = 1, periodic event
            uint userCtx = 0;
            _timerId = NativeMethods.TimeSetEvent((uint) Interval, (uint) Resolution, _callback, ref userCtx, 1);
            if (_timerId != 0)
                return;
            int error = Marshal.GetLastWin32Error();
            throw new Win32Exception(error);
        }

        public void Stop()
        {
            CheckDisposed();

            if (!IsRunning)
                throw new InvalidOperationException("Timer has not been started");

            StopInternal();
        }

        private void StopInternal()
        {
            NativeMethods.TimeKillEvent(_timerId);
            _timerId = 0;
        }

        private void TimerCallbackMethod(uint id, uint msg, ref uint userCtx, uint rsv1, uint rsv2)
        {
            var handler = Elapsed;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}