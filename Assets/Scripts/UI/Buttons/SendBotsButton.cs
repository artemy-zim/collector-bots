using UnityEngine;
using UnityEngine.UI;

public class SendBotsButton : MonoBehaviour
{
    [SerializeField] private Button _actionButton;
    [SerializeField] private BaseSelector _selector;

    private ResourceAssigner _currentResourceAssigner;

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
        if (selectable != null)
            _currentResourceAssigner = selectable.TryGetComponent(out ResourceAssigner @base) ? @base : null;
        else
            _currentResourceAssigner = null;
    }

    private void OnClick()
    {
        if (_currentResourceAssigner != null)
            _currentResourceAssigner.SendBots();
    }
}
