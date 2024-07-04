using UnityEngine;

public class TransformSetter : MonoBehaviour
{
    [SerializeField] private AngleCalc _angleCalcObject;

    private IAngleCalc _angleCalc;

    private void Awake()
    {
        _angleCalc = _angleCalcObject.TryGetComponent(out IAngleCalc angleCalc) ? angleCalc : null;
    }

    public void Set(Transform transform, Vector3 position)
    {
        Vector3 eulers = new(0, _angleCalc.GetAngle());

        transform.position = position;
        transform.Rotate(eulers);
    }
}
