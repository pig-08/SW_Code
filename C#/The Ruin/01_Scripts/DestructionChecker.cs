using UnityEngine;
using UnityEngine.Events;

public class DestructionChecker : MonoBehaviour
{
    public UnityEvent OnDestruction;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnDestructionInvoke();
        }
    }

    public void OnDestructionInvoke()
    {
        OnDestruction?.Invoke();
    }
}
