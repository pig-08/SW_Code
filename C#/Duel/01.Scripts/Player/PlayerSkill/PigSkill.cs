using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PigSkill : CharacterSkill
{
    public Action OnEat;
    private DamageCaster damageCaster;
    protected override void AwakePlayer()
    {
        damageCaster = GetComponentInChildren<DamageCaster>();
        OnEat += eventFeedbacks.PlayFeedbacks;

        if(!(_stat.Damage <= 0))
        {
            _stat.Damage = 1;
            print(_stat.Damage);
            damageCaster.OnShoot += (v) => StartCoroutine(DamagePlus());
        }


    }

    public override void ActiveSkill()
    {
        if (isSkillStart) return;
        OnEat?.Invoke();
        isSkillStart = true;
        _health.IsInvincibility = true;
        _health.SkillNumder = 1;
    }
    private IEnumerator DamagePlus()
    {
        yield return new WaitForSeconds(0.3f);
        _stat.Damage = 1;
    }
    private IEnumerator DamageZone()
    {
        yield return new WaitForSeconds(0.3f);
        _stat.Damage = 0;
    }

    private void OnDisable()
    {
        _player.InputReaderCompo.OnSkillEvent -= ActiveSkill;
        OnEat -= eventFeedbacks.PlayFeedbacks;
    }
}
