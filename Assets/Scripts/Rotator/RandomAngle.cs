using UnityEngine;

public class RandomAngle : AngleCalc
{
    private readonly float minAngle = 0f;
    private readonly float maxAngle = 359f;

    public override float GetAngle()
    {
        float angle = Random.Range(minAngle, maxAngle);

        return angle;
    }
}
