namespace WeatherApp.Services.Model
{
    using System;
    using System.Linq;

    public class Forecast
    {
        public double Lat { get; set; }

        public double Lon { get; set; }

        public DateTimeOffset ReferenceTime { get; set; }

        public TimeSeries[] TimeSeries { get; set; }

        public TimeSeries TimeSeriesForDate(DateTime dateTime)
        {
            TimeSeries series = null;

            if (TimeSeries != null)
            {
                series = TimeSeries.Where(ts =>
                {
                    if (ts.ValidTime == null) return false;

                    var local = ts.ValidTime.ToLocalTime();

                    // only include series valid for the specified date
                    return (local.Year == dateTime.Year && local.Month == dateTime.Month && local.Day == dateTime.Day);
                })
                .OrderBy(ts => Math.Abs(12 - ts.ValidTime.ToLocalTime().Hour))  // get the closest time to midday
                .FirstOrDefault();
            }

            return series;
        }

        public override string ToString()
        {
            return string.Format("ReferenceTime={0}, Latitude={1}, Longitude={2}", ReferenceTime, Lat, Lon);
        }
    }
}