using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    [SerializeField] private List<ResourceCounter> _counters;
    [SerializeField] private ResourceSpawner _resourceSpawner;

    private readonly Dictionary<Type, ResourceCounter> _resourceCounters = new();

    private void Awake()
    {
        InitCounters();
    }

    private void OnEnable()
    {
        _resourceSpawner.Released += Sort;
    }

    private void OnDisable()
    {
        _resourceSpawner.Released -= Sort;
    }

    public bool IsEnough(int count)
    {
        return _resourceCounters.Values.All(counter => counter.IsEnough(count));
    }

    public void SpendResources(int count)
    {
        foreach(ResourceCounter counter in _resourceCounters.Values)
            counter.Decrease(count);
    }

    private void Sort(Resource resource)
    {
        if (_resourceCounters.TryGetValue(resource.GetType(), out ResourceCounter counter))
            counter.Add(); 
    }

    private void InitCounters()
    {
        _resourceCounters.Add(typeof(Wood), _counters.FirstOrDefault(counter => counter.GetType() == typeof(WoodCounter)));
        _resourceCounters.Add(typeof(Rock), _counters.FirstOrDefault(counter => counter.GetType() == typeof(RockCounter)));
    }
}
