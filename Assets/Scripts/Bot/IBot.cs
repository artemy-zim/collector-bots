using System;

public interface IBot
{
    public event Action<IBot, Resource> ResourceCollected;


    public void Init(Base @base);
    public void AssignResource(Resource resource);
}
