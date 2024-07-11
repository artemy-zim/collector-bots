using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BotStorage))]
public class ResourceAssigner : MonoBehaviour
{
    private IAssignedResourcesStorage _assignedResources;
    private BotStorage _botStorage;
    private Scanner _scanner;

    public void Init(Scanner scanner, IAssignedResourcesStorage assignedResources)
    {
        _assignedResources = assignedResources;
        _scanner = scanner;
        _botStorage = TryGetComponent(out BotStorage botStorage) ? botStorage : null;
    }

    public void SendBots()
    {
        List<Resource> resources = _scanner.GetResources();

        if (resources.Count > 0) 
            AssignResources(resources);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void AssignResources(List<Resource> resources)
    {
        foreach (Resource resource in resources)
        {
            if (_botStorage.TryGetBot(out Bot bot))
            {
                _assignedResources.Add(resource);
                bot.AssignTarget(resource);
                resource.Collected += OnResourceCollected;
                bot.TaskCompleted += OnTaskCompleted;
            }
            else
            {
                break;
            }
        }
    }

    private void OnResourceCollected(Resource resource)
    {
        resource.Collected -= OnResourceCollected;
        _assignedResources.Remove(resource);
    }

    private void OnTaskCompleted(Bot bot)
    {
        bot.TaskCompleted -= OnTaskCompleted;
        _botStorage.AddBot(bot);
    }
}
