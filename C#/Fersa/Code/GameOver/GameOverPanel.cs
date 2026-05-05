using CIW.Code.System.Events;
using Code.Scripts.Entities;
using DG.Tweening;
using PSB.Code.CoreSystem.SaveSystem;
using PSB_Lib.StatSystem;
using PSW.Code.Dial;
using PSW.Code.EventBus;
using System.Collections.Generic;
using UnityEngine;
using Work.PSB.Code.CoreSystem;
using Work.PSB.Code.CoreSystem.Tests;
using YIS.Code.Defines;
using YIS.Code.Events;
using YIS.Code.Skills;

namespace PSW.Code.GameOver
{
    public class GameOverPanel : MainUiPopCompo
    {
        [SerializeField] private ItemPanel itemPanel;
        [SerializeField] private SkillPanel skillPanel;
        [SerializeField] private GameOverTextPanel textPanel;
        [SerializeField] private TransitionController controller;
        [SerializeField] private InventoryCode inventory;

        [SerializeField] private SkillDataListSO startSkillDataList;
        [SerializeField] private SkillCircleDataListSO colorDataList;
        [SerializeField] private GameObject playerVisual;
        [SerializeField] private Camera playerCam;
        [SerializeField] private EntityStat playerStat;

        [SerializeField] private float overCamSize;
        [SerializeField] private float overCamXPos;

        [SerializeField] private StatSO attackStat;
        [SerializeField] private StatSO defenseStat;
        [SerializeField] private StatSO hPStat;

        private List<SkillDataSO> _skillDataList;

        private void Awake()
        {
            if(playerVisual != null)
                playerCam.gameObject.SetActive(false);

            UiInit();
            Bus<SkillUpdateEvent>.OnEvent += SetSkillSlot;
            Bus<BattleEnd>.OnEvent += HandleBattleEnd;
        }

        private void OnDestroy()
        {
            Bus<BattleEnd>.OnEvent -= HandleBattleEnd;
            Bus<SkillUpdateEvent>.OnEvent -= SetSkillSlot;
        }

        public void SetSkillSlot(SkillUpdateEvent evt)
        {
            _skillDataList = evt.Skills;
            print($"{_skillDataList.Count} 이거 몇개일까?");
        }

        private void HandleBattleEnd(BattleEnd evt)
        {
            if(evt.IsVictory == false)
            {
                playerCam.gameObject.SetActive(true);
                playerVisual.transform.SetParent(null);
                PopUpPanel();
                SetText();
                SetSlot();
            }
        }

        private void SetText()
        {
            textPanel.SetGameOverText(GameOverTextType.Coin, CurrencyContainer.Get(ItemType.Coin).ToString());
            textPanel.SetGameOverText(GameOverTextType.BossCoin, CurrencyContainer.Get(ItemType.BossCoin).ToString());
            textPanel.SetGameOverText(GameOverTextType.PP, CurrencyContainer.Get(ItemType.PP).ToString());
            textPanel.SetGameOverText(GameOverTextType.Enemy, KillCounter.Instance.GetTotalKillCount().ToString());
            textPanel.SetGameOverText(GameOverTextType.Attack, Mathf.RoundToInt(playerStat.GetStat(attackStat).Value).ToString());
            textPanel.SetGameOverText(GameOverTextType.Defense, Mathf.RoundToInt(playerStat.GetStat(defenseStat).Value).ToString());
            textPanel.SetGameOverText(GameOverTextType.HP, Mathf.RoundToInt(playerStat.GetStat(hPStat).Value).ToString());
            textPanel.SetTimeText(PlayTimeCounter.Instance.GetCurrentTime());
        }

        private void SetSlot()
        {
            inventory.EnsureSlotsInitialized();
            for (int i = 0; i < inventory.inventorySlots.Length; ++i)
            {
                if (inventory.inventorySlots[i].item == null)
                    break;

                GameOverItemSlot slot = itemPanel.NewSlot(inventory.inventorySlots[i].item);
                slot.SetNumber(inventory.inventorySlots[i].amount.ToString());
            }

            for(int i = 0; i < startSkillDataList.skills.Length; ++i)
            {
                GameOverSkillSlot slot = skillPanel.NewSlot(startSkillDataList.skills[i]);
                slot.SetOutLineColor(colorDataList.GetOutLineColor(startSkillDataList.skills[i].grade));
            }

            for (int i = 0; i < _skillDataList.Count; ++i)
            {
                GameOverSkillSlot slot = skillPanel.NewSlot(_skillDataList[i]);
                slot.SetOutLineColor(colorDataList.GetOutLineColor(_skillDataList[i].grade));
            }
        }

        public void PopUpPanel()
        {
            PopUp();
            playerCam.DOOrthoSize(overCamSize, tweenDuration);
            playerCam.transform.DOLocalMoveX(overCamXPos, tweenDuration);
        }

        public void HomeButton()
        {
            PlayTimeCounter.Instance.StartPlayTime();
            Invoke(nameof(Transition), 0.75f);
        }

        private void Transition()
        {
            controller.Transition("PSB_Field");
        }
    }
}