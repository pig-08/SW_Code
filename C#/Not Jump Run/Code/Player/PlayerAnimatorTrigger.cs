using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimatorTrigger : MonoBehaviour, IPlayerComponent
{
    public UnityEvent OnDieEvent;
    public UnityEvent OnRunEvent;

    public void Init(Player player)
    {

    }

    public void PlayOnDieEvent() => OnDieEvent?.Invoke();
    public void PlayOnRunEvent() => OnRunEvent?.Invoke();

}
