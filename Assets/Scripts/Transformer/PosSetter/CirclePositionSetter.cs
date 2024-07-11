using UnityEngine;

[RequireComponent(typeof(ICirclePosCalc))]
public class CirclePositionSetter : MonoBehaviour
{
    private ICirclePosCalc _positionCalc;

    private void Awake()
    {
        _positionCalc = TryGetComponent(out ICirclePosCalc positionCalc) ? positionCalc : null;
    }

    public void Set(Transform transform, Transform targetTransform)
    {
        transform.SetParent(targetTransform);
        Vector3 position = _positionCalc.GetPosition(targetTransform);

        transform.position = position;
    }
}
