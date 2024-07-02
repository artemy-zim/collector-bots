using System;

public interface IResourceCounter
{
    public event Action<int> CountChanged;

    public void Add();
}
