using System;
using System.Globalization;
using Xamarin.Forms;

namespace voltaire.Converters
{
    public class StringToBoolConverter : IValueConverter
    {
		public object Convert(object valu, Type targetType, object parameter, CultureInfo culture)
		{
            var item = valu as string;

            if (string.IsNullOrWhiteSpace(item))
            return false;
            else
            {
                if (item.ToLower() == "true")
                    return true;
                else
                    return false;
            }

		}

		public object ConvertBack(object valu, Type targetType, object parameter, CultureInfo culture)
		{
            var item = (bool) valu;

            if (item)
                return "true";
            else
                return "false";

		}
    }
}
