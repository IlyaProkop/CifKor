using Cysharp.Threading.Tasks;

public interface IWeatherService
{
    UniTask<WeatherResponse> GetWeatherAsync();
}