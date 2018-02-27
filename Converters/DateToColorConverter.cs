using System;
using System.Globalization;
using Xamarin.Forms;

namespace voltaire.Converters
{
    public class DateToColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = (DateTime?)value;

            DateTime date;

            if (item.HasValue)
                date = item.Value;
            else
                return "#eb1010"; // red

            if (DateTime.Now.Subtract(date).Days <= 7)
            {
                return "#13c10d"; // green
            }
            else if (DateTime.Now.Subtract(date).Days <= 30)
            {
                return "#fc9835"; // orange
            }
            else if (DateTime.Now.Subtract(date).Days > 30)
            {
                return "#eb1010";  // red
            }
            else 
                return "#eb1010"; // red

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
