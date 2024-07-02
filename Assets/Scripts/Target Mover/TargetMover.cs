using System.Collections;
using UnityEngine;

public class TargetMover : MonoBehaviour, ITargetMover
{
    [SerializeField, SerializeInterface(typeof(IRotator))] private GameObject _rotatorObject;
    [SerializeField, SerializeInterface(typeof(IMover))] private GameObject _moverObject;

    private IRotator _rotator;
    private IMover _mover;
    private Coroutine _moveCoroutine;

    private void Awake()
    {
        _rotator = _rotatorObject.GetComponent<IRotator>();
        _mover = _moverObject.GetComponent<IMover>();
    }

    public void AssignTarget(ITarget target)
    {
        Vector3 position = target.GetPosition();

        Stop();
        _moveCoroutine = StartCoroutine(MoveCoroutine(position));
    }

    public void Stop()
    {
        if(_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }
    }

    private IEnumerator MoveCoroutine(Vector3 position)
    {
        while (enabled)
        {
            _mover.Move(position);
            _rotator.Rotate(position);
            yield return null;
        }
    }
}
