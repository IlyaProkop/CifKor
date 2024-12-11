using System.Collections.Generic;

public class LoaderModel
{
    public bool IsGlobalLoaderActive { get; private set; }
    private readonly HashSet<string> _activeFactLoaders = new();

    public void SetGlobalLoader(bool isActive)
    {
        IsGlobalLoaderActive = isActive;
    }

    public void AddFactLoader(string factId)
    {
        _activeFactLoaders.Add(factId);
    }

    public void RemoveFactLoader(string factId)
    {
        _activeFactLoaders.Remove(factId);
    }

    public bool IsFactLoaderActive(string factId)
    {
        return _activeFactLoaders.Contains(factId);
    }
}
