using System;
using System.Globalization;
using System.Windows.Data;

namespace MoneyTrack.WPF.Client.Converters
{
    public class DateTimeToDateTimeOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTimeOffset dttm)
            {
                return dttm.DateTime;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dttm)
            {
                return new DateTimeOffset(dttm);
            }

            return null;
        }
    }
}
