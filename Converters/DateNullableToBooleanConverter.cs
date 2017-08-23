using System;
using System.Globalization;
using Xamarin.Forms;
namespace voltaire.Converters
{
    public class DateNullableToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals("01/01/0001 00:00:00")) { return false; } else { return value != null; }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}