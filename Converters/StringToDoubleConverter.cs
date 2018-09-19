using System;
using System.Globalization;
using Xamarin.Forms;

namespace voltaire.Converters
{
    public class StringToDoubleConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result;

            bool isValid = double.TryParse(value as string, out result);

            if (isValid)
                return result;
            else
                return 0;
        }
    }
}
