using System.Collections.Generic;
using UnityEngine;

public class BotStorage : MonoBehaviour
{
    [SerializeField, SerializeInterface(typeof(IBot))] private GameObject[] _botObjects;

    private readonly Queue<IBot> _bots = new();

    public int BotsAmount => _botObjects.Length;

    private void Awake()
    {
        foreach(GameObject bot in _botObjects)
            _bots.Enqueue(bot.GetComponent<IBot>());
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

    private bool IsEmpty()
    {
        return _bots.Count == 0;
    }
}
