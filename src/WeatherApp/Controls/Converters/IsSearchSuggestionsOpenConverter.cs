using System;
using System.Windows.Data;
using System.Globalization;

namespace WeatherApp.Controls.Converters
{
    public class IsSearchSuggestionsOpenConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // check for valid data -> don't bother opening the suggestions box if not valid...
            if (values.Length < 2 || values.Length > 3) return false;

            if (values[0] is string == false) return false;

            if (values[1] is bool == false) return false;

            string currentText = (string)values[0];
            bool hasFocus = (bool)values[1];

            string defaultText = string.Empty;

            if (values.Length == 3)
            {
                if (values[2] is string)
                {
                    defaultText = (string)values[2];
                }
            }

            // if the search has both valid text and focus, show the suggestions box
            return hasFocus && 
                   !string.IsNullOrWhiteSpace(currentText) &&
                   string.Compare(currentText, defaultText, true) != 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
