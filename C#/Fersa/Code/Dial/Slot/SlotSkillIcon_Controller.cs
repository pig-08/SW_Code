using System;
using UnityEngine.EventSystems;
using YIS.Code.Skills;
using static UnityEngine.EventSystems.PointerEventData;

namespace PSW.Code.Dial.Slot
{
    public class SlotSkillIcon_Controller : SlotIcon_Controller, IPointerClickHandler
    {
        private SlotSkillIcon_View _viewSkill;
        private SlotSkillIcon_Model _modelSkill;  

        public Action OnIconRightClick;

        public override void Init()
        {
            _view = view as SlotIcon_View;
            _model = model as SlotIcon_Model;
            _viewSkill = view as SlotSkillIcon_View;
            _modelSkill = model as SlotSkillIcon_Model;
        }

        public void Chaining(bool isChaining)
        {
            if (isChaining)
                _viewSkill.ShakeIcon(_modelSkill.GetShakeValueData());

            _viewSkill.PlayChainEffect(_modelSkill.GetChainEffectName(isChaining));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == InputButton.Right)
            {
                _model.SetSkillData(null);
                OnIconRightClick?.Invoke();
                Chaining(false);
            }
        }

        public override void SetSkill(SkillDataSO skillData)
        {
            base.SetSkill(skillData);
            _modelSkill.SetSkillData(skillData);
        }

        public override void NextChainNot()
        {
            base.NextChainNot();
        }

        public override void PopUp(bool isPopUp)
        {
            base.PopUp(isPopUp);
        }
        public bool GetPopUp() => model.GetPopUp();

    }
}