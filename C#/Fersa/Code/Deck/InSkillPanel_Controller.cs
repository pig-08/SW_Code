using PSB.Code.BattleCode.Events;
using PSW.Code.EventBus;
using UnityEngine;
using UnityEngine.InputSystem;
using YIS.Code.Skills;

namespace PSW.Code.Deck
{
    public class InSkillPanel_Controller : MonoBehaviour
    {
        [SerializeField] private InSkillPanel_Model model;
        [SerializeField] private InSkillPanel_View view;

        private void Start()
        {
            model.SkillIcon_Circles = GetComponentsInChildren<SkillIcon_Circle_Controller>();
            model.DeckSkillSlots = GetComponentsInChildren<DeckSkillSlot_Controller>();

            model.SkillsPanel.OnAddSkill += AddSkill;

            Bus<VillageResetEvent>.OnEvent += HandleVillageReset;
            foreach (SkillIcon_Circle_Controller skillIcon in model.SkillIcon_Circles)
            {
                skillIcon.OnRightClickEvnet += DeleteSkill;
            }

            foreach (DeckSkillSlot_Controller deckSkillSlot in model.DeckSkillSlots)
            {
                deckSkillSlot.Init(model.DeckDataList);
                SkillDataSO data = model.DeckDataList.deckInSkills[deckSkillSlot.GetIndex()];

                if (data != null)
                    deckSkillSlot.InSkill(data);
                else
                    deckSkillSlot.PopUp(false);

                deckSkillSlot.SetLockOn(false, true);
            }
            model.SetIndex();
        }

        private void OnDestroy()
        {
            DeckDataSaver.DeckSave(model.DeckDataList.deckInSkills, 0, 0);
            Bus<VillageResetEvent>.OnEvent -= HandleVillageReset;
        }

        private void HandleVillageReset(VillageResetEvent evt)
        {
            ReSetDeck();
            Debug.Log("<color=purple>마을 초기화 - 장착 스킬</color>");
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Keyboard.current.rKey.wasPressedThisFrame)
                ReSetDeck();
        }
#endif

        public void ReSetDeck()
        {
            foreach (SkillIcon_Circle_Controller skillIcon in model.SkillIcon_Circles)
                skillIcon.NotChoice(false);

            foreach (DeckSkillSlot_Controller deckSkillSlot in model.DeckSkillSlots)
            {
                deckSkillSlot.PopUp(false);
            }

            model.DeckDataList.AllDataReSet();
            model.SetIndex();
        }


        public void AddSkill(SkillIcon_Circle_Controller skill)
        {
            if (model.GetIsPoolList())
                return;

            foreach (DeckSkillSlot_Controller slot in model.DeckSkillSlots)
                slot.SetLockOn(false);

            model.DeckSkillSlots[model.GetIndex()].InSkill(skill.ChoiceDeck());
            model.SetIndex();
        }

        public void DeleteSkill(SkillIcon_Circle_Controller skillIcon)
        {
            SkillDataSO data = skillIcon.ChoiceDeck();

            if (data == null)
                return;

            int index = model.DeckDataList.GetIndex(data);

            if (index <= -1)
                return;

            model.SkillsPanel.PopUpSkillIcon(data);
            model.DeckDataList.deckInSkills[index] = null;

            model.SetIndex();
        }

        public void AllClearSlot()
        {
            foreach (SkillIcon_Circle_Controller skillIcon in model.SkillIcon_Circles)
                DeleteSkill(skillIcon);
        }
    }
}

