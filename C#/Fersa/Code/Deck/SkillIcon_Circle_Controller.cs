using PSW.Code.Dial.Slot;
using PSW.Code.EventBus;
using System;
using UnityEngine.EventSystems;
using YIS.Code.Skills;
using static UnityEngine.EventSystems.PointerEventData;

namespace PSW.Code.Deck
{
    public class SkillIcon_Circle_Controller : SkillIconSetting_Controller, IPointerClickHandler
    {
        private SkillIcon_Circle_View _view;
        private SkillIcon_Circle_Model _model;

        public Action<SkillIcon_Circle_Controller> OnClickEvnet;
        public Action<SkillIcon_Circle_Controller> OnRightClickEvnet;

        private void Awake()
        {
            _view = view as SkillIcon_Circle_View;
            _model = model as SkillIcon_Circle_Model;
        }

        private void OnDestroy()
        {
            OnClickEvnet = null;
            OnRightClickEvnet = null;
        }

        public override void SetSkill(SkillDataSO skillData)
        {
            base.SetSkill(skillData);
            _model.SetSkillIData(skillData);
            _view.SetEffectColor(skillData.grade);
        }

        public void DOKill() => _view.DOAllSkill();

        public void MouseOn()
        {
            //if (model.GetPopMove()) return;

            _model.SetUpOnMouse();
            _view.PlayMouseUpEffect(_model.GetMouseUpEffectName(_model.GetOnMouse()));
            if (_model.GetOnMouse())
                Bus<DescriptionSkillIEvent>.Raise(_model.GetSkillIEvent());
            else
                Bus<DescriptionSkillIEvent>.Raise(_model.GetSkillINullEvent());
        }

        public void DestroySkill()
        {
            Destroy(transform.parent.gameObject);
        }

        public void Click()
        {
            if(_model.GetPopUp() == false) return;

            Bus<DescriptionSkillIEvent>.Raise(_model.GetSkillIEvent(true));
            Bus<DonChangeSkillDataEvent>.Raise(new DonChangeSkillDataEvent(true));

            OnClickEvnet?.Invoke(this);
        }

        public void NotChoice(bool isNull = true)
        {
            _view.SetChoiceImageFade(0, _model.GetClickSizeChangeTime());

            if(isNull)
            {
                Bus<DonChangeSkillDataEvent>.Raise(new DonChangeSkillDataEvent(false));
                Bus<DescriptionSkillIEvent>.Raise(_model.GetSkillINullEvent());
            }
        }

        public SkillDataSO ChoiceDeck()
        {
            PopUp(false);
            return _model.GetSkillIEvent().skillData;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == InputButton.Right)
            {
                OnRightClickEvnet?.Invoke(this);
                NotChoice();
            }
        }
    }
}