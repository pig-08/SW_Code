using System;
using UnityEngine;
using UnityEngine.Events;
using YIS.Code.Skills;

namespace PSW.Code.Deck
{
    public class DeckSkillSlot_Controller : MonoBehaviour
    {
        [SerializeField] private DeckSkillSlot_Model model;
        [SerializeField] private DeckSkillSlot_View view;

        public Action<DeckSkillSlot_Controller> OnSlotClickEvnet;

        private void Awake()
        {
            model.InitModel();
            //AddButtonAction(Click);
        }

        public void Init(DeckSkillDataListSO deckDataList)
        {
            model.SetDeckSkillData(deckDataList);
        }

        private void OnDestroy()
        {
            OnSlotClickEvnet = null;
        }

        public void SetLockOn(bool isOnSkill, bool isForce = false)
        {
            if (model.GetDeckSkillData().deckInSkills[model.GetIndex()] == null || isForce)
            {
                model.SetUpIsLock(isOnSkill);
                view.SetLockOn(model.GetIsLock(), model.GetOutLinePopTime());
                return;
            }
        }

        public int GetIndex() => model.GetIndex();

        public void InSkill(SkillDataSO skillData)
        {
            if (skillData == null)
                return;

            model.GetDeckSkillData().deckInSkills[model.GetIndex()] = skillData;
            model.GetSkillIcon().SetSkill(skillData);
            PopUp(true);
        }

        public void PopUp(bool isUp) => model.GetSkillIcon().PopUp(isUp);

    }
}