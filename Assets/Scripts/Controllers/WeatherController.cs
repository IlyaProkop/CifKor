public class WeatherController
{
    private readonly IWeatherService _weatherService;
    private readonly WeatherView _weatherView;
    private readonly WeatherModel _weatherModel;

    public WeatherController(IWeatherService weatherService, WeatherView weatherView, WeatherModel weatherModel)
    {
        _weatherService = weatherService;
        _weatherView = weatherView;
        _weatherModel = weatherModel;
    }

    public async void FetchWeather()
    {
        var weatherResponse = await _weatherService.GetWeatherAsync();

        if (weatherResponse?.Properties?.Periods == null || weatherResponse.Properties.Periods.Count == 0)
        {
            // _weatherView.ShowError("Weather data is not available.");
            return;
        }

        
        var todayWeather = weatherResponse.Properties.Periods[0];

        SetModel(todayWeather);
        SetView();

    }
    private void SetModel(WeatherPeriod todayWeather)
    {
        _weatherModel.Icon = todayWeather.Icon;
        _weatherModel.Temperature = todayWeather.Temperature.ToString();
        _weatherModel.TemperatureUnit = todayWeather.TemperatureUnit;
    }
    private void SetView()
    {
        _weatherView.UpdateWeather(_weatherModel.TemperatureUnit, _weatherModel.Temperature, _weatherModel.Icon);
    }

}