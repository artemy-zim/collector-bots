using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField, SerializeInterface(typeof(IReadonlyAssignedResourcesStorage))] private AssignedResourcesStorage _assignedResourcesObject;
    [SerializeField] private BotCounter _botCounter;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float x;
    [SerializeField] private float z;

    private IReadonlyAssignedResourcesStorage _assignedResourcesStorage;
    private readonly float y = 1f;

    private void Awake()
    {
        _assignedResourcesStorage = _assignedResourcesObject;
    }

    public List<Resource> GetResources()
    {
        Collider[] colliders = GetColliders();
        List<Resource> resources = GetActiveResources(colliders);
        IReadOnlyCollection<Resource> assignedResources = _assignedResourcesStorage.Get();

        return resources.Where(resource => assignedResources.Contains(resource) == false).ToList();
    }

    private List<Resource> GetActiveResources(Collider[] colliders)
    {
        List<Resource> resources = new();

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.activeSelf && collider.TryGetComponent(out Resource resource))
                resources.Add(resource);
        }

        return resources;
    }

    private Collider[] GetColliders()
    {
        Collider[] colliders = new Collider[_botCounter.Count];
        Vector3 halfExtents = new(x, y, z);
        int collidersCount = Physics.OverlapBoxNonAlloc(transform.position, halfExtents, colliders, Quaternion.identity, _layerMask);

        return GetCleanColliders(colliders, collidersCount);
    }

    private Collider[] GetCleanColliders(Collider[] colliders, int collidersCount)
    {
        if (colliders.Length > collidersCount)
        {
            Collider[] cleanColliders = new Collider[collidersCount];
            Array.Copy(colliders, cleanColliders, collidersCount);

            return cleanColliders;
        }

        return colliders;
    }
}
