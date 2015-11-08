using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace WeatherApp.Controls.Converters
{
    public class NullToVisibiltyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool visibleIfNull = (parameter == null) || (parameter is bool == false) || (bool)parameter;

            bool visible = (visibleIfNull && (value == null)) || (!visibleIfNull && (value != null));

            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
