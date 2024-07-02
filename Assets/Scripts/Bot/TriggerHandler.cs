using System;
using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class TriggerHandler : MonoBehaviour
{
    public event Action<ITarget> TriggerEntered;

    private void OnValidate()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ITarget target))
            TriggerEntered?.Invoke(target);
    }
}
