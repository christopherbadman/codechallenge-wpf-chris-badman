using System;
using System.Windows.Data;
using System.Globalization;

namespace WeatherApp.Controls.Converters
{
    public class SearchTextToTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string defaultText = string.Empty;

            if (values.Length < 2 || values.Length > 3) return defaultText;

            // check if the default text has been provided
            if (values.Length == 3)
            {
                if (values[2] is string == false) return defaultText;

                defaultText = (string)values[2];
            }

            // incorrect data types provided
            if (values[0] is string == false) return defaultText;
            if (values[1] is bool == false) return defaultText;

            string currentText = (string)values[0];
            bool hasFocus = (bool)values[1];

            if (hasFocus) return currentText;

            return (!string.IsNullOrWhiteSpace(currentText)) ? currentText : defaultText;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
