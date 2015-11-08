using System;
using System.Globalization;
using System.Windows.Data;

using WeatherApp.Services.Model;

namespace WeatherApp.Controls.Converters
{
    public class SeriesToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var icon = "PartlySunny";

            if (value is TimeSeries)
            {
                var series = (TimeSeries)value;
                
                if (series.Pcat == 1 || series.Pcat == 2)
                {
                    // prioritise snow first...

                    icon = "Snowy";
                }
                else if (series.Pcat == 3 || series.Pcat == 4)
                {
                    // or rain next...

                    icon = "Rainy";
                }
                else if (series.Tcc > 4)
                {
                    // then check the cloudiness...

                    icon = "Cloudy";
                }
                else if (series.Tcc > 1)
                {
                    icon = "PartlySunny";
                }
                else
                {
                    icon = "Sunny";
                }
            }
        
            return string.Format(@"pack://application:,,,/Images/{0}.jpg", icon);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
