using UnityEngine;

public class RandomAngle : MonoBehaviour, IAngleCalc
{
    private readonly float minAngle = 0f;
    private readonly float maxAngle = 359f;

    public float GetAngle()
    {
        float angle = Random.Range(minAngle, maxAngle);

        return angle;
    }
}
