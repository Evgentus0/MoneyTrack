using MoneyTrack.Core.Models.Operational;
using System;
using System.Globalization;
using System.Windows.Data;

namespace MoneyTrack.WPF.Client.Converters
{
    internal class StringToOperationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string operation)
            {
                return Enum.Parse<Operations>(operation);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
