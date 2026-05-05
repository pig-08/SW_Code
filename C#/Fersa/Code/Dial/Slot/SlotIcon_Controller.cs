using CIW.Code.System.Events;
using PSW.Code.CombinationSkill;
using PSW.Code.EventBus;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using YIS.Code.Defines;
using YIS.Code.Skills;

namespace PSW.Code.Dial.Slot
{
    public class SlotIcon_Controller : SkillIconSetting_Controller
    {
        public Action<SlotIcon_Controller> OnClickEvent;

        protected SlotIcon_View _view; 
        protected SlotIcon_Model _model;

        public virtual void Init()
        {
            _view = view as SlotIcon_View;
            _model = model as SlotIcon_Model;

            Bus<ChangeCurrentSlot>.OnEvent += NextChain;
            Bus<SkillIconUnequippedEvent>.OnEvent += NotNextChain;
        }

        private void OnDisable()
        {
            Bus<ChangeCurrentSlot>.OnEvent -= NextChain;
            Bus<SkillIconUnequippedEvent>.OnEvent -= NotNextChain;
        }
        public void SetCoolTime(CoolTimeEvemt evt)
        {
            if (evt.DefaultTime <= 0) return;

            _model.SetDonMoveIcon(evt.CurrentTime > 0);
            _view.SetCoolTime((float)evt.CurrentTime / evt.DefaultTime, _model.GetCoolTimeSetTime());
        }

        public virtual void NextChain(List<(SkillDataSO, bool)> skillList, bool isCurrent = true, bool isInSkill = true)
        {
            NextChainNot();

            bool next = false;
            bool previous = false;

            if (skillList == null)
                return;

            foreach ((SkillDataSO, bool) skill in skillList)
            {
                if (skill.Item1 == _model.GetSkillData() || _model.GetSkillData() == null || skill.Item1 == null)
                    continue;

                BaseSkill currentSkill = _model.GetSkillData().skillPrefab.GetComponent<BaseSkill>();

                if (currentSkill.SkillData == null)
                    currentSkill.SetData(_model.GetSkillData());

                BaseSkill inSkill = skill.Item1.skillPrefab.GetComponent<BaseSkill>();

                if (inSkill.SkillData == null)
                    inSkill.SetData(skill.Item1);

                if(isInSkill)
                {
                    next = isChain(inSkill, currentSkill, CheckType.Next, next, skill.Item2);
                    previous = isChain(inSkill, currentSkill, CheckType.Previous, previous, skill.Item2 == false);
                }
            
                if(isCurrent)
                {
                    next = next || isChain(currentSkill, inSkill, CheckType.Next, next, skill.Item2 == false);
                    previous = previous || isChain(currentSkill, inSkill, CheckType.Previous, previous, skill.Item2);
                }
            }

            if (next || previous)
                SetNextChainEffect(true);

            _model.SetNextChain(next || previous);
        }

        public bool isChain(BaseSkill inSkill, BaseSkill currentSkill, CheckType type, bool isOrigin, bool isLeft)
        {
            return ((inSkill.SkillData.checkSkillType == type)
                    && inSkill.CanChainNext(currentSkill) || isOrigin)
                    && isLeft;
        }

        public void NextChain(ChangeCurrentSlot evt) => NextChain(evt.Skill);
        public void NotNextChain(SkillIconUnequippedEvent evt) => NextChainNot();
        
        public virtual void NextChainNot()
        {
            if (_model.GetNextChain())
                SetNextChainEffect(false);

            _model.SetNextChain(false);
        }
        public override void SetSkill(SkillDataSO skillData)
        {
            base.SetSkill(skillData);
            _model.SetSkillData(skillData);
        }
        public void SetNextChainEffect(bool isUp)
        {
            _view.PlayNextChainEffect(_model.GetNextChainEffectName(isUp));
        }
        public void SetDonMoveIcon(bool isDonMove) => _model.SetDonMoveIcon(isDonMove);
        public void InvokeShowTooltipEvent() => Bus<ShowTooltipEvent>.Raise(_model.GetShowTooltipEvent());
        public void InvokeHideTooltipEvent() => Bus<PointerExitEvent>.Raise(new PointerExitEvent());
        public virtual void SetCurrentButton(bool isCurrent)
        {
            _view.SetSelectBoxSize(_model.GetSelectBoxSizeData(isCurrent), _model.GetSelectBoxTime());
        }

        public SkillDataSO GetSkill() => _model.GetSkillData();

        public void Click()
        {
            if(_model.GetIsDonMove() == false)
                OnClickEvent?.Invoke(this);
        }
        public override void PopUp(bool isPopUp)
        {
            base.PopUp(isPopUp);
        }

    }

    public struct GetIsCurrentSlot : IEvent {}
    public struct ChangeCurrentSlot : IEvent
    {
        public List<(SkillDataSO, bool)> Skill;
        public ChangeCurrentSlot(List<(SkillDataSO, bool)> skill)
        {
            Skill = skill;
        }
    }

    public struct CoolTimeEvemt : IEvent
    {
        public CoolTimeEvemt(SkillDataSO currentSkillData, int currentTime, int defaultTime, bool isEndTime)
        {
            CurrentSkillData = currentSkillData;
            CurrentTime = currentTime;
            DefaultTime = defaultTime;
            IsEndTime = isEndTime;
        }

        public SkillDataSO CurrentSkillData;
        public int CurrentTime;
        public int DefaultTime;
        public bool IsEndTime;
    }
}