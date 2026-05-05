using PSW.Code.Dial;
using PSW.Code.Dial.Slot;
using PSW.Code.EventBus;
using System.Collections.Generic;
using UnityEngine;
using Work.CSH.Scripts.Managers;
using YIS.Code.Skills;

public class SlotIcon_Model : SkillIconSetting_Model
{
    [SerializeField] private TurnManagerSO turnManager;
    [SerializeField] private SizeData selectBoxSizeData;
    [SerializeField] private SizeData arrowRotationData;

    [SerializeField] private List<string> nextChainAnimClipNameList;

    [SerializeField] private float coolTimeSetTime = 0.2f;
    [SerializeField] private float selectBoxTime = 0.3f;

    private SkillDataSO _currentSkillData;

    private ShowTooltipEvent _showTooltipEvent = new ShowTooltipEvent();

    private bool _donMoveIcon;
    private bool _isNextChain;

    #region Get
    public float GetCoolTimeSetTime() => coolTimeSetTime;
    public float GetSelectBoxTime() => selectBoxTime;
    public bool GetNextChain() => _isNextChain;
    public SkillDataSO GetSkillData() => _currentSkillData;
    public ShowTooltipEvent GetShowTooltipEvent() => _showTooltipEvent;
    public string GetNextChainEffectName(bool isUp) => isUp ? nextChainAnimClipNameList[0] : nextChainAnimClipNameList[1];
    public bool GetIsDonMove() => turnManager.Turn == false || _donMoveIcon;
    
    #endregion

    #region Set
    public Vector2 GetSelectBoxSizeData(bool isUp) => selectBoxSizeData.GetSize(isUp);
    public void SetNextChain(bool isNextChain) => _isNextChain = isNextChain;
    public void SetSkillData(SkillDataSO skillData)
    {
        _showTooltipEvent.skillData = skillData;
        _currentSkillData = skillData;
    }
    public void SetDonMoveIcon(bool isDonMoveIcon)
    {
        _donMoveIcon = isDonMoveIcon;
    }
    #endregion

}

public struct ShowTooltipEvent : IEvent
{
    public SkillDataSO skillData;
}
