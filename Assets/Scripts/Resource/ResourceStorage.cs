using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    [SerializeField,SerializeInterface(typeof(IResourceCounter))] private List<GameObject> _resourceCounterObjects;
    [SerializeField] private ResourceSpawner _resourceSpawner;

    private List<IResourceCounter> _counters = new();

    private void Awake()
    {
        foreach(GameObject counter in _resourceCounterObjects)
            _counters.Add(counter.GetComponent<IResourceCounter>());
    }

    private void OnEnable()
    {
        _resourceSpawner.Released += Sort;
    }

    private void OnDisable()
    {
        _resourceSpawner.Released -= Sort;
    }

    private void Sort(Resource resource)
    {
        Type resourceType = resource.GetType();

        foreach (IResourceCounter counter in _counters)
        {
            Type genericType = counter.GetType().BaseType;

            if (genericType != null && genericType.IsGenericType)
            {
                Type resourceCounterType = genericType.GetGenericArguments()[0];

                if (resourceType == resourceCounterType)
                    counter.Add();  
            }
        }
    }
}
