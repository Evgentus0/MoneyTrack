using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MoneyTrack.WPF.Client.Converters
{
    public class CollapseIfEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string strValue)
            {
                if (string.IsNullOrEmpty(strValue))
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }

            else if(value is int intValue)
            {
                if(intValue <= 0)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
