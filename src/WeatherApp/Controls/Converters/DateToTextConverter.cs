using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherApp.Controls.Converters
{
    public class DateToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTimeOffset == false) return value;

            DateTime input = ((DateTimeOffset)value).LocalDateTime;
            
            DateTime now = DateTime.Now;

            if (input.Year == now.Year && input.Month == now.Month && input.Day == now.Day) return string.Format("Idag {0}/{1}", input.Day, input.Month);

            return string.Format("{0} {1}/{2}", input.DayOfWeek, input.Day, input.Month);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
