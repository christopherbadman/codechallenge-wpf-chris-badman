using System;
using System.Linq;
using System.Collections.Generic;

using WeatherApp.DataAccess.Entities;

namespace WeatherApp.Services
{
    public class SearchService : ISearchService
    {
        private readonly IEnumerable<Location> _locations;

        public SearchService(IEnumerable<Location> locations)
        {
            if (locations == null) throw new ArgumentNullException("locations");

            _locations = locations;
        }

        public IEnumerable<Location> Match(string str)
        {
            var matches = new List<Location>();

            if (string.IsNullOrWhiteSpace(str) == false)
            {
                matches = _locations.Where(x => x.Name.IndexOf(str, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();
            }

            return matches;
        }
    }
}
