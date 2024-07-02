using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float x;
    [SerializeField] private float z;

    private readonly float y = 1f;

    public List<IResource> GetResources(int amount)
    {
        Collider[] colliders = GetColliders(amount);
        List<IResource> resources = new();

        foreach(Collider collider in colliders)
        {
            if(collider.gameObject.activeSelf && collider.TryGetComponent(out IResource resource))
            {
                if(resource.IsAssigned() == false)
                    resources.Add(resource);
            }
        }

        return resources;
    }

    private Collider[] GetColliders(int amount)
    {
        Collider[] colliders = new Collider[amount];
        Vector3 halfExtents = new(x, y, z);
        int collidersCount = Physics.OverlapBoxNonAlloc(transform.position, halfExtents, colliders, Quaternion.identity);

        CleanColliders(colliders, collidersCount);

        return colliders;
    }

    private void CleanColliders(Collider[] colliders, int index)
    {
        for (int i = index; i < colliders.Length; i++)
            colliders[i] = null;
    }
}
