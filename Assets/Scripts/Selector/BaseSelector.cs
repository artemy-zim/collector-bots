using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseSelector : MonoBehaviour
{
    [SerializeField] private BuildSelector _spawnSelector;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField, Min(0)] private float _maxSelectDistance;

    private Camera _mainCamera;
    private SelectableObject _currentSelectable;

    public event Action<SelectableObject> SelectedChanged;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void Select()
    {
        Vector2 mousePos = Mouse.current.position.value;
        Ray ray = _mainCamera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxSelectDistance, _layerMask))
            ProcessHit(hit);
        else if(_currentSelectable != null) 
            _spawnSelector.Select(ray);
    }

    public void Deselect()
    {
        if (_currentSelectable != null)
        {
            _currentSelectable.OnDeselect();
            _currentSelectable = null;
            SelectedChanged?.Invoke(_currentSelectable);
        }
    }

    private void ProcessHit(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out SelectableObject selectable))
        {
            if (_currentSelectable == selectable)
                return;

            Deselect();
            SelectNew(selectable);

            SelectedChanged?.Invoke(_currentSelectable);
        }
    }

    private void SelectNew(SelectableObject selectableObject)
    {
        _currentSelectable = selectableObject;
        _currentSelectable.OnSelect();
    }
}
