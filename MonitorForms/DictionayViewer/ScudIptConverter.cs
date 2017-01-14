using System;
using System.ComponentModel;
using System.Globalization;

namespace MonitorForms
{
    /// <summary>
    /// Класс-конвертер для отображения значений в таблице
    /// </summary>
    internal class ScudIptConverter : TypeConverter
    {
        private static readonly object _obj = new object();
        private static ScudIptConverter _converter;

        public static ScudIptConverter GetInstance()
        {
            lock (_obj)
            {
                return _converter ?? (_converter = new ScudIptConverter());
            }
        }

        private ScudIptConverter()
        {
            
        }

        #region Overrides of TypeConverter

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            string format = string.Empty;
            switch (context.PropertyDescriptor.Name)
            {
                case "I_1":
                case "I_2":
                    format = "e2";
                    break;
                case "R_1":
                case "R_2":
                    format = "f3";
                    break;
                default:
                    format = "f2";
                    break;
            }
            return Math.Abs(((double)value)) < double.Epsilon 
                ? base.ConvertTo(context, culture, value, destinationType) 
                : ((double)value).ToString(format, culture);
        }

        #endregion
    }
}