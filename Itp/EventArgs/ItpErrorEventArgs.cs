using System;

namespace Itp
{
    /// <summary>Параметры события при ошибке</summary>
    public class ItpErrorEventArgs : EventArgs
    {
        /// <summary>Код ошибки</summary>
        public int ErrorCode { get; set; }
        /// <summary>Сообщение об ошибке</summary>
        public string InternalMessage { get; set; }

        public ItpErrorEventArgs(int errorCode, string internalMessage)
        {
            ErrorCode = errorCode;
            InternalMessage = internalMessage;
        }
    }
}