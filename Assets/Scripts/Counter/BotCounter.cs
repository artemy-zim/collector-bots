
public class BotCounter : Counter
{
    public new int Count => base.Count;

    public void Init(int count)
    {
        base.Count = count;
    }

    public override void Add()
    {
        base.Count++;
    }
}
