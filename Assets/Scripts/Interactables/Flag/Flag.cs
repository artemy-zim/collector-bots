using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Flag : MonoBehaviour, ISpawnable, ITarget, IInteractable
{
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Reset()
    {
        gameObject.SetActive(false);
    }
}
