using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    [SerializeField] private List<Counter> _counters;
    [SerializeField] private ResourceSpawner _resourceSpawner;

    private readonly Dictionary<Type, ICounter> _resourceCounters = new();

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

    private void Sort(Resource resource)
    {
        if (_resourceCounters.TryGetValue(resource.GetType(), out ICounter counter))
            counter.Add(); 
    }

    private void InitCounters()
    {
        _resourceCounters.Add(typeof(Wood), _counters.FirstOrDefault(counter => counter.GetType() == typeof(WoodCounter)));
        _resourceCounters.Add(typeof(Rock), _counters.FirstOrDefault(counter => counter.GetType() == typeof(RockCounter)));
    }
}
