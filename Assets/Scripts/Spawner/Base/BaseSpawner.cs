using UnityEngine;

public class BaseSpawner : Spawner<Base>
{
    [SerializeField] private FlagSpawner _flagSpawner;
    [SerializeField] private BaseInitializer _initializer;

    private Vector3 _currentPos;
    private Bot _currentBot;

    private void OnEnable()
    {
        _flagSpawner.BuildStarted += OnBuildStarted;
    }

    private void OnDisable()
    {
        _flagSpawner.BuildStarted -= OnBuildStarted;
    }

    protected override void Spawn(Base spawnable)
    {
        spawnable.transform.position = _currentPos;
        _initializer.Initialize(spawnable, _currentBot);
    }

    private void OnBuildStarted(Vector3 position, Bot bot)
    {
        _currentPos = position;
        _currentBot = bot;
        GetSpawnable();
    }
}
