using System;
using System.Collections.Generic;
using UnityEngine;

public class FlagSpawner : Spawner<Flag>
{
    [SerializeField] private BaseSelector _baseSelector;
    [SerializeField] private BuildSelector _buildSelector;

    private readonly Dictionary<BuildAssigner, Flag> _activeFlags = new();
    private BuildAssigner _currentBuildAssigner;
    private Vector3 _currentPos;

    public event Action<Vector3, Bot> BuildStarted;

    private void OnEnable()
    {
        _baseSelector.SelectedChanged += OnSelectedChanged;
        _buildSelector.PositionSelected += InitiateSpawn;
    }

    private void OnDisable()
    {
        _baseSelector.SelectedChanged -= OnSelectedChanged;
        _buildSelector.PositionSelected -= InitiateSpawn;
    }

    protected override void Spawn(Flag spawnable)
    {
        spawnable.gameObject.SetActive(true);
        UpdatePosition(_currentPos, spawnable);

        _activeFlags.Add(_currentBuildAssigner, spawnable);
        _currentBuildAssigner.DestinationReached += OnDestinationReached;
    }

    private void OnDestinationReached(BuildAssigner buildAssigner, Bot bot)
    {
        if(_activeFlags.Remove(buildAssigner, out Flag flag))
        {
            Vector3 buildPos = flag.transform.position;

            ReleaseSpawnable(flag);
            BuildStarted?.Invoke(buildPos, bot);
        }

        buildAssigner.DestinationReached -= OnDestinationReached;
    }

    private void OnSelectedChanged(SelectableObject selectable)
    {
        if (selectable != null)
            _currentBuildAssigner = selectable.TryGetComponent(out BuildAssigner buildAssigner) ? buildAssigner : null;
        else
            _currentBuildAssigner = null;
    }

    private void InitiateSpawn(Vector3 position)
    {
        if (_currentBuildAssigner.IsBusy || _currentBuildAssigner == null)
            return;

        if (_activeFlags.TryGetValue(_currentBuildAssigner, out Flag flag))
            UpdatePosition(position, flag);
        else
            SetPosition(position);
    }

    private void SetPosition(Vector3 position)
    {
        _currentPos = position;
        GetSpawnable();
    }

    private void UpdatePosition(Vector3 position, Flag flag)
    {
        flag.transform.position = position;
        _currentBuildAssigner.AssignBuild(flag);
    }
}
