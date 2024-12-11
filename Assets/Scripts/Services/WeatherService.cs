using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class WeatherService : IWeatherService
{
    private readonly RequestManager _requestManager;
    private readonly LoaderController _loaderController;

    public WeatherService(RequestManager requestManager, LoaderController loaderController)
    {
        _requestManager = requestManager;
        _loaderController = loaderController;
    }

    public async UniTask<WeatherResponse> GetWeatherAsync()
    {
        return await _requestManager.ExecuteRequest(async token =>
        {
            var url = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";
            using var request = UnityWebRequest.Get(url);

            var response = await request.SendWebRequest().WithCancellation(token);

            if (response.result != UnityWebRequest.Result.Success)
            {
                // Debug.LogError($"Failed to fetch weather: {response.error}");
                return null;
            }

            try
            {

                var json = response.downloadHandler.text;
                return JsonConvert.DeserializeObject<WeatherResponse>(json);
            }
            catch (JsonException ex)
            {
                //  Debug.LogError($"JSON parsing error: {ex.Message}");
                return null;
            }
        },
        _loaderController.ShowGlobalLoader,
        _loaderController.HideGlobalLoader
        );
    }
}
