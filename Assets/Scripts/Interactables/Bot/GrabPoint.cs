using UnityEngine;

public class GrabPoint : MonoBehaviour 
{
    private Transform _currentTransform;

    public bool IsEmpty => _currentTransform == null;

    public void Fill(Transform transform)
    {
        _currentTransform = transform;
        _currentTransform.position = this.transform.position;
        _currentTransform.SetParent(this.transform);
    }

    public void Release()
    {
        _currentTransform.SetParent(null);
        _currentTransform = null;
    }
}
