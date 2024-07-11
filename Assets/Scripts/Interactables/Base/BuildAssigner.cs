using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BotStorage))]
public class BuildAssigner : MonoBehaviour
{
    [SerializeField] private float _assignDelay;
    [SerializeField] private int _buildCost;

    private BotStorage _botStorage;
    private ResourceStorage _resourceStorage;
    private Coroutine _assignCoroutine;

    public bool IsBusy { get; private set; }

    public event Action<BuildAssigner, Bot> DestinationReached;

    public void Init(ResourceStorage resourceStorage)
    {
        _resourceStorage = resourceStorage;
        IsBusy = false;
        _botStorage = TryGetComponent(out BotStorage botStorage) ? botStorage : null;
    }

    public void AssignBuild(Flag flag)
    {
        StopAssignBuild();
        _assignCoroutine = StartCoroutine(AssignCoroutine(flag));
    }

    private void TryAssign(Flag flag)
    {
        if (_resourceStorage.IsEnough(_buildCost) && _botStorage.TryGetBot(out Bot bot))
        {
            IsBusy = true;
            _resourceStorage.SpendResources(_buildCost);
            bot.AssignTarget(flag);
            bot.TaskCompleted += OnTaskCompleted;
        }
    }

    private void OnTaskCompleted(Bot bot)
    {
        bot.TaskCompleted -= OnTaskCompleted;
        IsBusy = false;
        DestinationReached?.Invoke(this, bot);
    }

    private void StopAssignBuild()
    {
        if (_assignCoroutine != null)
            StopCoroutine(_assignCoroutine);
    }

    private IEnumerator AssignCoroutine(Flag flag)
    {
        WaitForSeconds wait = new(_assignDelay);

        while (enabled && IsBusy == false)
        {
            TryAssign(flag);
            yield return wait;
        }
    }
}
