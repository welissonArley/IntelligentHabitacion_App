using System;
using System.Globalization;
using Xamarin.Forms;

namespace Homuai.App.Converter
{
    public class ShortNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Services.ShortNameConverter().Converter(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
