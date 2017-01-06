using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Timers;

namespace Ipt
{
    /// <summary>Класс для чтения данных.</summary>
    public class DataReader : IDisposable
    {
        #region Свойства

        private static DataReader _instance;
        private static readonly object _padlock = new object();
        private readonly Timer _iptTimer;
        private readonly Timer _scudTimer;

        private Buffer _buffer;

        private IReader<Ipt4> _iptReader;

        private bool _isIptConnected;
        private bool _isScudConnected;
        private IReader<Buffer> _scudReader;

        //Интервал чтения данных со СКУД
        public double ScudInterval
        {
            get { return _scudTimer.Interval; }
            set { _scudTimer.Interval = value; }
        }

        //Интервал чтения данных с ИПТ
        public double IptInterval
        {
            get { return _iptTimer.Interval; }
            set { _iptTimer.Interval = value; }
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
            _scudTimer = new Timer(1000);
            _scudTimer.Elapsed += ScudTimerElapsed;

            _iptTimer = new Timer(250);
            _iptTimer.Elapsed += _iptTimer_Elapsed;

            MbCliWrapper.Connected += (s, e) => { _isScudConnected = true; };
            MbCliWrapper.Disconnected += (s, e) => { _isScudConnected = false; };
            MbCliWrapper.Error +=
                (s, e) =>
                {
                    OnScudError(
                        new DataReaderErrorEventArgs(e.ErrorCode, string.Format("Ошибка СКУД.\n{0}", e.ErrorText)));
                };
        }

        /// <summary>Событие при любых ошибках ридера.</summary>
        public event EventHandler<DataReaderErrorEventArgs> Error;

        /// <remarks>Событие возникает каждый раз, когда данные читаются из ИПТ.</remarks>
        public event EventHandler<DataReadEventArgs> IptDataRead;

        private void _iptTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ReadIpt();
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
            _iptTimer.Stop();
            _iptReader.Disconnect();
            _iptReader.Dispose();
            _isIptConnected = false;
        }

        private void DisconnectScud()
        {
            _scudTimer.Stop();
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
            if (handler != null) handler(this, e);
        }

        protected virtual void OnIptError(DataReaderErrorEventArgs e)
        {
            EventHandler<DataReaderErrorEventArgs> handler = Error;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnScudError(DataReaderErrorEventArgs e)
        {
            EventHandler<DataReaderErrorEventArgs> handler = Error;
            if (handler != null) handler(this, e);
        }

        /// <summary>Чтение данных ИПТ.</summary>
        public void ReadIpt()
        {
            Ipt4 ipt;
            try
            {
                ipt = _iptReader.Read();
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
            OnIptDataRead(new DataReadEventArgs(_buffer, ipt));
        }

        /// <summary>Чтение данных СКУД.</summary>
        public void ReadScud()
        {
            //_buffer = _scudReader.Read();
            var rnd = new Random(DateTime.Now.Millisecond);
            var bytes = new byte[Marshal.SizeOf(typeof(Buffer))];
            rnd.NextBytes(bytes);
            _buffer = bytes.ToStruct<Buffer>();
        }

        private void ScudTimerElapsed(object sender, ElapsedEventArgs e)
        {
            ReadScud();
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
            if (_scudTimer != null) _scudTimer.Dispose();
            if (_iptTimer != null) _iptTimer.Dispose();
        }

        #endregion
    }
}