using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WeatherView : MonoBehaviour
{
    [SerializeField] private Text weatherTemperatureUnit;
    [SerializeField] private Text weatherTemperature;
    [SerializeField] private Image weatherIcon;

    public void UpdateWeather(string temperatureUnit, string temperature, string iconUrl)
    {
        weatherTemperatureUnit.text = temperatureUnit;
        weatherTemperature.text = temperature;
        
        StartCoroutine(LoadWeatherIcon(iconUrl));
    }

    private IEnumerator LoadWeatherIcon(string url)
    {
        using var www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("url: " + url);
            Debug.LogError($"Failed to load weather icon: {www.error}");
            yield break;
        }

        var texture = DownloadHandlerTexture.GetContent(www);
        weatherIcon.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}