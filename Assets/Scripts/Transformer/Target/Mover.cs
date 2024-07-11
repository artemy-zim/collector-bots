using UnityEngine;

public abstract class Mover : MonoBehaviour, IMover
{
    [SerializeField] private float _speed;

    protected float Speed => _speed;

    public abstract void Move(Vector3 position);
}
