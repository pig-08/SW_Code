using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IPlayerComponents
{
    public UnityEvent OnDeadEvent;
    public UnityEvent OnHitEvent;
    
    private Player _player;
    private StatData _stat;
    public int IsInvincibilityHit { get; private set; }
    public bool IsInvincibility {  get; set; }
    private float invincibilityTime = 1f;

    public bool isResurrection { get; set; }
    public bool IsDead { get; private set; }
    public int SkillNumder { get; set; }

    public void Initialize(Player player)
    {
        _player = player;
        _stat = _player.StatDataCompo;
        OnDeadEvent.AddListener(() =>
        {
            _player.GetCompo<PlayerAni>().PlayAni(PlayerAniName.Die);
        });
    }

    public void TakeDamage(int damage)
    {
        if(IsDead) return;
        
        if (IsInvincibility)
        {
            IsInvincibilityHit++;
            if (SkillNumder == 1)
            {
                _stat.Health++;
                SkillNumder = 0;
                IsInvincibility = false;
            }
            return;
        }

        if (_player.IsOnBarrier)
        {
            if(_stat.BarrierCount > 0)
            {
                if (SkillNumder != 2) _stat.BarrierCount--;
                _player.OnHitBarrier?.Invoke();
                return;
            }
        }
        
        _player.GetCompo<PlayerAni>().PlayAni(PlayerAniName.Hit);
        _stat.Health -= damage;
        if (_stat.Health <= 0 && !isResurrection)
        {
            IsDead = true;
            OnDeadEvent?.Invoke();
            GameManager.Instance.OnGameWin?.Invoke(!_player.InputReaderCompo.IsRight);
        }
        else
        {
            OnHitEvent?.Invoke();
            IsInvincibility = true;
            InvincibilityStart(_player.PlayerSpriteRenderer);
            InvincibilityStart(_player.MaskSpriteRenderer);
        }
    }

    private void InvincibilityStart(SpriteRenderer _spriteRenderer)
    {
        _spriteRenderer.DOFade(0.6f, 0.2f);
        StartCoroutine(PlayerInvincibilityStart(invincibilityTime,_spriteRenderer));
    }


    private IEnumerator PlayerInvincibilityStart(float Time,SpriteRenderer _spriteRenderer)
    {
        yield return new WaitForSeconds(Time);
        _spriteRenderer.DOFade(1f, 0.2f);
        IsInvincibility = false;
    }
}
