using System;

public interface ICounter
{
    public event Action<int> CountChanged;

    public void Add();
}
