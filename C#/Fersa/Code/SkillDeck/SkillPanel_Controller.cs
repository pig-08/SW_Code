using CIW.Code.System.Events;
using PSW.Code.Battle;
using PSW.Code.CombinationSkill;
using PSW.Code.Deck;
using PSW.Code.Dial;
using PSW.Code.Dial.Slot;
using PSW.Code.EventBus;
using PSW.Code.Input;
using System;
using System.Collections.Generic;
using UnityEngine;
using Work.CSH.Scripts.Interfaces;
using Work.CSH.Scripts.Managers;
using YIS.Code.Skills;

namespace PSW.Code.SkillDeck
{
    public class SkillPanel_Controller : MonoBehaviour, ITurnable
    {
        [field:SerializeField] public TurnManagerSO TurnManager { get; set; }

        [SerializeField] private SkillPanel_Model model;
        [SerializeField] private List<SkillPanel_View> viewList;
        [SerializeField] private UiInputSO inputSO;

        private void Awake()
        {
            model.InitModel();
            TurnManager.AddITurnableList(this);


            for (int i = 0; i < viewList.Count; i++)
            {
                List<SkillDataSO> temps = model.CuttingSkillDataList(i, model.DeckDataList.GetCurrentIndexDeckData());
                viewList[i].Init(temps);
                viewList[i].SetList(temps, SetCurrentButton);
            }

            Bus<GetSkillDataEvent>.OnEvent += AddSkill;
            Bus<ChainingEvent>.OnEvent += ChainingDonMoveSlotList;
        }

        private void OnEnable()
        {
            model.DeckDataList.ChangeIndex(model.DeckDataList.GetIndex());
        }


        private void OnDestroy()
        {
            TurnManager.RemoveITurnableList(this);
            Bus<GetSkillDataEvent>.OnEvent -= AddSkill;
            Bus<ChainingEvent>.OnEvent -= ChainingDonMoveSlotList;
        }

        private void AddSkill(GetSkillDataEvent evt)
        {
            if (model.GetIcon() == null) return;

            evt.OnSkillEvent?.Invoke(model.GetIcon().GetSkill(), model.GetIcon().GetPopTime(), true);
            model.GetIcon()?.SetCurrentButton(false);
            model.GetIcon()?.PopUp(false);
            model.SetIcon(null);
        }

        private void ChainingDonMoveSlotList(ChainingEvent evt) => viewList.ForEach(v => v.DowMoveSlotList());
        public void OnStartTurn(bool isPlayerTurn)
        {
            if (isPlayerTurn)
            {
                for (int i = 0; i < viewList.Count; i++)
                    viewList[i].NextTurn(SetCurrentButton);
            }
        }

        public void SetCurrentButton(SlotIcon_Controller icon)
        {
            model.SetIcon(icon);
            Bus<GetIsCurrentSlot>.Raise(new GetIsCurrentSlot());
        }

        public void OnEndTurn(bool isPlayerTurn)
        {

        }
    }
}