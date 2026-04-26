using System;
using UnityEngine;
using UnityEngine.Events;

public class MochiSkill : CharacterSkill
{
    public Action OnHit; 
    protected override void AwakePlayer()
    {
        OnHit += eventFeedbacks.PlayFeedbacks;
        _health.IsInvincibility = true;
    }
    protected override void UpdateCharacterSkill()
    {

        if (_health.IsInvincibilityHit >= 3 && !isSkillStart)
        {
            _health.IsInvincibility = false;
            isSkillStart = true;
            OnHit?.Invoke();
        }
    }

    private void OnDisable()
    {
        OnHit -= eventFeedbacks.PlayFeedbacks;
    }
}
