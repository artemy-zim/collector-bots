using System;

public interface IBot
{
    public event Action<IBot> ResourceCollected;

    public void AssignResource(IResource resource);
}
