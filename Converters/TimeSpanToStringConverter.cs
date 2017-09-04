using System;
using System.Globalization;
using Xamarin.Forms;

namespace voltaire.Converters
{
    public class TimeSpanToStringConverter : IValueConverter
    {
       

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timespan = (TimeSpan) value;

			var dateTime = new DateTime(timespan.Ticks);
            return dateTime.ToString("h:mm tt", CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
