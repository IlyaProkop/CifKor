using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class FactsService : IFactsService
{
    private readonly RequestManager _requestManager;
    private readonly LoaderController _loaderController;
    private readonly WebRequestHandler _webRequestHandler;

    public FactsService(RequestManager requestManager, LoaderController loaderController, WebRequestHandler webRequestHandler)
    {
        _requestManager = requestManager;
        _loaderController = loaderController;
        _webRequestHandler = webRequestHandler;
    }

    public async UniTask<List<BreedData>> GetFactsAsync()
    {
        Debug.Log("GetFactsAsync");
        return await _requestManager.ExecuteRequest(async token =>
        {
            var url = "https://dogapi.dog/api/v2/breeds";
            var breedResponse = await _webRequestHandler.GetAsync<BreedsResponse>(url, token);
            return breedResponse?.Data ?? new List<BreedData>();
        },
        _loaderController.ShowGlobalLoader,
        _loaderController.HideGlobalLoader);
    }

    public async UniTask<BreedData> GetFactDetailsAsync(string factId)
    {
        Debug.Log("GetFactDetailsAsync");
        return await _requestManager.ExecuteRequest(async token =>
        {
            var url = $"https://dogapi.dog/api/v2/breeds/{factId}";
            var breedResponse = await _webRequestHandler.GetAsync<OneBreedResponse>(url, token);
            return breedResponse?.Data;
        });
    }
}
