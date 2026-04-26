using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MiraSkill : CharacterSkill
{
    public Action OnCurse;
    protected override void AwakePlayer()
    {
        _health.isResurrection = true;
        if(eventFeedbacks != null) OnCurse -= eventFeedbacks.PlayFeedbacks;
    }

    protected override void UpdateCharacterSkill()
    {
        if (_stat.Health <= 0 &&!isSkillStart)
        {
            isSkillStart = true;
            MireSkillStart();
        }
    }

    private void MireSkillStart()
    {
        print("????? ???????");
        if(_stat.Damage > 0)
            _stat.Damage++;
        _stat.CoolTime--;
        StartCoroutine(DieTime());
    }

    private IEnumerator DieTime()
    {
        yield return new WaitForSeconds(5f);
        _health.OnDeadEvent?.Invoke();
        GameManager.Instance.OnGameWin?.Invoke(!_player.InputReaderCompo.IsRight);
    }

    private void OnDisable()
    {
        OnCurse -= eventFeedbacks.PlayFeedbacks;
    }

}
