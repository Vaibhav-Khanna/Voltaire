using System;
using System.Globalization;
using voltaire.Models;
using Xamarin.Forms;

namespace voltaire.Converters
{

    public class EnumColorToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
                throw new ArgumentException("Invalid argument type!");

            var color = (string)value;



            if (color.Equals(ColorEnum.Gray.ToString()))
            {
                return "grayfilter";
            }
            else if (color.Equals(ColorEnum.Green.ToString()))
            {
                return "greenfilter";
            }
            else if (color.Equals(ColorEnum.Orange.ToString()))
            {
                return "orangefilter";
            }
            else if (color.Equals(ColorEnum.Red.ToString()))
            {
                return "redfilter";
            }
            else
            {
                return "All";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}