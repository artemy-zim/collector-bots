using System.Collections.Generic;
using UnityEngine;

public class AssignedResourcesStorage : MonoBehaviour, IReadonlyAssignedResourcesStorage, IAssignedResourcesStorage
{
    private readonly List<Resource> resources = new();

    public void Add(Resource resource)
    {
        resources.Add(resource);
    }

    public IReadOnlyCollection<Resource> Get()
    {
        return resources;
    }

    public void Remove(Resource resource)
    {
        resources.Remove(resource);
    }
}

