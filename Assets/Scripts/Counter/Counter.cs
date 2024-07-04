using System;
using UnityEngine;

public class Counter : MonoBehaviour, ICounter
{
    private int _count;

    public event Action<int> CountChanged;

    public void Add()
    {
        _count++;
        CountChanged?.Invoke(_count);
    }
}
