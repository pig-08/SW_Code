using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MedusaSkill : CharacterSkill
{
    public Action OnPetrification;
    protected override void AwakePlayer()
    {
        if (eventFeedbacks != null)
            OnPetrification += eventFeedbacks.PlayFeedbacks;
    }
    public override void ActiveSkill()
    {
        if (isSkillStart) return;
        OnPetrification?.Invoke();
        _player.Barrier.GetComponent<SpriteRenderer>().color = new Color(Color.gray.r,Color.gray.g,Color.gray.b,0.85f);
        _health.SkillNumder = 2;
        base.ActiveSkill();
        StartCoroutine(SkillTime());
    }

    private IEnumerator SkillTime()
    {
        yield return new WaitForSeconds(2f);
        _player.Barrier.GetComponent<SpriteRenderer>().color = _player.BarrierColer;
        _health.SkillNumder = 0;
    }

    private void OnDisable()
    {
        _player.InputReaderCompo.OnSkillEvent -= ActiveSkill;
        OnPetrification -= eventFeedbacks.PlayFeedbacks;
    }
}
