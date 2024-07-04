using UnityEngine;

public class LinearRotator : Rotator
{
    public override void Rotate(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Speed * Time.deltaTime);
    }
}
