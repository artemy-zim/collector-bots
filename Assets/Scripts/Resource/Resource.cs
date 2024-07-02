using System;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class Resource : MonoBehaviour, ITarget, IResource, IPickable, ISpawnable
{
    [SerializeField, SerializeInterface(typeof(IAngleCalc))] private GameObject _angleCalcObject;
    [SerializeField] private Collider _collider;

    private IAngleCalc _angleCalc;
    private bool _isAssigned = false;

    public event Action<Resource> Collected;

    private void Awake()
    {
        _angleCalc = _angleCalcObject.GetComponent<IAngleCalc>();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool IsAssigned()
    {
        return _isAssigned;
    }

    public void SetAssignedStatus(bool status)
    {
        _isAssigned = status;

        if(_isAssigned == false)
            Collected?.Invoke(this);
    }

    public void OnPick(GrabPoint point)
    {
        transform.position = point.transform.position;
        transform.SetParent(point.transform);
        _collider.enabled = false;
    }

    public void OnSpawn(Vector3 position)
    {
        transform.position = position;
        transform.Rotate(0, _angleCalc.GetAngle(), 0);
        gameObject.SetActive(true);
        _collider.enabled = true;
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
        transform.SetParent(null);
    }
}
