using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FactsContainerModel : MonoBehaviour
{
    private const int MAX_FACKS = 10;

    [SerializeField] private FactView factPrefab; 
    [SerializeField] private Transform factsContainer;

    public FactView FactPrefab => factPrefab;
    public Transform FactsContainer => factsContainer;

    private FactViewPool _factViewPool;

    [Inject]
    public void Construct(FactViewPool factViewPool)
    {
        _factViewPool = factViewPool;
    }

    public void UpdateFacts(List<FactModel> facts)
    {
        Clear();
        
        int count = Mathf.Min(facts.Count, MAX_FACKS);

        for (int i = 0; i < count; i++)
        {
            var factView = _factViewPool.Get();
            factView.transform.SetParent(_factViewPool.Parent);
            factView.transform.localScale = Vector3.one;
            factView.UpdateView(i + 1, facts[i].Name, facts[i].Id);
        }
    }
    private void Clear()
    {
        foreach (Transform child in _factViewPool.Parent)
        {
            var factView = child.GetComponent<FactView>();
            if (factView != null)
            {
                _factViewPool.Return(factView);
            }
            else
            {
                Destroy(child.gameObject);
            }
        }
    }
}
