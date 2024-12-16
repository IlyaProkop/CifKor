using Cysharp.Threading.Tasks;

public class WeatherService : IWeatherService
{
    private readonly RequestManager _requestManager;
    private readonly LoaderController _loaderController;
    private readonly WebRequestHandler _webRequestHandler;

    public WeatherService(RequestManager requestManager, LoaderController loaderController, WebRequestHandler webRequestHandler)
    {
        _requestManager = requestManager;
        _loaderController = loaderController;
        _webRequestHandler = webRequestHandler;
    }

    public async UniTask<WeatherResponse> GetWeatherAsync()
    {
        return await _requestManager.ExecuteRequest(async token =>
        {
            var url = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";
            return await _webRequestHandler.GetAsync<WeatherResponse>(url, token);
        },
    _loaderController.ShowGlobalLoader,
    _loaderController.HideGlobalLoader);
    }
}
