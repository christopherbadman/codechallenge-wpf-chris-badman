using System;
using System.Diagnostics;

namespace WeatherApp.DataAccess.Entities
{    
    [DebuggerDisplay("Name = {Name}, Lat = {Lat}, Lon = {Lon}")]
    public class Location
    {
        /// <summary>
        /// The user friendly name of the location
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The latitudinal coordinate of the location
        /// </summary>
        public double Lat { get; private set; }

        /// <summary>
        /// The longitudinal coordinate of the location
        /// </summary>
        public double Lon { get; private set; }

        /// <summary>
        /// Instantiates a new location
        /// </summary>
        /// <param name="name">The user friendly name of the location</param>
        /// <param name="lat">The latitudinal coordinate of the location</param>
        /// <param name="lon">The longitudinal coordinate of the location</param>
        public Location(string name, double lat, double lon)
        {
            if (name == null) throw new ArgumentNullException("Name");

            Name = name;
            Lat = lat;
            Lon = lon;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
