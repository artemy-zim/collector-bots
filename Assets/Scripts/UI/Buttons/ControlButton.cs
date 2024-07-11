using UnityEngine;
using UnityEngine.UI;

public abstract class ControlButton : MonoBehaviour
{
    [SerializeField] private Button _actionButton;

    private void OnEnable()
    {
        _actionButton.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _actionButton.onClick.RemoveListener(OnClick);
    }

    protected abstract void OnClick();
}
