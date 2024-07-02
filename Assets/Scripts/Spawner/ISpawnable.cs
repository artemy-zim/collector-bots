using UnityEngine;

public interface ISpawnable
{
    public void OnSpawn(Vector3 position);
    public void OnDespawn();
}
