using System;
using UnityEngine;

public class Bot : MonoBehaviour, ISpawnable
{
    [SerializeField] private TargetMover _mover;
    [SerializeField] private TriggerHandler _triggerHandler;
    [SerializeField] private GrabPoint _grabPoint;

    private Base _base;
    private ITarget _target;

    public event Action<Bot> TaskCompleted;

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

    public void AssignTarget(ITarget target)
    {
        _mover.AssignPosition(target);
        _target = target;
    }

    private void ProcessTrigger(IInteractable interactable)
    {
        if (interactable == _target as IInteractable)
        {
            if (_target is Resource resource)
            {
                _grabPoint.Fill(resource.transform);
                resource.Collected += OnCollected;
                AssignTarget(_base);
            }
            else if (_grabPoint.IsEmpty &&_target is Base)
            {
                _mover.Stop();
                TaskCompleted?.Invoke(this);
            }
            else if (_target is Flag)
            {
                _mover.Stop();
                TaskCompleted?.Invoke(this);
            }
        }
    }

    private void OnCollected(Resource resource)
    {
        resource.Collected -= OnCollected;
        _grabPoint.Release();
    }

    public void Reset()
    {
        gameObject.SetActive(false);
    }
}
