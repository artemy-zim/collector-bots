using UnityEngine;

public abstract class Rotator : MonoBehaviour, IRotator
{
    [SerializeField] private float _speed;

    protected float Speed => _speed;

    public abstract void Rotate(Vector3 targetPosition);
}
