using UnityEngine;

public class LoaderView : MonoBehaviour
{
    [SerializeField] private GameObject globalLoaderUI;
    [SerializeField] private GameObject factLoaderPrefab;

    public void ShowGlobalLoader()
    {
        globalLoaderUI.SetActive(true);
    }

    public void HideGlobalLoader()
    {
        globalLoaderUI.SetActive(false);
    }

    public GameObject ShowFactLoader(Transform parent)
    {
        var loader = Instantiate(factLoaderPrefab, parent);
        loader.transform.localPosition = Vector3.zero;
        return loader;
    }

    public void HideFactLoader(GameObject loader)
    {
        Destroy(loader);
    }
}
