using System.Collections.Generic;

public interface IReadonlyAssignedResourcesStorage
{
    public IReadOnlyCollection<Resource> Get();
}
