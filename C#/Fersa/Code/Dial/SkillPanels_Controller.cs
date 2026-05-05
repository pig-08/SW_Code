using CIW.Code.System.Events;
using PSB.Code.BattleCode.UIs;
using PSW.Code.EventBus;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Work.CSH.Scripts.Battle;
using Work.CSH.Scripts.Interfaces;
using Work.CSH.Scripts.Managers;
using Work.PSB.Code.CoreSystem;

namespace PSW.Code.Dial
{
    public class SkillPanels_Controller : MonoBehaviour, ITurnable
    {
        [field:SerializeField] public TurnManagerSO TurnManager { get; set; }
        [SerializeField] private SkillPanels_View view;
        [SerializeField] private SkillPanels_Model model;
        [SerializeField] private BattleEnterContextSO battleEnterContext;
        private void Start()
        {
            
            TurnManager.AddITurnableList(this);
            Vector2 pos = view.GetPanelTrm().localPosition;
            pos.x = 0;
            model.SetUpData(pos);
            pos.y -= (model.GetPaenlLayout().preferredHeight + 100);
            model.SetDownData(pos);
            Bus<OnAttackEvent>.OnEvent += PopDownPanel;
            StartCoroutine(NextStart());
        }

        public IEnumerator NextStart()
        {
            yield return null;
            yield return null;
            model.VerticalLayoutGroup.enabled = false;
            view.DOKill();
            view.Init(model.GetMovePos(false)); // battleEnterContext.EnterBy == BattleEnterBy.Player
        }

        private void OnDestroy()
        {
            TurnManager.RemoveITurnableList(this);
            Bus<OnAttackEvent>.OnEvent -= PopDownPanel;
        }

        private void PopDownPanel(OnAttackEvent evt)
        {
            EnemySkillTooltipManager.Hide();
            view.SetMove(model.GetMovePos(false), model.GetTime(), model.GetEase());
        }

        public void EnterQuickSlotMode()
        {
            EnemySkillTooltipManager.Hide();
            view.SetMove(model.GetMovePos(false) + new Vector2(0, 250), model.GetTime(), model.GetEase());
        }
        public void ExitQuickSlotMode()
        {
            view.SetMove(model.GetMovePos(true), model.GetTime(), model.GetEase());
        }
        

        public void OnEndTurn(bool isPlayerTurn)
        {
        }

        public void OnStartTurn(bool isPlayerTurn)
        {
            view.SetMove(model.GetMovePos(isPlayerTurn), model.GetTime(), model.GetEase());
        }
    }
}