using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System.Linq;

public class FactsService : IFactsService
{
    private readonly RequestManager _requestManager;

    public FactsService(RequestManager requestManager)
    {
        _requestManager = requestManager;
    }

    public async UniTask<List<BreedData>> GetFactsAsync()
    {
        Debug.Log("GetFactsAsync");
        return await _requestManager.ExecuteRequest(async token =>
        {
            var url = "https://dogapi.dog/api/v2/breeds";
            using var request = UnityWebRequest.Get(url);

            var response = await request.SendWebRequest().WithCancellation(token);

            if (response.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to fetch facts: {response.error}");
                return new List<BreedData>();
            }

            var json = response.downloadHandler.text;
            var breedResponse = JsonConvert.DeserializeObject<BreedsResponse>(json);
            return breedResponse?.Data ?? new List<BreedData>();
        });
    }

    public async UniTask<BreedData> GetFactDetailsAsync(string factId)
    {
        Debug.Log(" GetFactDetailsAsync");
        return await _requestManager.ExecuteRequest(async token =>
        {            
            var url = $"https://dogapi.dog/api/v2/breeds/{factId}";
            using var request = UnityWebRequest.Get(url);

            var response = await request.SendWebRequest().WithCancellation(token);

            if (response.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to fetch fact details: {response.error}");
                return null;
            }

            var json = response.downloadHandler.text;
            var breedResponse = JsonConvert.DeserializeObject<OneBreedResponse>(json);
            return breedResponse?.Data;
        });
    }
}
