using System;
using UnityEngine;

public class BuildSelector : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _maxDistance;

    public event Action<Vector3> PositionSelected;

    public void Select(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _layerMask))
            PositionSelected?.Invoke(hit.point);
    }
}
