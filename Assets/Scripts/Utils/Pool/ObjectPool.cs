using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class ObjectPool<T> where T : Component
{
    private readonly Queue<T> _pool = new Queue<T>();
    private readonly DiContainer _container;
    private readonly T _prefab;

    public Transform Parent { get; } 

    public ObjectPool(DiContainer container, T prefab, Transform parent = null)
    {
        _container = container;
        _prefab = prefab;
        Parent = parent;
    }
    
    public T Get()
    {
        if (_pool.Count > 0)
        {
            var obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        return CreateNewObject();
    }
    
    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
   
    private T CreateNewObject()
    {      
        var obj = _container.InstantiatePrefabForComponent<T>(_prefab, Parent);        
        obj.gameObject.SetActive(true);
        return obj;
    }
}
