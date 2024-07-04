using TMPro;
using UnityEngine;

public abstract class CounterView<T> : MonoBehaviour where T : ICounter
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private T _counter;

    protected readonly string Header = "���������� ";

    protected TextMeshProUGUI Text => _text;

    private void OnEnable()
    {
        _counter.CountChanged += UpdateView;
    }

    private void OnDisable()
    {
        _counter.CountChanged -= UpdateView;
    }

    protected abstract void UpdateView(int amount);
}
