using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, ITarget
{
    [SerializeField] private BotStorage _botStorage;
    [SerializeField] private Scanner _scanner;

    [SerializeField] private float _sendBotsDelay;

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
        List<IResource> resources = GetAvailableResources();

        if (resources.Count > 0)
            AssignResourcesToBots(resources);
    }

    private void AssignResourcesToBots(List<IResource> resources)
    {
        foreach (IResource resource in resources)
        {
            if (_botStorage.TryGetBot(out IBot bot))
            {
                bot.AssignResource(resource);
                bot.ResourceCollected += OnResourceCollected;
            }
            else
            {
                break;
            }
        }
    }

    private void OnResourceCollected(IBot bot)
    {
        bot.ResourceCollected -= OnResourceCollected;
        _botStorage.AddBot(bot);
    }

    private List<IResource> GetAvailableResources()
    {
        return _scanner.GetResources(_botStorage.BotsAmount);
    }
}
