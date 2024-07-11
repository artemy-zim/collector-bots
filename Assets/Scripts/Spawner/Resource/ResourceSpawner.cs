using System;
using System.Collections;
using UnityEngine;

public class ResourceSpawner : Spawner<Resource>
{
    [SerializeField] private SpawnPointStorage _spawnPointStorage;
    [SerializeField] private RandomRotator _rotator;
    [SerializeField] private float _delay;

    private Coroutine _spawnCoroutine;

    public event Action<Resource> Released;

    private void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    protected override void Spawn(Resource resource)
    {
        SpawnPoint spawnPoint = _spawnPointStorage.GetAvailable(resource);

        _rotator.Set(resource.transform);
        resource.transform.position = spawnPoint.transform.position;
        resource.gameObject.SetActive(true);
        resource.Collected += ReleaseSpawnable;
    }

    protected override void ReleaseSpawnable(Resource resource)
    {
        resource.Collected -= ReleaseSpawnable;
        _spawnPointStorage.ReleaseSpawnPoint(resource);

        _spawnCoroutine ??= StartCoroutine(SpawnCoroutine());

        Released?.Invoke(resource);
        base.ReleaseSpawnable(resource);
    }

    private IEnumerator SpawnCoroutine()
    {
        WaitForSeconds delay = new(_delay);

        while (enabled && _spawnPointStorage.IsNotEmpty)
        {
            GetSpawnable();
            yield return delay;
        }

        _spawnCoroutine = null;
    }
}
