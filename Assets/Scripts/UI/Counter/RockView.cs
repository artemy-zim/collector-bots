public class RockView : CounterView<RockCounter>
{
    private readonly string _header = "Количество камня: ";

    protected override void UpdateView(int amount)
    {
       Text.text = _header + amount.ToString();
    }
}
