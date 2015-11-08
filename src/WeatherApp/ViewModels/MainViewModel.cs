using System;
using System.Collections;
using System.Collections.Generic;
using WeatherApp.DataAccess;
using WeatherApp.DataAccess.Entities;
using WeatherApp.Services;
using WeatherApp.Services.Model;

namespace WeatherApp.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly IWeatherForecastService _weatherForecastService;

        public MainViewModel(IDataAccess dataAccess)
        {
            Title = "Code Challenge Weather App";
            MapViewModel = new MapViewModel(59.326142, 17.9875455, 8.0);
            Locations = dataAccess.Locations;
            SearchService = new SearchService(dataAccess.Locations);

            _weatherForecastService = new WeatherForecastService();
        }

        public string Title { get; private set; }

        public MapViewModel MapViewModel { get; private set; }

        public IEnumerable<Location> Locations { get; private set; }

        public ISearchService SearchService { get; private set; }

        private Location _selectedLocation;

        public Location SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                if (_selectedLocation != value)
                {
                    _selectedLocation = value;

                    // update the forecast
                    SelectedForecast = GetForecastForLocation(_selectedLocation);

                    if (_selectedLocation != null)
                    {
                        MapViewModel.Center = new Microsoft.Maps.MapControl.WPF.Location(_selectedLocation.Lat, _selectedLocation.Lon);
                    }

                    NotifyPropertyChanged();
                }
            }
        }

        private Forecast _selectedForecast;

        /// <summary>
        /// The forecast details of the selected city
        /// </summary>
        public Forecast SelectedForecast
        {
            get { return _selectedForecast; }
            private set
            {
                if (_selectedForecast != value)
                {
                    _selectedForecast = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Attempts to get a forecast for a location
        /// </summary>
        /// <param name="location">The location to get the forecast for</param>
        /// <returns>The forecast for the location, or null if not found</returns>
        private Forecast GetForecastForLocation(Location location)
        {
            Forecast forecast = null;

            if (location != null)
            {
                try
                {
                    forecast = _weatherForecastService.GetForecastByCoords(_selectedLocation.Lat, _selectedLocation.Lon);
                }
                catch (Exception)
                {
                    // todo: log
                }
            }

            return forecast;
        }
    }
}