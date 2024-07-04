public class RockView : CounterView<RockCounter>
{
    private readonly string _amountText = "камня: ";

    protected override void UpdateView(int amount)
    {
       Text.text = Header + _amountText + amount.ToString();
    }
}
