using UnityEngine;

public class LinearRotator : MonoBehaviour, IRotator
{
    [SerializeField] private float _speed;

    public void Rotate(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _speed * Time.deltaTime);
    }
}
