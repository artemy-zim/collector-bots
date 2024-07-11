public class WoodView : CounterView<WoodCounter>
{
    private readonly string _header = "Количество дерева: ";

    protected override void UpdateView(int amount)
    {
        Text.text = _header + amount.ToString();
    }
}
