using UnityEngine;

[RequireComponent(typeof(BotStorage))]
public class BotBuilder : MonoBehaviour
{
    [SerializeField] private int _buildCost;

    private Base _base;
    private ResourceStorage _resourceStorage;
    private BotStorage _botStorage;
    private BotSpawner _botSpawner;

    public void Init(Base @base, ResourceStorage resourceStorage, BotSpawner botSpawner, int botsAmount)
    {
        CommonInit(@base, resourceStorage, botSpawner);
        InitBots(botsAmount);
    }

    public void Init(Base @base, ResourceStorage resourceStorage, BotSpawner botSpawner, Bot bot)
    {
        CommonInit(@base, resourceStorage, botSpawner);
        InitBot(bot);
    }

    public void Build()
    {
        if (_resourceStorage.IsEnough(_buildCost))
        {
            _resourceStorage.SpendResources(_buildCost);
            _botSpawner.InitiateSpawn(this);
        }
    }

    public void InitBot(Bot bot)
    {
        bot.Init(_base);
        _botStorage.AddBot(bot);
    }

    private void CommonInit(Base @base, ResourceStorage resourceStorage, BotSpawner botSpawner)
    {
        _base = @base;
        _resourceStorage = resourceStorage;
        _botSpawner = botSpawner;
        _botStorage = TryGetComponent(out BotStorage botStorage) ? botStorage : null;
    }

    private void InitBots(int amount)
    {
        for (int i = 0; i < amount; i++)
            _botSpawner.InitiateSpawn(this);
    }
}
