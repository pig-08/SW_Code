using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AnubisSkill : CharacterSkill
{
    public Action OnDamageUp;
    private DamageCaster damageCaster;
    protected override void AwakePlayer()
    {
        OnDamageUp += eventFeedbacks.PlayFeedbacks;
        damageCaster = GetComponentInChildren<DamageCaster>();
        print(damageCaster);
    }
    protected override void UpdateCharacterSkill()
    {
        if (_stat.Health == 1 && !isSkillStart)
        {
            isSkillStart = true;
            OnDamageUp?.Invoke();
            _stat.Damage++;
            damageCaster.OnShoot += (v) =>
            {
                StartCoroutine(DamagePlus());
            };
        }
    }

    private IEnumerator DamagePlus()
    {
        yield return new WaitForSeconds(0.3f);
        _stat.Damage++;
        print("데미지 증가");
    }

    private void OnDisable()
    {
        OnDamageUp -= eventFeedbacks.PlayFeedbacks;
    }
}
