using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

using WeatherApp.Services.Model;
using WeatherApp.DataAccess.Entities;

namespace WeatherApp.Controls
{
    /// <summary>
    /// Interaction logic for ResultCarousel.xaml
    /// </summary>
    public partial class ResultCarousel : UserControl
    {
        // maximum results the user can navigate forward from the first
        private const int MaxSteps = 10;

        public static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register("Location", typeof(Location), typeof(ResultCarousel));

        /// <summary>
        /// The current location result being displayed
        /// </summary>
        public Location Location
        {
            get { return (Location)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }

        public static readonly DependencyProperty ForecastProperty =
            DependencyProperty.Register("Forecast", typeof(Forecast), typeof(ResultCarousel), new PropertyMetadata(new PropertyChangedCallback(OnForecastChanged)));

        /// <summary>
        /// The current forecast result being displayed
        /// </summary>
        public Forecast Forecast
        {
            get { return (Forecast)GetValue(ForecastProperty); }
            set { SetValue(ForecastProperty, value); }
        }

        public static readonly DependencyProperty NoLocationTextProperty =
            DependencyProperty.Register("NoLocationText", typeof(string), typeof(ResultCarousel));

        /// <summary>
        /// The string to display when no location is selected
        /// </summary>
        public string NoLocationText
        {
            get { return (string)GetValue(NoLocationTextProperty); }
            set { SetValue(NoLocationTextProperty, value); }
        }

        protected static readonly DependencyProperty CurrentTimeSeriesProperty =
            DependencyProperty.Register("CurrentTimeSeries", typeof(TimeSeries), typeof(ResultCarousel));

        /// <summary>
        /// The currently displayed time series
        /// </summary>
        protected TimeSeries CurrentTimeSeries
        {
            get { return (TimeSeries)GetValue(CurrentTimeSeriesProperty); }
            set { SetValue(CurrentTimeSeriesProperty, value); }
        }

        private int _currentDayOffset;

        /// <summary>
        /// The currently shown day offset to today
        /// </summary>
        private int CurrentDayOffset
        {
            get { return _currentDayOffset; }
            set
            {
                if (_currentDayOffset != value)
                {
                    _currentDayOffset = value;
                }

                SetTimeSeries(_currentDayOffset);

                // command execution may have changed
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private ICommand _navigateForward;

        /// <summary>
        /// Command to navigate the carousel forwards one step
        /// </summary>
        public ICommand NavigateForward
        {
            get
            {
                if (_navigateForward == null)
                {
                    _navigateForward = new RoutedUICommand("NavigateForward", "NavigateForward", typeof(ResultCarousel));

                    CommandBinding binding = new CommandBinding(_navigateForward, OnNavigateForwardExecute, NavigateForwardCanExecute);

                    CommandManager.RegisterClassCommandBinding(typeof(ResultCarousel), binding);
                }

                return _navigateForward;
            }
        }

        private ICommand _navigateBackward;

        /// <summary>
        /// Command to navigate the carousel backwards one step
        /// </summary>
        public ICommand NavigateBackward
        {
            get
            {
                if (_navigateBackward == null)
                {
                    _navigateBackward = new RoutedUICommand("NavigateBackward", "NavigateBackward", typeof(ResultCarousel));

                    CommandBinding binding = new CommandBinding(_navigateBackward, OnNavigateBackwardExecute, NavigateBackwardCanExecute);

                    CommandManager.RegisterClassCommandBinding(typeof(ResultCarousel), binding);
                }

                return _navigateBackward;
            }
        }

        public ResultCarousel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the time series to display
        /// </summary>
        /// <param name="dayOffset">The day offset from today</param>
        private void SetTimeSeries(int dayOffset)
        {
            TimeSeries series = null;

            try
            {
                if (Forecast != null)
                {
                    var date = DateTime.Now.AddDays(dayOffset);

                    series = Forecast.TimeSeriesForDate(date);
                }
            }
            catch
            {
                // todo: logging
            }

            CurrentTimeSeries = series;
        }

        /// <summary>
        /// Can execute logic for the NavigateForward command
        /// </summary>        
        private void NavigateForwardCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // check if there is actually a series we can navigate forward to
            var nextSeriesExists = (Forecast != null && Forecast.TimeSeriesForDate(DateTime.Now.AddDays(CurrentDayOffset + 1)) != null);

            e.CanExecute = (CurrentDayOffset <= MaxSteps && nextSeriesExists);
        }

        /// <summary>
        /// Command execution logic for the NavigateForward command
        /// </summary>        
        private void OnNavigateForwardExecute(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentDayOffset++;
        }

        /// <summary>
        /// Can execute logic for the NavigateBackward command
        /// </summary>        
        private void NavigateBackwardCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // check if there is actually a series we can navigate back to
            var previousSeriesExists = (Forecast != null && Forecast.TimeSeriesForDate(DateTime.Now.AddDays(CurrentDayOffset - 1)) != null);

            e.CanExecute = (CurrentDayOffset > 0 && previousSeriesExists);
        }

        /// <summary>
        /// Command execution logic for the NavigateBackward command
        /// </summary>        
        private void OnNavigateBackwardExecute(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentDayOffset--;
        }

        /// <summary>
        /// Handler for the PropertyChanged event of the Forecast property
        /// </summary>
        private static void OnForecastChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ResultCarousel rc = o as ResultCarousel;
            if (rc != null)
            {
                // reset the current offset to 0 (today)
                rc.CurrentDayOffset = 0;
            }
        }
    }
}
