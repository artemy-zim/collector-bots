using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Resource : MonoBehaviour, ITarget, IInteractable, ISpawnable
{
    [SerializeField] private TriggerHandler _triggerHandler;

    public event Action<Resource> Collected;

    private void OnEnable()
    {
        _triggerHandler.TriggerEntered += ProcessTrigger;
    }

    private void OnDisable()
    {
        _triggerHandler.TriggerEntered -= ProcessTrigger;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void ProcessTrigger(IInteractable interactable)
    {
        if (interactable is Base)
            Collected?.Invoke(this);
    }

    public void Reset()
    {
        gameObject.SetActive(false);
        transform.SetParent(null);
    }
}
