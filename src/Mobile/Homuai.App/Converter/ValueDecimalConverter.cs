using System;
using System.Globalization;
using Xamarin.Forms;

namespace Homuai.App.Converter
{
    public class ValueDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            /*
             * This function is used in conjunction with DecimalBehavior.
             * We only need ConverterBack, to correctly convert the string to decimal for the Model.
             */
            return decimal.Round((decimal)value, 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            if (string.IsNullOrEmpty(strValue))
                strValue = "0";

            if (decimal.TryParse(strValue, out decimal resultdecimal))
                return resultdecimal;

            return (decimal)0.0;
        }
    }
}
