using TMPro;
using UnityEngine;

public abstract class ResourceCounterView<T> : MonoBehaviour where T : IResourceCounter
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private T _counter;

    protected readonly string Header = "Количество ";

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
