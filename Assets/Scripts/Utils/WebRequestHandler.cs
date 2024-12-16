using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestHandler
{
    public async UniTask<T> GetAsync<T>(string url, CancellationToken token)
    {
        using var request = UnityWebRequest.Get(url);

        var response = await request.SendWebRequest().WithCancellation(token);

        if (response.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to fetch data from {url}: {response.error}");
            return default;
        }

        try
        {
            var json = response.downloadHandler.text;
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to deserialize JSON from {url}: {ex.Message}");
            return default;
        }
    }
}
