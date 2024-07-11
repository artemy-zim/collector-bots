using System.Collections.Generic;
using UnityEngine;

public class BotStorage : MonoBehaviour
{
    private readonly Queue<Bot> _bots = new();

    public bool TryGetBot(out Bot bot)
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

    public void AddBot(Bot bot)
    {
        _bots.Enqueue(bot);
    }

    private bool IsEmpty()
    {
        return _bots.Count <= 0;
    }
}
