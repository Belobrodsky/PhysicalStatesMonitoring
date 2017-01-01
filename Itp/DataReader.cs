using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace Ipt
{
    /// <summary>Класс для чтения данных.</summary>
    public class DataReader : IDisposable
    {
        /// <summary>Событие при чтении данных.</summary>
        /// <remarks>Событие возникает каждый раз, когда данные читаются. Это не означает, что они изменились в СКУД или ИПТ.</remarks>
        public event EventHandler<DataReadEventArgs> DataRead;
        /// <summary>Событие при любых ошибках ридера.</summary>
        public event EventHandler<DataReaderErrorEventArgs> ErrorOccured;

        private readonly Timer _timer;
        private IReader<Ipt4> _iptReader;
        private IReader<Buffer> _scudReader;
        private bool _isIptConnected;
        private bool _isScudConnected;

        //Интервал чтения данных
        public double Interval
        {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }

        /// <summary>Состояние ридера. <see cref="ReaderStateEnum"/></summary>
        /// <remarks>Возможные состояния ридера:
        /// <para><see cref="ReaderStateEnum.Connected"/> — ридер соединён со СКУД и ИПТ. Устанавливается извне.</para>
        /// <para><see cref="ReaderStateEnum.Disconnected"/> — ридер отсоединён от СКУД и ИПТ. Устанавливается извне.</para>
        /// <para><see cref="ReaderStateEnum.DataReading"/> — ридер читает данные со СКУД и ИПТ.</para>
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

        private static DataReader _instance;
        private static readonly object _padlock = new object();
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

        private DataReader()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += _timer_Elapsed;

            MbCliWrapper.Connected += (s, e) =>
            {
                _isScudConnected = true;
            };
            MbCliWrapper.Disconnected += (s, e) =>
            {
                _isScudConnected = false;
            };
            MbCliWrapper.ErrorOccured += (s, e) =>
            {
                OnErrorOccured(new DataReaderErrorEventArgs(e.ErrorCode, string.Format("Ошибка СКУД.\n{0}", e.ErrorText)));
            };
        }

        /// <summary>Начать чтение данных.</summary>
        public void Start()
        {
            if (ReaderState == ReaderStateEnum.Disconnected)
            {
                Debug.WriteLine("Не подсоединён");
                return;
            }
            _timer.Start();
        }

        /// <summary>Остановить чтение данных.</summary>
        public void Stop()
        {
            _timer.Stop();
        }

        /// <summary>Соединение с ИПТ и СКУД.</summary>
        public void Connect(IPAddress scudAddress, int scudPort, IPAddress iptAddress, int iptPort)
        {
            ConnectIpt(iptAddress, iptPort);
            ConnectScud(scudAddress, scudPort);
        }

        /// <summary>Отсоединение от ИПТ и СКУД.</summary>
        public void Disconnect()
        {
            Stop();
            DisconnectScud();
            DisconnectIpt();
        }

        private void ConnectIpt(IPAddress address, int port)
        {
            _isIptConnected = false;
            try
            {
                _iptReader = IptReader.GetInstance(address, port);
            }
            catch (SocketException ex)
            {
                OnErrorOccured(new DataReaderErrorEventArgs(ex.ErrorCode, ex.Message));
                return;
            }
            _isIptConnected = true;
        }

        private void ConnectScud(IPAddress address, int port)
        {
            _scudReader = ScudReader.GetInstance(address, port);
            _scudReader.Connect();
        }

        private void DisconnectScud()
        {
            //TODO:Сделать работу СКУД и ИПТ независимыми.
            //Сейчас каким-то образом подключённый СКУД мешает повторному соединению с эмулятором ИПТ
            _scudReader.Disconnect();
            _scudReader.Dispose();
        }

        private void DisconnectIpt()
        {
            _iptReader.Dispose();
            _isIptConnected = false;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Read();
        }

        /// <summary>Чтение данных.</summary>
        public void Read()
        {
            var buff = _scudReader.Read();
            Ipt4 ipt;
            try
            {
                ipt = _iptReader.Read();
            }
            catch (SocketException ex)
            {
                Disconnect();
                Debug.WriteLine(ex.Message);
                OnErrorOccured(new DataReaderErrorEventArgs(ex.ErrorCode, ex.Message));
                return;
            }
            //Вызов события DataRead
            OnDataRead(new DataReadEventArgs(buff, ipt));
        }

        protected virtual void OnDataRead(DataReadEventArgs e)
        {
            EventHandler<DataReadEventArgs> handler = DataRead;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnErrorOccured(DataReaderErrorEventArgs e)
        {
            EventHandler<DataReaderErrorEventArgs> handler = ErrorOccured;
            if (handler != null) handler(this, e);
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

            if (_timer != null) _timer.Dispose();
        }

        #endregion
    }
}
