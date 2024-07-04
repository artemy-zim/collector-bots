public class WoodView : CounterView<WoodCounter>
{
    private readonly string _amountText = "дерева: ";

    protected override void UpdateView(int amount)
    {
        Text.text = Header + _amountText + amount.ToString();
    }
}
