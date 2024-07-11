using UnityEngine;
using UnityEngine.UI;

public class CreateBotButton : MonoBehaviour
{
    [SerializeField] private Button _actionButton;
    [SerializeField] private BaseSelector _selector;

    private BotBuilder _currentBotBuilder;

    private void OnEnable()
    {
        _actionButton.onClick.AddListener(OnClick);
        _selector.SelectedChanged += OnSelectedChanged;
    }

    private void OnDisable()
    {
        _actionButton.onClick.RemoveListener(OnClick);
        _selector.SelectedChanged -= OnSelectedChanged;
    }

    private void OnSelectedChanged(SelectableObject selectable)
    {
        if(selectable != null)
            _currentBotBuilder = selectable.TryGetComponent(out BotBuilder botBuilder) ? botBuilder : null;
        else
            _currentBotBuilder = null;
    }

    private void OnClick()
    {
        if (_currentBotBuilder != null)
            _currentBotBuilder.Build();
    }
}
