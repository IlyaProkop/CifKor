using UnityEngine;
using System.Collections.Generic;

public class LoaderController
{
    private readonly LoaderModel _loaderModel;
    private readonly LoaderView _loaderView;
    private readonly Dictionary<string, GameObject> _factLoaders = new();

    public LoaderController(LoaderModel loaderModel, LoaderView loaderView)
    {
        _loaderModel = loaderModel;
        _loaderView = loaderView;
    }
   
    public void ShowGlobalLoader()
    {
        _loaderModel.SetGlobalLoader(true);
        _loaderView.ShowGlobalLoader();
    }

    public void HideGlobalLoader()
    {
        _loaderModel.SetGlobalLoader(false);
        _loaderView.HideGlobalLoader();
    }
    
    public void ShowFactLoader(string factId, Transform parent)
    {
        if (_loaderModel.IsFactLoaderActive(factId)) return;

        _loaderModel.AddFactLoader(factId);
        var loader = _loaderView.ShowFactLoader(parent);
        _factLoaders[factId] = loader;
    }

    public void HideFactLoader(string factId)
    {
        if (!_loaderModel.IsFactLoaderActive(factId)) return;

        _loaderModel.RemoveFactLoader(factId);
        if (_factLoaders.TryGetValue(factId, out var loader))
        {
            _loaderView.HideFactLoader(loader);
            _factLoaders.Remove(factId);
        }
    }
}
