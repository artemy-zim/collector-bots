using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private BaseSelector _selector;

    private PlayerInput _input;
    private bool _isPointerOverUI;

    private void Awake()
    {
        _input = new PlayerInput();

        _input.Player.Select.performed += OnSelect;
        _input.Player.Deselect.performed += OnDeselect;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void Update()
    {
        _isPointerOverUI = EventSystem.current.IsPointerOverGameObject();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void OnSelect(InputAction.CallbackContext context)
    {
        if (_isPointerOverUI)
            return;

        _selector.Select();
    }

    private void OnDeselect(InputAction.CallbackContext context)
    {
        if (_isPointerOverUI)
            return;

        _selector.Deselect();
    }
}
