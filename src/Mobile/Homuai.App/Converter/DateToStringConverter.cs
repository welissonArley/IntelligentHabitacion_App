using System;
using System.Globalization;
using Xamarin.Forms;

namespace Homuai.App.Converter
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime?)value;
            return date.HasValue ? date.Value.ToString("dd MMMM yyyy") : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
