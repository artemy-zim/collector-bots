using UnityEngine;

public class BaseInitializer : MonoBehaviour
{
    [SerializeField] private Base _startingBase;
    [SerializeField] private int _startingBotsAmount;

    [SerializeField] private Scanner _scanner;
    [SerializeField] private BotSpawner _botSpawner;
    [SerializeField] private ResourceStorage _resourceStorage;
    [SerializeField] private AssignedResourcesStorage _assignedResourcesStorage;

    private void Awake()
    {
        _startingBase.Init(_scanner, _botSpawner, _resourceStorage, _assignedResourcesStorage, _startingBotsAmount);
    }

    public void Initialize(Base @base, Bot bot)
    {
        @base.Init(_scanner, _botSpawner, _resourceStorage, _assignedResourcesStorage, bot);
    }
}
