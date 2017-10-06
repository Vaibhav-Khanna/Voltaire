using System;
using System.Globalization;
using Xamarin.Forms;

namespace voltaire.Converters
{
    public class BoolToLineBreakModeConverter : IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return LineBreakMode.TailTruncation;
            else
                return LineBreakMode.WordWrap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
