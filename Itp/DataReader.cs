using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Timers;
using GraphMonitor;
using Ipt.Timer;

namespace Ipt
{
    /// <summary>Класс для чтения данных.</summary>
    public class DataReader : IDisposable
    {
        #region Свойства

        private static DataReader _instance;
        private static readonly object _padlock = new object();
        private readonly MultimediaTimer _iptTimer;
        private readonly MultimediaTimer _scudTimer;

        private Buffer _buffer;

        private Ipt4 _ipt;

        private IReader<Ipt4> _iptReader;

        private bool _isIptConnected;
        private bool _isScudConnected;
        private IReader<Buffer> _scudReader;

        //Интервал чтения данных со СКУД
        public int ScudInterval
        {
            get
            {
                return _scudTimer.Interval;
            }
            set
            {
                _scudTimer.Interval = value;
            }
        }

        //Интервал чтения данных с ИПТ
        public int IptInterval
        {
            get
            {
                return _iptTimer.Interval;
            }
            set
            {
                _iptTimer.Interval = value;
            }
        }

        /// <summary>Состояние ридера. <see cref="ReaderStateEnum" /></summary>
        /// <remarks>
        ///     Возможные состояния ридера:
        ///     <para><see cref="ReaderStateEnum.Connected" /> — ридер соединён со СКУД и ИПТ. Устанавливается извне.</para>
        ///     <para><see cref="ReaderStateEnum.Disconnected" /> — ридер отсоединён от СКУД и ИПТ. Устанавливается извне.</para>
        ///     <para><see cref="ReaderStateEnum.DataReading" /> — ридер читает данные со СКУД и ИПТ.</para>
        /// </remarks>
        public ReaderStateEnum ReaderState
        {
            get
            {
                return _isIptConnected || _isScudConnected
                    ? ReaderStateEnum.Connected
                    : ReaderStateEnum.Disconnected;
            }
        }

        #endregion

        private DataReader()
        {
            _scudTimer = new MultimediaTimer(1000);
            _scudTimer.Elapsed += _scudTimerElapsed;

            _iptTimer = new MultimediaTimer(250);
            _iptTimer.Elapsed += _iptTimer_Elapsed;

            MbCliWrapper.Connected += (s, e) =>
            {
                _isScudConnected = true;
            };
            MbCliWrapper.Disconnected += (s, e) =>
            {
                _isScudConnected = false;
            };
            MbCliWrapper.Error +=
                (s, e) =>
                {
                    OnScudError(
                        new DataReaderErrorEventArgs(e.ErrorCode, string.Format("Ошибка СКУД.\n{0}", e.ErrorText)));
                };
        }

        /// <summary>Событие при любых ошибках ридера.</summary>
        public event EventHandler<DataReaderErrorEventArgs> Error;

        /// <summary>Событие возникает каждый раз, когда данные читаются из ИПТ.</summary>
        public event EventHandler<DataReadEventArgs> IptDataRead;

        /// <summary>Событие возникает каждый раз, когда данные читаются из СКУД.</summary>
        public event EventHandler<DataReadEventArgs> ScudDataRead;

        private void _iptTimer_Elapsed(object sender, EventArgs eventArgs)
        {
            ReadIpt();
        }

        private void _scudTimerElapsed(object sender, EventArgs eventArgs)
        {
            ReadScud();
        }

        /// <summary>Соединение с ИПТ и СКУД.</summary>
        public void Connect(IPAddress scudAddress, int scudPort, IPAddress iptAddress, int iptPort)
        {
            ConnectIpt(iptAddress, iptPort);
            ConnectScud(scudAddress, scudPort);
        }

        private void ConnectIpt(IPAddress address, int port)
        {
            _isIptConnected = false;
            try
            {
                _iptReader = IptReader.GetInstance(address, port);
                _iptReader.Connect();
            }
            catch (SocketException ex)
            {
                OnIptError(new DataReaderErrorEventArgs(ex.ErrorCode, ex.Message));
                return;
            }
            _isIptConnected = true;
        }

        private void ConnectScud(IPAddress address, int port)
        {
            _scudReader = ScudReader.GetInstance(address, port);
            _scudReader.Connect();
        }

        /// <summary>Отсоединение от ИПТ и СКУД.</summary>
        public void Disconnect()
        {
            Stop();
            DisconnectScud();
            DisconnectIpt();
        }

        private void DisconnectIpt()
        {
            _iptReader.Disconnect();
            _iptReader.Dispose();
            _isIptConnected = false;
        }

        private void DisconnectScud()
        {
            _scudReader.Disconnect();
            _scudReader.Dispose();
        }

        public static DataReader GetInstance()
        {
            lock (_padlock)
            {
                if (_instance != null)
                {
                    return _instance;
                }
                _instance = new DataReader();
                return _instance;
            }
        }

        protected virtual void OnIptDataRead(DataReadEventArgs e)
        {
            EventHandler<DataReadEventArgs> handler = IptDataRead;
            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnIptError(DataReaderErrorEventArgs e)
        {
            EventHandler<DataReaderErrorEventArgs> handler = Error;
            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnScudDataRead(DataReadEventArgs e)
        {
            EventHandler<DataReadEventArgs> handler = ScudDataRead;
            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnScudError(DataReaderErrorEventArgs e)
        {
            EventHandler<DataReaderErrorEventArgs> handler = Error;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>Чтение данных ИПТ.</summary>
        public void ReadIpt()
        {
            try
            {
                _ipt = _iptReader.Read();
            }
            catch (SocketException ex)
            {
                DisconnectIpt();
                OnIptError(new DataReaderErrorEventArgs(ex.ErrorCode, ex.Message));
                return;
            }
            catch (Exception ex)
            {
                DisconnectIpt();
                OnIptError(new DataReaderErrorEventArgs(0, ex.Message));
                return;
            }
            OnIptDataRead(new DataReadEventArgs(_buffer, _ipt));
        }

        /// <summary>Чтение данных СКУД.</summary>
        public void ReadScud()
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            var bytes = new byte[Marshal.SizeOf(typeof(Buffer))];
            rnd.NextBytes(bytes);
            _buffer = bytes.ToStruct<Buffer>();
            OnScudDataRead(new DataReadEventArgs(_buffer, _ipt));
        }

        /// <summary>Начать чтение данных.</summary>
        public void Start()
        {
            if (ReaderState == ReaderStateEnum.Disconnected)
            {
                Debug.WriteLine("Не подсоединён");
                return;
            }
            //Поскольку таймер сработает не сразу, а через интервал, то нужно считать значения в нулевой момент времени.
            PerformanceMeter.StartTime = DateTime.Now;
            ReadScud();
            ReadIpt();

            _scudTimer.Start();
            _iptTimer.Start();
        }

        /// <summary>Остановить чтение данных.</summary>
        public void Stop()
        {
            _scudTimer.Stop();
            _iptTimer.Stop();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_scudTimer != null)
                _scudTimer.Dispose();
            if (_iptTimer != null)
                _iptTimer.Dispose();
        }

        #endregion
    }
}