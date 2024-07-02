using System;
using UnityEngine;

public class ResourceCounter<T> : MonoBehaviour, IResourceCounter where T : Resource
{
    private int _count = 0;

    public event Action<int> CountChanged;

    public void Add()
    {
        _count++;
        CountChanged?.Invoke(_count);
    }
}
