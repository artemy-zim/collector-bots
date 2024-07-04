using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float x;
    [SerializeField] private float z;

    private readonly float y = 1f;

    public List<Resource> GetResources(List<Resource> assignedResources, int amount)
    {
        Collider[] colliders = GetColliders(amount);
        List<Resource> resources = new();

        foreach (Collider collider in colliders)
        {
            if (collider != null && collider.gameObject.activeSelf && collider.TryGetComponent(out Resource resource))
            {
                if(assignedResources.Contains(resource) == false)
                    resources.Add(resource);
            }
        }

        return resources;
    }

    private Collider[] GetColliders(int amount)
    {
        Collider[] colliders = new Collider[amount];
        Vector3 halfExtents = new(x, y, z);
        int collidersCount = Physics.OverlapBoxNonAlloc(transform.position, halfExtents, colliders, Quaternion.identity, _layerMask);

        CleanColliders(colliders, collidersCount);

        return colliders;
    }

    private void CleanColliders(Collider[] colliders, int index)
    {
        for (int i = index; i < colliders.Length; i++)
            colliders[i] = null;
    }
}
