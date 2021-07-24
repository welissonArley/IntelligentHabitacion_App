using System;
using System.Globalization;
using Xamarin.Forms;

namespace Homuai.App.Converter
{
    public class SvgImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imageSource = (FileImageSource)value;

            return $"resource://Homuai.App.Resources.{imageSource.File}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
