using System;
using System.ComponentModel;
using System.Globalization;

namespace GraphMonitor
{
    internal class TimeStampConverter : TypeConverter
    {
        #region Overrides of TypeConverter

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                                         Type destinationType)
        {
            if (value is DateTime)
                return ( (DateTime) value ).ToString("HH:mm:ss fff мс");
            return base.ConvertTo(context, culture, value, destinationType);
        }

        #endregion
    }
}