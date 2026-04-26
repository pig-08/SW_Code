using System;
using UnityEngine;
using UnityEngine.Events;

public class DevilSkill : CharacterSkill
{
    private int? _curBulletCount = null;
    public Action OnTransaction;
    private int practiceCount;
    protected override void AwakePlayer()
    {
        OnTransaction += eventFeedbacks.PlayFeedbacks;
        
    }
    public override void ActiveSkill()
    {
        if (_stat.BarrierCount == 0) return;
        OnTransaction?.Invoke();
        base.ActiveSkill();
        _stat.BarrierCount -= 1;
        _stat.Damage += 1;
        _curBulletCount = _stat.CurBulletCount - 1;
        practiceCount++;
    }

    protected override void UpdateCharacterSkill()
    {
        if (_curBulletCount == null) return;

        if  (_curBulletCount == _stat.CurBulletCount)
        {
            _curBulletCount = null;
            _stat.Damage -= practiceCount;
        }
    }

    private void OnDisable()
    {
        _player.InputReaderCompo.OnSkillEvent -= ActiveSkill;
        OnTransaction -= eventFeedbacks.PlayFeedbacks;
    }
}
