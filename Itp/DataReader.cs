using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Timers;

namespace Itp
{
    /// <summary>Класс для чтения данных со СКУД.</summary>
    public class DataReader
    {
        /// <summary>Событие при чтении данных со СКУД.</summary>
        /// <remarks>Событие возникает каждый раз, когда данные читаются. Это не означает, что они изменились в СКУД.</remarks>
        public event EventHandler<DataReadEventArgs> DataRead;
        /// <summary>Событие при изменении состояния ридера. <see cref="ReaderStateEnum"/>.</summary>
        public event EventHandler<ReaderStateChangedEventArgs> StateChanged;
        /// <summary>Событие при любых ошибках ридера.</summary>
        public event EventHandler<DataReaderErrorEventArgs> ErrorOccured;

        private readonly Timer _timer;
        private ReaderStateEnum _readerState;
        private Socket _socket;
        private DataWriter _dataWriter;
        //Интервал чтения данных
        public double Interval
        {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }

        /// <summary>Состояние ридера. <see cref="ReaderStateEnum"/></summary>
        /// <remarks>Возможные состояния ридера:
        /// <para><see cref="ReaderStateEnum.Connected"/> — ридер соединён со СКУД. Устанавливается извне.</para>
        /// <para><see cref="ReaderStateEnum.Disconnected"/> — ридер отсоединён от СКУД. Устанавливается извне.</para>
        /// <para><see cref="ReaderStateEnum.DataReading"/> — ридер читает данные со СКУД.</para>
        /// </remarks>
        public ReaderStateEnum ReaderState
        {
            get { return _readerState; }
            set
            {
                if (_readerState == value) return;
                _readerState = value;
                OnStateChanged(new ReaderStateChangedEventArgs(_readerState));
            }
        }

        public DataReader(DataWriter dataWriter)
        {
            _dataWriter = dataWriter;
            _dataWriter.WriteHeaders();
            _readerState = ReaderStateEnum.Disconnected;
            _timer = new Timer(1000);
            _timer.Elapsed += _timer_Elapsed;
            MbCliWrapper.Connected += (s, e) =>
            {
                ReaderState = ReaderStateEnum.Connected;
            };
            MbCliWrapper.Disconnected += (s, e) =>
            {
                ReaderState = ReaderStateEnum.Disconnected;
            };
            MbCliWrapper.ErrorOccured += (s, e) =>
            {
                OnErrorOccured(new DataReaderErrorEventArgs(e.ErrorCode, string.Format("Ошибка СКУД.\n{0}", e.InternalMessage)));
            };

            //_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //_socket.Connect(address, port);
        }

        /// <summary>Начать чтение данных.</summary>
        public void Start()
        {
            _timer.Start();
            ReaderState = ReaderStateEnum.DataReading;
        }

        /// <summary>Остановить чтение данных.</summary>
        public void Stop()
        {
            _timer.Stop();
        }

        /// <summary>Соединение с ИПТ и СКУД.</summary>
        public void Connect(IPAddress scudAddress, int scudPort, IPAddress iptAddress, int iptPort)
        {
            ConnectScud(scudAddress, scudPort);
            ConnectIpt(iptAddress, iptPort);
        }

        /// <summary>Отсоединение от ИПТ и СКУД.</summary>
        public void Disconnect()
        {
            Stop();
            DisconnectScud();
            DisconnectIpt();
        }
        //TODO:Добавить реализацию соединения с ИПТ
        private void ConnectIpt(IPAddress address, int port)
        {
            Debug.WriteLine("ConnectIpt();");
            //throw new NotImplementedException();
        }

        private void ConnectScud(IPAddress address, int port)
        {
            MbCliWrapper.Connect(address, port);
        }

        private void DisconnectScud()
        {
            MbCliWrapper.Disconnect();
        }

        //TODO:Добавить реализацию отсоединения от ИПТ
        private void DisconnectIpt()
        {
            Debug.WriteLine("DisconnectIpt();");
            //throw new NotImplementedException();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Read();
        }

        //TODO:Добавить чтение данных с ИПТ
        public void Read()
        {
            var buff = new Buffer();
            var size = Marshal.SizeOf(buff);
            //Debug.WriteLine("Размер структуры: {0}", size);
            //Выделение памяти под структуру
            var ptr = Marshal.AllocHGlobal(size);
            //Чтение данных
            MbCliWrapper.HoldRegisters(0, 1000, ptr);
            //Запись данных из памяти в структуру
            buff = (Buffer)Marshal.PtrToStructure(ptr, typeof(Buffer));
            //Освобождение памяти
            Marshal.FreeHGlobal(ptr);
            //Вызов события
            OnDataRead(new DataReadEventArgs(buff));
            //TODO:Добавить вычисление токов перед записью в файл
            _dataWriter.WriteData(buff);
        }

        protected virtual void OnDataRead(DataReadEventArgs e)
        {
            EventHandler<DataReadEventArgs> handler = DataRead;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnStateChanged(ReaderStateChangedEventArgs e)
        {
            EventHandler<ReaderStateChangedEventArgs> handler = StateChanged;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnErrorOccured(DataReaderErrorEventArgs e)
        {
            EventHandler<DataReaderErrorEventArgs> handler = ErrorOccured;
            if (handler != null) handler(this, e);
        }
    }
}
