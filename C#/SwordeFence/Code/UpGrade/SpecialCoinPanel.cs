using SW.Code.Stat;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;
using static Unity.Burst.Intrinsics.X86.Sse4_2;

namespace SW.Code.UpGrade
{
    public class SpecialCoinPanel : CoinPanelComponent
    {
        

        private string[] nameList = {"Name","Stat"};

        private Label[] _textList = new Label[2];
        private VisualElement[] _panelList = new VisualElement[2];

        public override void Init(VisualElement root)
        {
            base.Init(root);

            for(int i = 0; i < nameList.Length; ++i)
            {
                _textList[i] = _panel.Q<Label>(nameList[i] + "Text");
                _panelList[i] = _panel.Q<VisualElement>(nameList[i] + "Panel");
            }
        }

        public override void Open(TurretStat turretStat)
        {
            base.Open(turretStat);
            ScrollText();
        }

        public override void Close()
        {
            base.Close();

            foreach (Label text in _textList)
                text.ToggleInClassList("Up");
        }

        private async void ScrollText()
        {
            int index = 0;

            bool isOddNumber = false;

            for (int i = 0; i < 30; ++i)
            {
                await Awaitable.WaitForSecondsAsync(0.03f);
                _textList[0].ToggleInClassList("Plus");
                _textList[1].ToggleInClassList("Plus");

                await Awaitable.WaitForSecondsAsync(0.03f);

                foreach (Label text in _textList)
                {
                    text.ToggleInClassList("Plus");
                    text.ToggleInClassList("Up");
                }

                index = Random.Range(0, _statList.Count);

                _textList[0].text = $"{_statList[index].statName} :";
                _textList[1].text = $"{_statList[index].minRange * 100} ~ {_statList[index].maxRange * 100} (%)";

                isOddNumber = i % 2 != 0;

                if (isSkip)
                {
                    isSkip = false;
                    i = 31;
                }

            }

            foreach (Label text in _textList)
            {
                if(isOddNumber)
                    text.ToggleInClassList("Up");
            }
            await Awaitable.WaitForSecondsAsync(0.5f);
            RandomStatRange(index);
        }

        private async void RandomStatRange(int index)
        {
            STAT currStat = _statList[index];
            float statRange = 0;

            bool isOddNumber = false;

            for (int i = 0; i < 15; ++i)
            {
                await Awaitable.WaitForSecondsAsync(0.03f);
                _textList[1].ToggleInClassList("Plus");

                await Awaitable.WaitForSecondsAsync(0.03f);

                _textList[1].ToggleInClassList("Plus");
                _textList[1].ToggleInClassList("Up");

                statRange = (float)System.Math.Floor(Random.Range(currStat.minRange, currStat.maxRange) * 10) / 10;

                _textList[1].text = $"{statRange * 100} (%)";

                isOddNumber = i % 2 != 0;

                if (isSkip)
                {
                    isSkip = false;
                    i = 31;
                }
            }

            foreach (StatSO stat in statDataListSO.statSOListSO)
            {
                if (stat.Type == _statList[index].upGradepType)
                {
                    string id = System.Guid.NewGuid().ToString();
                    _upGradepTypeDtat.Add(id, stat.Type);
                    currTurretStat.AddModifier(stat, $"{stat.Type}in{id}", statRange, true);
                }
            }

            if (isOddNumber == false)
                _textList[1].ToggleInClassList("Up");

            await Awaitable.WaitForSecondsAsync(0.8f);
            Close();
        }
    }
}