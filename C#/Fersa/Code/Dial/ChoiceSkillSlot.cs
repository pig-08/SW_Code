using DG.Tweening;
using PSW.Code.Dial.Slot;
using PSW.Code.EventBus;
using System;
using UnityEngine;
using UnityEngine.UI;
using YIS.Code.Skills;

namespace PSW.Code.Dial
{
    public class ChoiceSkillSlot : MonoBehaviour
    {
        [SerializeField] private int currentIndex;
        [SerializeField] private float animDelayTime = 0.5f;

        private GetSkillDataEvent _InSkillChangeEvent;
        private bool _isDonMove;
        
        public event Action<ChoiceSkillSlot> OnCurrentSlotEvent;
        public event Action<ChoiceSkillSlot> OnNotSkillClickEvnet;
        public event Action<SkillDataSO, int, bool, bool> OnNewSkill;

        private GraphicRaycaster _graphicRaycaster;

        public ChoiceSkillSlot_Visual SkillSlot_Visual { private set; get; }
        public SlotSkillIcon_Controller SlotSkillIcon { private set; get; }

        private void Awake()
        {
            _graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
            SlotSkillIcon = GetComponentInChildren<SlotSkillIcon_Controller>();
            SkillSlot_Visual = GetComponentInChildren<ChoiceSkillSlot_Visual>();

            SlotSkillIcon.OnIconRightClick += SkillItOff;
            SlotSkillIcon.Init();

            _InSkillChangeEvent = new GetSkillDataEvent();
            _InSkillChangeEvent.OnSkillEvent += InSkillChange;
        }

        private void OnDestroy()
        {
            SlotSkillIcon.OnIconRightClick -= SkillItOff;
            _InSkillChangeEvent.OnSkillEvent -= InSkillChange;
        }

        public async void SetSkill(SkillDataSO skillData, float delayTime, bool isUnequipped = true)
        {
            SlotSkillIcon?.SetSkill(skillData);
            SlotSkillIcon?.SetCurrentButton(false);

            OnNewSkill?.Invoke(skillData, currentIndex, true, isUnequipped);
            
            _graphicRaycaster.enabled = false;

            PopUp(true);
            await Awaitable.WaitForSecondsAsync(delayTime - 0.1f);

            await Awaitable.WaitForSecondsAsync(animDelayTime);
            
            if (_isDonMove == false)
                _graphicRaycaster.enabled = true;
        }

        public void SetGraphicRaycaster(bool isEnabled) => _graphicRaycaster.enabled = isEnabled;

        public void InSkillChange(SkillDataSO skillData, float delayTime, bool isUnequipped)
        {
            SkillItOff();
            SetSkill(skillData, delayTime, isUnequipped);
        }

        public void SetPullSlot(bool isPullSlot)
        {
            OnCurrentSlotEvent?.Invoke(null);
            _isDonMove = isPullSlot;
        }
        public void InvokeGetSkillDataEvent() => Bus<GetSkillDataEvent>.Raise(_InSkillChangeEvent);

        public void SkillChange()
        {
            OnCurrentSlotEvent?.Invoke(this);
        }
        public void Click()
        {
            if(SlotSkillIcon.GetPopUp() == false)
                OnNotSkillClickEvnet?.Invoke(this);
        }

        public void SkillItOff()
        {
            PopUp(false);
            SlotSkillIcon.SetSkill(null);
            OnNewSkill?.Invoke(null, currentIndex, false, true);
            OnCurrentSlotEvent?.Invoke(null);
        }
        public void SkillItOff(bool isUnequipped)
        {
            PopUp(false);
            SlotSkillIcon.SetSkill(null);
            OnNewSkill?.Invoke(null, currentIndex, false, isUnequipped);
            OnCurrentSlotEvent?.Invoke(null);
        }

        public void PopUp(bool isUp)
        {
            SlotSkillIcon.PopUp(isUp);
            SkillSlot_Visual.SetSlotEffect(isUp);
        }

        public void StartChain(Action callBack = null) => SkillSlot_Visual.ChainEffect(SlotSkillIcon, callBack);
    }

    public struct GetSkillDataEvent : IEvent
    {
        public Action<SkillDataSO, float, bool> OnSkillEvent;
    }
}