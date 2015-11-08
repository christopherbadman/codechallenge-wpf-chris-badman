namespace WeatherApp.ViewModels
{
  using Microsoft.Maps.MapControl.WPF;

  public class MapViewModel : ViewModel
  {
    private Location _center;
    private double _zoomLevel;

    public MapViewModel(double latitude, double longitude, double zoomLevel)
    {
      _center = new Location(latitude, longitude);
      _zoomLevel = zoomLevel;
    }

    public Location Center
    {
      get { return _center; }

      set
      {
        _center = value;
        NotifyPropertyChanged();
      }
    }

    public double ZoomLevel
    {
      get { return _zoomLevel; }

      set
      {
        _zoomLevel = value;
        NotifyPropertyChanged();
      }
    }
  }
}