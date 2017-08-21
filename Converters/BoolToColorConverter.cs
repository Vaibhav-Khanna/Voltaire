using System;
using System.Globalization;
using Xamarin.Forms;

namespace voltaire.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public Color ColorTrue { get; set; }
        public Color ColorFalse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                throw new ArgumentException("Type d'argument invalide");

            return (bool)value ? ColorTrue : ColorFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
