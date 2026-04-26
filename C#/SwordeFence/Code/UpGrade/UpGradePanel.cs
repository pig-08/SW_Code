using AJ;
using Gwamegi.Code.Managers;
using SW.Code.Managers;
using SW.Code.Stat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace SW.Code.UpGrade
{
    public class UpGradePanel : UpGradeComponent
    {
        [Header("가격")]
        [SerializeField] private float coinPrice;
        [SerializeField] private int specialCoinPrice;

        private int _currSpecialCoinCount;

        private VisualElement _turretImageIcon;
        private Label _nameText;
        private Label _notCoinText;

        private Tower _tower;

        private string[] buttonNameList = { "UpGrade", "SpecialUpGrade", "Stat", "Close" };
        private Button[] buttonList = new Button[4];

        private Dictionary<string, CoinPanelComponent> _coinComponentList = 
                new Dictionary<string, CoinPanelComponent>();

        private TurretStat _currTurretStat;

        private CoinDataManager _coinManager;
        public override void Init(VisualElement root)
        {
            base.Init(root);
            _turretImageIcon = _root.Q<VisualElement>("Icon");
            _nameText = _root.Q<Label>("Name");
            _notCoinText = _root.Q<Label>("NotCoinText");
            _coinManager = Manager.GetCompoManager<CoinDataManager>();
            
            AddComponents();
            ComponentListInit();
            for (int i = 0; i < 2; i++)
            {
                int index = i;

                buttonList[i] = _panel.Q<Button>(buttonNameList[index] + "Button");
                buttonList[i].RegisterCallback<ClickEvent>((v) => OpenCoinPanel(_coinComponentList.Keys.ToList()[index]));
            }
            buttonList[2] = _panel.Q<Button>("StatButton");
            buttonList[2].RegisterCallback<ClickEvent>((v) => OpenStatPanel());

            buttonList[3] = _panel.Q<Button>("CloseButton");
            buttonList[3].RegisterCallback<ClickEvent>((v) => ClosePanel());
        }

        private void OpenStatPanel()
        {
            _coinComponentList.TryGetValue("Stat", out CoinPanelComponent coinPanel);
            coinPanel.Open(_currTurretStat);
        }

        public bool GetAllOpen()
        {
            bool isAllOpen = false;

            foreach (CoinPanelComponent coinPanel in _coinComponentList.Values)
            {
                isAllOpen = isAllOpen || coinPanel.GetIsOpen();
            }

            return isAllOpen;
        }

        public override async void Open(TurretSO turretData, TurretStat turretStat)
        {
            _tower = Manager.GetCompoManager<UpGradeMangers>().currentTower;
            if (_tower == null)
            {
                Debug.Log("오류의 원흉이였던 새끼");
                return;
            }

            if (isAllOpen == false)
                return;
            else
                isAllOpen = false;

            if (isOpen)
            {
                SetPanel();
                await Awaitable.WaitForSecondsAsync(0.3f);
            }
            else
                isOpen = true;

            _currTurretStat = turretStat;

            _currSpecialCoinCount = _tower.specialCoinCount;
            _turretImageIcon.style.backgroundImage = new StyleBackground(turretData.TurretImage);
            _nameText.text = turretData.TurretName;

            if (_notCoinText.ClassListContains("Up"))
                _notCoinText.ToggleInClassList("Up");

            for (int i = 0; i < 2; i++)
            {
                buttonList[i].text = buttonNameList[i] == buttonNameList[0] ? $"Coin : {(int)coinPrice}"
                  : $"SpecialGode : {specialCoinPrice}({1}/{_currSpecialCoinCount})";
            }

            SetPanel();
            await Awaitable.WaitForSecondsAsync(0.5f);
            isAllOpen = true;
        }



        public override void ClosePanel()
        {
            isOpen = false;
            if (_coinComponentList.TryGetValue("Stat", out CoinPanelComponent coinPanel))
            {
                coinPanel.ClosePanel();
            }

            Manager.GetCompoManager<UpGradeMangers>().Close();
            
            SetPanel();
        }

        private void AddComponents()
        {
            GetComponentsInChildren<CoinPanelComponent>().ToList().ForEach(v => _coinComponentList.Add(v.GetPanelName(),v));
        }

        private void ComponentListInit()
        {
            _coinComponentList.Values.ToList().ForEach((v) => v.Init(_root));
        }

        private void OpenCoinPanel(string panelName)
        {
            _coinComponentList.TryGetValue(panelName, out CoinPanelComponent coinPanel);

            bool isCoin = panelName == _coinComponentList.Keys.ToList()[0];

            if(GetNotCoin(isCoin))
            {
                StopAllCoroutines();
                StartCoroutine(PopUpText("코인이 부족합니다."));
            }
            else if(GetNotCount(isCoin))
            {
                StopAllCoroutines();
                StartCoroutine(PopUpText("강화 횟수가 부족합니다."));
            }
            else
            {
                _coinManager.MinusCoin(isCoin ? (int)coinPrice : specialCoinPrice, !isCoin);
                coinPrice *= 1.2f;
                if (isCoin)
                    buttonList[0].text = $"Coin : {coinPrice}";
                else
                    buttonList[1].text = $"SpecialGode : {specialCoinPrice}({--_tower.specialCoinCount}/{1})";

                coinPanel.Open(_currTurretStat);
                ClosePanel();
            }
        }

        private IEnumerator PopUpText(string text)
        {
            if (_notCoinText.ClassListContains("Up"))
                _notCoinText.ToggleInClassList("Up");

            _notCoinText.text = text;
            _notCoinText.ToggleInClassList("Up");
            yield return new WaitForSeconds(0.4f);
            _notCoinText.ToggleInClassList("Up");
        }

        private bool GetNotCoin(bool isCoin)
        {
            if (isCoin)
                return _coinManager.GetCoin() < coinPrice;
            else
                return _coinManager.GetSpecialCoin() < specialCoinPrice;
        }

        private bool GetNotCount(bool isCoin)
        {
            if (isCoin)
                return false;
            else
                return _tower.specialCoinCount <= 0;
        }
    }
}