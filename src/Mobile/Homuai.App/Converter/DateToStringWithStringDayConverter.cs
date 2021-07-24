using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace Homuai.App.Converter
{
    public class DateToStringWithStringDayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;

            var dayofWeek = culture.DateTimeFormat.GetDayName(date.DayOfWeek);

            return $"{dayofWeek.First().ToString().ToUpper() + dayofWeek.Substring(1)} • {date.Day} {date:MMM} {date.Year}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
