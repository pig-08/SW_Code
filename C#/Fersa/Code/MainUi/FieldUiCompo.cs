using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class FieldUiCompo : MonoBehaviour
{
    [SerializeField] protected int weight;
    public Func<int, FieldUiCompo , bool> OnPopUpEvent;
    public event Action OnPopDownEvent;

    public bool IsHighWeight(int weight)
    {
        return weight > this.weight;
    }
    public virtual void PopDown()
    {
        OnPopDownEvent?.Invoke();
    }
}
