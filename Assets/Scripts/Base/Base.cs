using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour, ITarget, IInteractable
{
    [SerializeField] private BotStorage _botStorage;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private float _sendBotsDelay;

    private readonly List<Resource> _assignedResources = new();

    private void Start()
    {
        InvokeRepeating(nameof(SendBots), _sendBotsDelay, _sendBotsDelay);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void SendBots()
    {
        List<Resource> resources = GetAvailableResources();

        if (resources.Count > 0)
            AssignResourcesToBots(resources);
    }

    private void AssignResourcesToBots(List<Resource> resources)
    {
        foreach (Resource resource in resources)
        {
            if (_botStorage.TryGetBot(out IBot bot))
            {
                bot.AssignResource(resource);
                _assignedResources.Add(resource);
                bot.ResourceCollected += OnResourceCollected;
            }
            else
            {
                break;
            }
        }
    }

    private void OnResourceCollected(IBot bot, Resource resource)
    {
        bot.ResourceCollected -= OnResourceCollected;
        _assignedResources.Remove(resource);
        _botStorage.AddBot(bot);
    }

    private List<Resource> GetAvailableResources()
    {
        int availableBotsAmount = _botStorage.BotsAmount;

        return _scanner.GetResources(_assignedResources, availableBotsAmount);
    }
}
