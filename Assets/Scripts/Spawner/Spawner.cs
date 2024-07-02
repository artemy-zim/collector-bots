using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable
{
    [SerializeField] private T[] _spawnables;
    [SerializeField, Min(0)] private int _poolSize; 
    [SerializeField, Min(0)] private int _poolMaxSize;
    
    private ObjectPool<T> _pool;

    private void OnValidate()
    {
        if(_poolSize > _poolMaxSize)
            _poolSize = _poolMaxSize;
    }

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(_spawnables[Random.Range(0, _spawnables.Length)]),
            actionOnGet: (spawnable) => Spawn(spawnable),
            actionOnRelease: (spawnable) => spawnable.OnDespawn(),
            actionOnDestroy: (spawnable) => Destroy(spawnable),
            collectionCheck: true,
            defaultCapacity: _poolSize,
            maxSize: _poolMaxSize
            );
    }

    protected void GetSpawnable()
    {
        _pool.Get();
    }

    protected virtual void ReleaseSpawnable(T spawnable)
    {
        _pool.Release(spawnable);
    }

    protected abstract void Spawn(T spawnable);
}
