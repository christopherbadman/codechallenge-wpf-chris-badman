using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using WeatherApp.DataAccess.Entities;

namespace WeatherApp.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private IEnumerable<Location> _locations;

        public IEnumerable<Location> Locations
        {
            get
            {
                if (_locations == null) _locations = new List<Location>();

                return _locations;
            }
        }

        /// <summary>
        /// Instantiates a new instance of the data access class
        /// </summary>
        /// <param name="filename">The full path of the file to load the data from</param>
        public DataAccess(string filename)
        {
            try
            {
                if (File.Exists(filename) == false)
                {
                    throw new Exception("File not found.");
                }

                using (var s = new StreamReader(File.OpenRead(filename)))
                {
                    string line;

                    while ((line = s.ReadLine()) != null)
                    {
                        var properties = line.Split(',');

                        var location = ParseToLocation(properties);
                        if (location != null)
                        {
                            // avoid duplicate locations
                            if (Locations.FirstOrDefault(x => string.Compare(x.Name, location.Name, true) == 0) == null)
                            {
                                ((IList<Location>)Locations).Add(location);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Could not read data from file '{0}': {1}", filename, ex.Message));
            }
        }

        /// <summary>
        /// Parses an array of strings into a Location instance
        /// </summary>
        /// <param name="properties">The array of string properties to parse from</param>
        /// <returns>The parsed Location instance, or null if not parsable</returns>
        private Location ParseToLocation(string[] properties)
        {
            Location location = null;

            try
            {
                if (properties.Length != 3) throw new Exception("Location source string is not in the expected format.");

                string name = properties[0];
                double lat = double.Parse(properties[1]);
                double lon = double.Parse(properties[2]);

                location = new Location(name, lat, lon);
            }
            catch (Exception)
            {
                // todo: logging...
            }

            return location;
        }
    }
}
