using UnityEngine;

public class LinearMover : Mover
{
    public override void Move(Vector3 targetPosition)
    {
        Vector3 newPosition = new(targetPosition.x, transform.position.y, targetPosition.z);

        transform.position = Vector3.MoveTowards(transform.position, newPosition, Speed * Time.deltaTime);
    }
}
