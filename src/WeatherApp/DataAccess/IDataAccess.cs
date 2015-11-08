using System.Collections.Generic;

using WeatherApp.DataAccess.Entities;

namespace WeatherApp.DataAccess
{
    public interface IDataAccess
    {
        /// <summary>
        /// The collection of available locations obtained from the data source
        /// </summary>
        IEnumerable<Location> Locations { get; }
    }
}
