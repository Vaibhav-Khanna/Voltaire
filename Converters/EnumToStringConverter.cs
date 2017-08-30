using System;
using System.Globalization;
using Xamarin.Forms;

namespace voltaire.Converters
{
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object valu, Type targetType, object parameter, CultureInfo culture)
        {
            var en = (Models.QuotationStatus) valu;

            return en.ToString();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
