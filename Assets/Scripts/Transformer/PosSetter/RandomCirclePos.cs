using UnityEngine;

public class RandomCirclePos : MonoBehaviour, ICirclePosCalc
{
    [SerializeField] private float _height;
    [SerializeField] private float _radius;
    [SerializeField] private int _positionsAmount;

    public Vector3 GetPosition(Transform targetTransform)
    {
        Vector3[] positions = CalculatePositions(targetTransform);

        return positions[Random.Range(0, _positionsAmount)];
    }

    private float CalculateAngleStep()
    {
        int maxAngleDeg = 360;

        return maxAngleDeg / _positionsAmount * Mathf.Deg2Rad;
    }

    private Vector3[] CalculatePositions(Transform targetTransform)
    {
        Vector3[] positions = new Vector3[_positionsAmount];
        Vector3 center = targetTransform.position;
        float angleStep = CalculateAngleStep();

        for (int i = 0; i < _positionsAmount; i++)
        {
            float angle = i * angleStep;
            Vector2 currentPos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _radius;

            positions[i] = new Vector3(center.x + currentPos.x, center.y + _height, center.z + currentPos.y);
        }

        return positions;
    }
}
