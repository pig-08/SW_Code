using System;
using UnityEngine;
using UnityEngine.Events;

public class NinjaSkill : CharacterSkill
{
    private int deductedBarrierCount;
    public Action OnReflex;
    private SelectManager _selectManager;

    protected override void AwakePlayer()
    {
        if (eventFeedbacks != null) OnReflex += eventFeedbacks.PlayFeedbacks;
        deductedBarrierCount = _stat.BarrierCount - 1;
        _selectManager = FindFirstObjectByType<SelectManager>();
    }
    protected override void UpdateCharacterSkill()
    {
        if(deductedBarrierCount == _stat.BarrierCount)
        {
            if (_player.InputReaderCompo.IsRight)
            {
                _selectManager.PlayerGroup[0].StatDataCompo.Health--;
            }
            else
            {
                _selectManager.PlayerGroup[1].StatDataCompo.Health--;
            }
            OnReflex?.Invoke();
            deductedBarrierCount = _stat.BarrierCount - 1;
        }
    }

    private void OnDisable()
    {
        if (eventFeedbacks != null) OnReflex -= eventFeedbacks.PlayFeedbacks;
    }
}
