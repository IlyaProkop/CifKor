using UnityEngine;
using Zenject;

public class FactViewPool : ObjectPool<FactView>
{
    public FactViewPool(FactView prefab, Transform parent, DiContainer container)
        : base(container, prefab, parent)
    {        
    }
}
