using System;

namespace Ipt
{
    public class DataReaderErrorEventArgs : EventArgs
    {
        #region Свойства

        public int ErrorCode { get; set; }
        public string ErrorText { get; set; }

        #endregion

        public DataReaderErrorEventArgs(int errorCode, string errorText)
        {
            ErrorCode = errorCode;
            ErrorText = errorText;
        }
    }
}