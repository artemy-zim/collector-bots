using UnityEngine;

public abstract class TargetRotator : MonoBehaviour, ITargetRotator
{
    [SerializeField] private float _speed;

    protected float Speed => _speed;

    public abstract void Rotate(Vector3 targetPosition);
}
