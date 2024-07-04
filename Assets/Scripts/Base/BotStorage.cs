using System.Collections.Generic;
using UnityEngine;

public class BotStorage : MonoBehaviour
{
    [SerializeField, SerializeInterface(typeof(IBot))] private Bot[] _botObjects;
    [SerializeField] private Base _base;

    private readonly Queue<IBot> _bots = new();

    public int BotsAmount => _botObjects.Length;

    private void Awake()
    {
        InitBots();
    }

    public bool TryGetBot(out IBot bot)
    {
        if (IsEmpty())
        {
            bot = null;
            return false;
        }
        else
        {
            bot = _bots.Dequeue();
            return true;
        }
    }

    public void AddBot(IBot bot)
    {
        _bots.Enqueue(bot);
    }

    private void InitBots()
    {
        foreach (Bot botObject in _botObjects)
        {
            if (botObject.TryGetComponent(out IBot bot))
            {
                bot.Init(_base);
                _bots.Enqueue(bot);
            }
        }
    }

    private bool IsEmpty()
    {
        return _bots.Count == 0;
    }
}
