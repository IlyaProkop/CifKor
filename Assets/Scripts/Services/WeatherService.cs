using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class WeatherService : IWeatherService
{
    private readonly RequestManager _requestManager;

    public WeatherService(RequestManager requestManager)
    {
        _requestManager = requestManager;
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
        });
    }
}
