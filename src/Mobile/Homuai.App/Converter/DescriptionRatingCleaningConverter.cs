using System;
using System.Globalization;
using Xamarin.Forms;

namespace Homuai.App.Converter
{
    public class DescriptionRatingCleaningConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null)
            {
                var date = (DateTime)values[0];
                var friend = values[1] as string;

                var dayofWeek = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(date.DayOfWeek);

                return string.Format(ResourceText.DESCRIPTION_DATE_CLEANING_BY_FRIEND,
                    date.ToString(ResourceText.FORMAT_DATE), dayofWeek, friend);
            }

            return "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return (object[])value;
        }
    }
}
