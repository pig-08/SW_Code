using System;
using UnityEngine;
using UnityEngine.Events;

public class BeastSkill : CharacterSkill
{
    private int? _curBulletCount = null;
    public Action OnRoaring;
    protected override void AwakePlayer()
    {
        if (eventFeedbacks != null)
            OnRoaring += eventFeedbacks.PlayFeedbacks;
    }

    public override void ActiveSkill()
    {
        if (isSkillStart) return;
        print("포효");
        isSkillStart = true;
        OnRoaring?.Invoke();
        _stat.Damage += 2;
        _curBulletCount = _stat.CurBulletCount - 2;
        base.ActiveSkill();
    }

    protected override void UpdateCharacterSkill()
    {
        if (_curBulletCount == null) return;

        if (_curBulletCount == _stat.CurBulletCount)
        {
            print("능력끝");
            _stat.Damage -= 2;
        }
    }

    private void OnDisable()
    {
        try
        {
            _player.InputReaderCompo.OnSkillEvent -= ActiveSkill;
            OnRoaring -= eventFeedbacks.PlayFeedbacks;
        }
        catch(Exception e)
        {
            return;
        }
    }
}
