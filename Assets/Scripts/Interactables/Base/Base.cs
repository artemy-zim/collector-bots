using UnityEngine;

public class Base : MonoBehaviour, ITarget, ISpawnable, IInteractable
{
    [SerializeField] private ResourceAssigner _resourceAssigner;
    [SerializeField] private BuildAssigner _buildAssigner;
    [SerializeField] private BotBuilder _botBuilder;

    public void Init(Scanner scanner, BotSpawner botSpawner, ResourceStorage resourceStorage, IAssignedResourcesStorage assignedResourcesStorage, Bot bot)
    {
        CommonInit(scanner, resourceStorage, assignedResourcesStorage);
        _botBuilder.Init(this, resourceStorage, botSpawner, bot);
    }

    public void Init(Scanner scanner, BotSpawner botSpawner, ResourceStorage resourceStorage, IAssignedResourcesStorage assignedResourcesStorage, int botsAmount)
    {
        CommonInit(scanner, resourceStorage, assignedResourcesStorage);
        _botBuilder.Init(this, resourceStorage, botSpawner, botsAmount);
    }

    private void CommonInit(Scanner scanner, ResourceStorage resourceStorage, IAssignedResourcesStorage assignedResourcesStorage)
    {
        _resourceAssigner.Init(scanner, assignedResourcesStorage);
        _buildAssigner.Init(resourceStorage);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Reset()
    {
        gameObject.SetActive(false);    
    }
}
