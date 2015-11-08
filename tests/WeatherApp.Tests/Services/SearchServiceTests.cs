using System.IO;
using System.Linq;
using System.Collections.Generic;

using WeatherApp.Services;
using WeatherApp.DataAccess;
using WeatherApp.DataAccess.Entities;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeatherApp.Tests.Services
{
    [TestClass]
    public class SearchServiceTests
    {
        private IDataAccess _data;

        private IDataAccess Data
        {
            get
            {
                if (_data == null)
                {
                    var dir = Directory.GetCurrentDirectory();

                    string filepath = dir + "\\..\\..\\..\\..\\data\\coords.csv";

                    _data = new DataAccess.DataAccess(filepath);
                }

                return _data;
            }
        }

        private ISearchService _searchService;

        private ISearchService SearchService
        {
            get
            {
                if (_searchService == null)
                {
                    _searchService = new SearchService(Data.Locations);
                }

                return _searchService;
            }
        }

        /**
        *   NOTE: the following are not really Unit Tests as they are also partially testing
        *         the DataAccess functionality of reading in from an .csv, but that's what the
        *         instructions specified
        */

        [TestMethod]
        public void Should_return_valid_if_exists()
        {
            var searchString = "Stockholm";
            
            var results = SearchService.Match(searchString);

            Assert.IsTrue(results.Count() > 0);
        }

        [TestMethod]
        public void Should_return_empty_if_not_exists()
        {
            var searchString = "Australia";

            var results = SearchService.Match(searchString);

            Assert.IsTrue(results.Count() == 0);
        }

        /**
        *   The following are more consistent with 'Unit' tests as they are only
        *   testing the SearchService and not reliant on the DataAccess class
        */

        [TestMethod]
        public void Should_return_valid_if_exists_unit()
        {
            var items = new List<Location>
            {
                new Location("Stockholm", 10.0, 15.0),
                new Location("Växjö", 20.0, 20.0)
            };

            var searchService = new SearchService(items);

            var searchString = "Stockholm";

            var results = searchService.Match(searchString);

            Assert.IsTrue(results.Count() == 1);
            Assert.IsTrue(results.First().Lat == 10.0);
            Assert.IsTrue(results.First().Lon == 15.0);
        }

        [TestMethod]
        public void Should_return_empty_if_not_exists_unit()
        {
            var items = new List<Location>
            {
                new Location("Stockholm", 10.0, 15.0),
                new Location("Växjö", 20.0, 20.0)
            };

            var searchService = new SearchService(items);

            var searchString = "Adelaide";

            var results = searchService.Match(searchString);

            Assert.IsTrue(results.Count() == 0);
        }
    }
}
