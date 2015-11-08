using System.Collections.Generic;

using WeatherApp.DataAccess.Entities;

namespace WeatherApp.Services
{
    public interface ISearchService
    {
        /// <summary>
        /// Searches the data source for a string
        /// </summary>
        /// <param name="str">The string to search for</param>
        /// <returns>The matched Location instance that the search string </returns>
        IEnumerable<Location> Match(string str);
    }
}
