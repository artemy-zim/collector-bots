using System;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class TriggerHandler : MonoBehaviour
{
    public event Action<IInteractable> TriggerEntered;

    private void OnValidate()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out IInteractable target))
            TriggerEntered?.Invoke(target);
    }
}
