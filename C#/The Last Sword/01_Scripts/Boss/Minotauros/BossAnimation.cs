using DG.Tweening;
using System;
using UnityEngine;

namespace SW.Code.Boss
{
    public class BossAnimation : MonoBehaviour, BossInterface
    {
        private Animator _bossAnimator;
        private BossBrain _brain;
        public event Action OnCharging;
        public event Action OnAttackEnd;
        public void Init(BossBrain brain)
        {
            _brain = brain;
            _bossAnimator = GetComponent<Animator>();
        }

        public void Filp(bool right)
        {
            if (right) _brain.gameObject.transform.eulerAngles = Vector3.zero;
            else _brain.gameObject.transform.eulerAngles = new Vector3 (0, -180, 0);
        }

        public void ChangeAnimation(BossAnimationType animationType)
        {
            switch(animationType)
            {
                case BossAnimationType.idle:
                    _bossAnimator.Play("idle");
                    break;
                case BossAnimationType.walk:
                    _bossAnimator.Play("walk");
                    break;
                case BossAnimationType.attackReady:
                    _bossAnimator.Play("AttackReady");
                    break;
                case BossAnimationType.attackEnd:
                    _bossAnimator.Play("AttackEnd");
                    break;
                case BossAnimationType.death:
                    _bossAnimator.Play("death");
                    break;
                case BossAnimationType.hit:
                    _bossAnimator.Play("hit");
                    break;
            }
        }
        public void Patten1ChargingStart() => OnCharging?.Invoke();
        public void AttackEnd() => OnAttackEnd?.Invoke();
    }
}