using System;
using UnityEngine;
using UnityEngine.Events;

public class DemeterSkill : CharacterSkill
{
    private int? _curBulletCount = null;
    private DamageCaster _damageCaster;
    public Action OnHpRecovery;
    protected override void AwakePlayer()
    {
        _damageCaster = _player.GetComponentInChildren<DamageCaster>();
        OnHpRecovery += eventFeedbacks.PlayFeedbacks;
    }

    public override void ActiveSkill()
    {
        if (isSkillStart) return;
        _stat.Damage = 0;
        _curBulletCount = _stat.CurBulletCount - 1;
        OnHpRecovery?.Invoke();
        _damageCaster.OnShoot += HpRecovery;
        base.ActiveSkill();
    }

    protected override void UpdateCharacterSkill()
    {
        if (_stat.CurBulletCount == _curBulletCount)
            _damageCaster.OnShoot -= HpRecovery;
    }

    private void HpRecovery(bool isTrigger)
    {
        _stat.Health++;
        if(isTrigger)
            _damageCaster.OnShoot -= HpRecovery;
    }

    private void OnDisable()
    {
        _player.InputReaderCompo.OnSkillEvent -= ActiveSkill;
        OnHpRecovery -= eventFeedbacks.PlayFeedbacks;
    }
}
