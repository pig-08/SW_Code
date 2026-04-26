using SW.Code.Stat;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace SW.Code.UpGrade
{
    public class CoinPanel : CoinPanelComponent
    {
        private Button[] _statButtonList = new Button[3];

        private List<Label> _statTextList = new List<Label>();
        
        private List<int> _indexList = new List<int>();

        private int _mainIndex = -1;

        public override void Init(VisualElement root)
        {
            base.Init(root);

            for (int i = 0; i < _statButtonList.Length; ++i)
            {
                _statButtonList[i] = _root.Q<Button>("ButtonPanel" + i.ToString());
                _statTextList.Add(_statButtonList[i].Q<Label>("StatText"));
            }
        }

        public async override void Open(TurretStat turretStat)
        {
            base.Open(turretStat);
            await Awaitable.WaitForSecondsAsync(0.5f);

            int index = 0;

            foreach (Label text in _statTextList)
            {
                ScrollText(text, index++);
                await Awaitable.WaitForSecondsAsync(0.5f);
            }
            
        }

        public override void Close()
        {
            base.Close();
            foreach (Label text in _statTextList)
                text.ToggleInClassList("Up");

            foreach (Button button in _statButtonList)
            {
                button.ToggleInClassList("NotChoice");
                if (button != _statButtonList[_mainIndex])
                    button.ToggleInClassList("NotMain");

                button.UnregisterCallback<ClickEvent>(SetButtonEve);
            }

            _indexList.Clear();
            _mainIndex = -1;
        }

        private void SetButtonEve(ClickEvent evt)
        {
            Button targetButton = (Button)evt.target;

            int index = _statButtonList.ToList().IndexOf(targetButton);
            SetMainButton(targetButton, _indexList[index]);
        }

        private async void ScrollText(Label text, int buttonIndex)
        {
            int index = 0;

            bool isOddNumber = false;

            for (int i = 0; i < 30; ++i)
            {
                await Awaitable.WaitForSecondsAsync(0.03f);
                text.ToggleInClassList("Plus");

                await Awaitable.WaitForSecondsAsync(0.03f);
                text.ToggleInClassList("Plus");
                text.ToggleInClassList("Up");

                index = Random.Range(0, _statList.Count);
                bool isPlus = _statList[index].calculationType == CalculationType.Plus;
                string calculation = isPlus ? "+" : "%";
                int percent = isPlus ? 1 : 100;
                text.text = $"{_statList[index].statName} : {_statList[index].minRange * percent} ~ {_statList[index].maxRange * percent} ({calculation})";

                isOddNumber = i % 2 != 0;

                if (isSkip && _indexList.Count == buttonIndex)
                {
                    isSkip = !(_indexList.Count >= 2);
                    i = 31;
                }

            }

            _indexList.Add(index);
            Button indexButton = _statButtonList[buttonIndex];

            indexButton.ToggleInClassList("NotChoice");
            indexButton.RegisterCallback<ClickEvent>(SetButtonEve);
            if(isOddNumber)
                text.ToggleInClassList("Up");
        }

        private async void SetMainButton(Button mainButton, int index)
        {
            if (_mainIndex == _statButtonList.ToList().IndexOf(mainButton)) return;

            bool isOddNumber = false;

            foreach (Button button in _statButtonList)
            {
                if(button != mainButton)
                    button.ToggleInClassList("NotMain");
            }

            _mainIndex = _statButtonList.ToList().IndexOf(mainButton);

            bool isPlus = _statList[index].calculationType == CalculationType.Plus;
            Label text = _statTextList[_mainIndex];
            float randomRange = 0;
            for (int i = 0; i < 15; ++i)
            {
                
                await Awaitable.WaitForSecondsAsync(0.03f);
                text.ToggleInClassList("Plus");

                await Awaitable.WaitForSecondsAsync(0.03f);
                text.ToggleInClassList("Plus");
                text.ToggleInClassList("Up");

                randomRange = (float)System.Math.Floor(Random.Range(_statList[index].minRange, _statList[index].maxRange) * 10) / 10;
                randomRange = randomRange * (isPlus ? 1 : 100);
                text.text = $"{_statList[index].statName} : {randomRange}";

                isOddNumber = i % 2 != 0;

                if (isSkip)
                {
                    isSkip = false;
                    i = 31;
                }

            }

            randomRange = randomRange / (isPlus ? 1 : 100);
            if (isOddNumber == false)
                text.ToggleInClassList("Up");

            foreach (StatSO stat in statDataListSO.statSOListSO)
            {
                if(stat.Type == _statList[index].upGradepType)
                {
                    string id = System.Guid.NewGuid().ToString();
                    _upGradepTypeDtat.Add(id, stat.Type);
                    currTurretStat.AddModifier(stat, $"{stat.Type}in{id}", randomRange, !isPlus);
                }
            }

            await Awaitable.WaitForSecondsAsync(0.8f);
            Close();
        }
    }
}