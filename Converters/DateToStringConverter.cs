using System;
using System.Globalization;
using voltaire.Resources;
using Xamarin.Forms;

namespace voltaire.Converters
{
    public class DateToStringConverter : IValueConverter
    {

        public object Convert(object valu, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime?) valu;

            if (date == null)
                return AppResources.LastVisit + " ";

            var dt = (DateTime)date;

            return string.Format( AppResources.LastVisit + " {0:D1}/{1:D2}/{2:D3}", dt.Day, dt.Month, dt.Year );

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
