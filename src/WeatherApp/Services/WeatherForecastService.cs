namespace WeatherApp.Services
{
  using Model;
  using Newtonsoft.Json;
  using System.Globalization;
  using System.Net.Http;
  using System.Net.Http.Headers;
  
  public class WeatherForecastService : IWeatherForecastService
  {
    private const string _requestUriFormat =
      @"http://opendata-download-metfcst.smhi.se/api/category/pmp1.5g/version/1/geopoint/lat/{0}/lon/{1}/data.json";

    private readonly HttpClient _httpClient;
    private readonly NumberFormatInfo _numberFormatInfo;

    public WeatherForecastService()
    {
      var handler = new HttpClientHandler();
      _httpClient = new HttpClient(handler);
      _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      _numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
    }
    
    public Forecast GetForecastByCoords(double latitude, double longitude)
    {
      var requestUri = string.Format(_numberFormatInfo, _requestUriFormat, latitude.ToString("F6"), longitude.ToString("F6"));
      var json = _httpClient.GetStringAsync(requestUri).Result;
      return JsonConvert.DeserializeObject<Forecast>(json);
    }
  }
}