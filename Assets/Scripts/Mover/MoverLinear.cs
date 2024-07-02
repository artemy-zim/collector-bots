using UnityEngine;

public class MoverLinear : MonoBehaviour, IMover
{
    [SerializeField] private float _speed;

    public void Move(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
    }
}
