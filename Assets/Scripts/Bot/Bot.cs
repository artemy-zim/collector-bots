using System;
using UnityEngine;

public class Bot : MonoBehaviour, IBot
{
    [SerializeField, SerializeInterface(typeof(ITargetMover))] private GameObject _mover;
    [SerializeField] private Base _base;
    [SerializeField] private TriggerHandler _triggerHandler;
    [SerializeField] private GrabPoint _grabPoint;

    private IResource _assignedResource;
    private ITargetMover _targetMover;
    private bool _isResourcePicked = false;

    public event Action<IBot> ResourceCollected;

    private void Awake()
    {
        _targetMover = _mover.GetComponent<ITargetMover>();
    }

    private void OnEnable()
    {
        _triggerHandler.TriggerEntered += ProcessTrigger;
    }

    private void OnDisable()
    {
        _triggerHandler.TriggerEntered -= ProcessTrigger;
    }

    public void AssignResource(IResource resource)
    {
        if (resource is ITarget target)
        {
            _targetMover.AssignTarget(target);
            _assignedResource = resource;
            _assignedResource.SetAssignedStatus(true);
        }
    }

    private void ProcessTrigger(ITarget target)
    {
        if (target == _base as ITarget)
        {
            if (_isResourcePicked == true)
            {
                _targetMover.Stop();
                _isResourcePicked = false;
                _assignedResource.SetAssignedStatus(false);
                _assignedResource = null;
                ResourceCollected?.Invoke(this);
            }
        }

        if (target is IPickable pickable)
        {
            if (_assignedResource == pickable as IResource)
            {
                pickable.OnPick(_grabPoint);
                _targetMover.AssignTarget(_base);
                _isResourcePicked = true;
            }
        }
    }
}
