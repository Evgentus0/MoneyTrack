using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MoneyTrack.WPF.Client.Converters
{
    internal class HideIfCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool isChecked)
            {
                return isChecked ? Visibility.Hidden : Visibility.Visible;
            }
            throw new ArgumentException("Value should be bool!");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
