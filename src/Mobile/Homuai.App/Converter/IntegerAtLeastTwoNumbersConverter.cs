using System;
using System.Globalization;
using Xamarin.Forms;

namespace Homuai.App.Converter
{
    public class IntegerAtLeastTwoNumbersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var number = (int?)value;
            return number.HasValue ? $"{(number < 10 ? "0" : "")}{number}" : "00";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
