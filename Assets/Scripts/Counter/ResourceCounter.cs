using System;

public class ResourceCounter : Counter
{
    public event Action<int> CountChanged;

    public override void Add()
    {
        Count++;
        CountChanged?.Invoke(Count);
    }

    public void Decrease(int count)
    {
        Count -= count;
        CountChanged?.Invoke(Count);
    }

    public bool IsEnough(int count)
    {
        return Count >= count;
    }
}
