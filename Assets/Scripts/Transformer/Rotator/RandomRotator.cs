using UnityEngine;

[RequireComponent (typeof(IAngleCalc))]
public class RandomRotator : MonoBehaviour
{
    private IAngleCalc _angleCalc;

    private void Awake()
    {
        _angleCalc = TryGetComponent(out IAngleCalc angleCalc) ? angleCalc : null;
    }

    public void Set(Transform transform)
    {
        Vector3 eulers = new(0, _angleCalc.GetAngle());

        transform.Rotate(eulers);
    }
}
