using UnityEngine;

public class BotSpawner : Spawner<Bot>
{
    [SerializeField] private BotCounter _botCounter;
    [SerializeField] private RandomRotator _rotator;
    [SerializeField] private CirclePositionSetter _positionSetter;

    private BotBuilder _currentBotBuilder;

    public void InitiateSpawn(BotBuilder botBuilder)
    {
        _currentBotBuilder = botBuilder;
        GetSpawnable();
    }

    protected override void Spawn(Bot spawnable)
    {
        _positionSetter.Set(spawnable.transform, _currentBotBuilder.transform);
        _rotator.Set(spawnable.transform);
        _botCounter.Add();
        _currentBotBuilder.InitBot(spawnable);
    }
}
