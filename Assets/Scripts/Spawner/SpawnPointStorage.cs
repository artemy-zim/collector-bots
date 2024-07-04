using System.Collections.Generic;
using UnityEngine;

public class SpawnPointStorage : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    private readonly Dictionary<Resource, SpawnPoint> _takenSpawnPoints = new();

    public bool IsNotEmpty => _spawnPoints.Count > 0;

    public SpawnPoint GetAvailable(Resource resource)
    {
        int index = Random.Range(0, _spawnPoints.Count);
        SpawnPoint spawnPoint = _spawnPoints[index];

        _takenSpawnPoints.Add(resource, spawnPoint);
        _spawnPoints.RemoveAt(index);

        return spawnPoint;
    }

    public void ReleaseSpawnPoint(Resource resource)
    {
        if (_takenSpawnPoints.TryGetValue(resource, out SpawnPoint spawnPoint))
        {
            _spawnPoints.Add(spawnPoint);
            _takenSpawnPoints.Remove(resource);
        }
    }
}
