public class RockView : ResourceCounterView<RockCounter>
{
    private readonly string _amountText = "�����: ";

    protected override void UpdateView(int amount)
    {
       Text.text = Header + _amountText + amount.ToString();
    }
}
