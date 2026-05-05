using PSW.Code.Deck;
using PSW.Code.Dial.Slot;
using UnityEngine;
using UnityEngine.UI;
using YIS.Code.Skills;

public class DeckSkillSlot_Model : ModelCompo<int>
{
    [SerializeField] private int index;
    [SerializeField] private Button slotButton;

    [SerializeField] private float outlinePopTime = 0.2f;

    private DeckSkillDataListSO _deckDataList;
    private SkillIconSetting_Controller _skillIcon;

    private bool _isLock;
    public override int InitModel()
    {
        _skillIcon = GetComponentInChildren<SkillIconSetting_Controller>();
        return index;
    }

    public float GetOutLinePopTime() => outlinePopTime;
    public void SetUpIsLock(bool isLock) => _isLock = isLock;
    public void SetDeckSkillData(DeckSkillDataListSO deckDataList) => _deckDataList = deckDataList;
    public DeckSkillDataListSO GetDeckSkillData() => _deckDataList;
    public SkillIconSetting_Controller GetSkillIcon() => _skillIcon;
    public Button GetSlotButton() => slotButton; 
    public int GetIndex() => index;
    public bool GetIsLock() => _isLock;

}
