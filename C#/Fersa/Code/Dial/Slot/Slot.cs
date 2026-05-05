using PSW.Code.CombinationSkill;
using PSW.Code.EventBus;
using System;
using UnityEngine;
using YIS.Code.Skills;

namespace PSW.Code.Dial.Slot
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private GameObject skillIcon;

        private SkillDataSO _currentSkillData;
        private CoolTimeEvemt _coolTime;

        private SlotIcon_Controller _slotIconCompo;

        public void SetSkillData(SkillDataSO skillData, Action<SlotIcon_Controller> clickAction)
        {
            Bus<CoolTimeEvemt>.OnEvent += SetCoolTimeData;
            Bus<SkillIconUnequippedEvent>.OnEvent += SetUpIcon;
            _currentSkillData = skillData;
            NewIcon(clickAction);
            ChildSettingCompoCheck();
        }

        private void OnDestroy()
        {
            Bus<SkillIconUnequippedEvent>.OnEvent -= SetUpIcon;
            Bus<CoolTimeEvemt>.OnEvent -= SetCoolTimeData;
        }
        
        public void SetUpIcon(SkillIconUnequippedEvent evt)
        {
            if (_currentSkillData == null) return;

            if(evt.Skill == _currentSkillData)
                _slotIconCompo.PopUp(true);
        }

        public void SetCoolTimeData(CoolTimeEvemt evt)
        {
            if (_currentSkillData == null ||
                _currentSkillData.skillName != evt.CurrentSkillData.skillName)
                return;

            print(_currentSkillData.skillName + "쿨타임 있음");
            _coolTime = evt;
        }
        public void DonMoveSlot() => _slotIconCompo?.SetDonMoveIcon(true);

        private void NewIcon(Action<SlotIcon_Controller> clickAction)
        {
            if (_currentSkillData == null) return;

            if (transform.childCount == 0)
            {
                GameObject newIcon = Instantiate(skillIcon, transform);
                _slotIconCompo = newIcon.GetComponent<SlotIcon_Controller>();
                _slotIconCompo.OnClickEvent += clickAction;
                _slotIconCompo.Init();
                _slotIconCompo.SetSkill(_currentSkillData);
                _slotIconCompo.PopUp();
            }
        }

        public void ChildSettingCompoCheck()
        {
            if(_slotIconCompo == null) return;

            _slotIconCompo.PopUp(true);
            _slotIconCompo.SetDonMoveIcon(false);
            _slotIconCompo.SetCoolTime(_coolTime);
            _slotIconCompo.NextChainNot();
        }
    }
}