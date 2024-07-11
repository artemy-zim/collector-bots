using UnityEngine;

public interface IAssignedResourcesStorage
{
    public void Add(Resource resource);

    public void Remove(Resource resource);
}
