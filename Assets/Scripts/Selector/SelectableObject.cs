using UnityEngine;

[RequireComponent (typeof(MeshRenderer))]
public class SelectableObject : MonoBehaviour
{
    [SerializeField] private Material[] _selectionMats;

    private MeshRenderer _meshRenderer;
    private Material[] _defaultMats;

    private void Awake()
    {
        _meshRenderer = TryGetComponent(out MeshRenderer renderer) ? renderer : null;
        _defaultMats = _meshRenderer.materials;
    }

    public void OnSelect()
    {
        _meshRenderer.materials = _selectionMats;
    }


    public void OnDeselect()
    {
        _meshRenderer.materials = _defaultMats;
    }
}
