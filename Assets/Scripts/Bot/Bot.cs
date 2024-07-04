using System;
using UnityEngine;

public class Bot : MonoBehaviour, IBot
{
    [SerializeField] private TargetMover _mover;
    [SerializeField] private TriggerHandler _triggerHandler;
    [SerializeField] private GrabPoint _grabPoint;

    private Base _base;
    private Resource _assignedResource;

    public event Action<IBot, Resource> ResourceCollected;

    private void OnEnable()
    {
        _triggerHandler.TriggerEntered += ProcessTrigger;
    }

    private void OnDisable()
    {
        _triggerHandler.TriggerEntered -= ProcessTrigger;
    }

    public void Init(Base @base)
    {
        _base = @base;
    }

    public void AssignResource(Resource resource)
    {
        _mover.AssignTarget(resource);
        _assignedResource = resource;
        resource.Collected += OnCollected;
    }

    private void OnCollected(Resource resource)
    {
        resource.Collected -= OnCollected;
        _assignedResource = null;
        _mover.Stop();
        ResourceCollected?.Invoke(this, resource);
    }

    private void ProcessTrigger(IInteractable interactable)
    {
        if (interactable == _assignedResource as IInteractable)
        {
            _assignedResource.transform.position = _grabPoint.transform.position;
            _assignedResource.transform.SetParent(transform);
            _mover.AssignTarget(_base);
        }
    }
}
