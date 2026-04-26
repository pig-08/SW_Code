using SW.Code.Boss;
using System;
using UnityEngine;

public class AgisBossAnimation : MonoBehaviour, BossInterface
{
    private Animator _bossAnimator;
    private AgisBossBrain _bossBrain;

    public event Action OnAttack1;

    public event Action OnAttack2;
    public event Action OnAttack2Hit;
    public event Action OnAttack2End;

    public event Action OnAttack3;

    public event Action OnAttack4;

    public void Init(BossBrain brain)
    {
        _bossAnimator = GetComponent<Animator>();
        _bossBrain = (AgisBossBrain)brain;
    }

    public void ChangeTrueAnimation(AgisBossAnimationType animationType) => _bossAnimator.SetBool(animationType.ToString(), true);
    
    public void ChangeFalseAnimation(AgisBossAnimationType animationType) => _bossAnimator.SetBool(animationType.ToString(), false);

    public void OnAttack1Invoke() => OnAttack1?.Invoke();
    public void OnAttack2Invoke() => OnAttack2?.Invoke();
    public void OnAttack2HitInvoke() => OnAttack2Hit?.Invoke();
    public void OnAttack2EndInvoke() => OnAttack2End?.Invoke();
    public void OnAttack3Invoke() => OnAttack3?.Invoke();
    public void OnAttack4Invoke() => OnAttack4?.Invoke();
}
