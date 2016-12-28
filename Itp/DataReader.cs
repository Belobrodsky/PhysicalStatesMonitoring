using System;
using System.Diagnostics;
using System.Net;
using System.Timers;

namespace Itp
{
    /// <summary>Класс для чтения данных.</summary>
    public class DataReader
    {
        /// <summary>Событие при чтении данных.</summary>
        /// <remarks>Событие возникает каждый раз, когда данные читаются. Это не означает, что они изменились в СКУД.</remarks>
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
                return _isIptConnected && _isScudConnected
                     ? ReaderStateEnum.Connected
                     : ReaderStateEnum.Disconnected;

            }
        }

        public DataReader()
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
            //TODO: Состояние соединения должно зависеть от результат соединения с ИПТ и со СКУД. Сейчас зависит только от СКУД
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
            _iptReader = new IptReader(address, port);
            _iptReader.Connect();
            _isIptConnected = true;
            //throw new NotImplementedException();
        }

        private void ConnectScud(IPAddress address, int port)
        {
            _scudReader = new ScudReader(address, port);
            _isScudConnected = true;
        }

        private void DisconnectScud()
        {
            _scudReader.Disconnect();
            _scudReader = null;
        }

        //TODO:Добавить реализацию отсоединения от ИПТ
        private void DisconnectIpt()
        {
            _iptReader.Disconnect();
            _iptReader = null;
            _isIptConnected = false;
            Debug.WriteLine("DisconnectIpt();");
            //throw new NotImplementedException();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Read();
        }

        public void Read()
        {
            var buff = _scudReader.Read();
            var ipt = _iptReader.Read();
            //Вызов события
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
    }
}
