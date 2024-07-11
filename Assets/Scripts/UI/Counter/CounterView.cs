using TMPro;
using UnityEngine;

public abstract class CounterView<T> : MonoBehaviour where T : ResourceCounter
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private T _counter;

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
