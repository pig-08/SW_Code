using AJ;
using Gwamegi.Code.Input;
using SW.Code.SO;
using SW.Code.Stat;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

namespace SW.Code.UpGrade
{
    public class StatPanel : CoinPanelComponent
    {
        [SerializeField] private GameObject panel;

        private List<TextMeshProUGUI> _textList = new List<TextMeshProUGUI>();
        

        public override void Init(VisualElement root)
        {
            GetComponentsInChildren<TextMeshProUGUI>(true).ToList().ForEach(v => _textList.Add(v));
        }

        public override async void Open(TurretStat turretStat)
        {
            panel.SetActive(true);
            StatSO stat;
            for (int i = 0; i < statDataListSO.statSOListSO.Count; i++)
            {
                Color s = Color.yellow;
                stat = turretStat.GetStat(statDataListSO.statSOListSO[i]);
                _textList[i].text = $"{stat.statName}:{stat.BaseValue}<size=40>(<color=yellow>+{stat.Value - stat.BaseValue}</color>)</size>";
            }
            await Awaitable.WaitForSecondsAsync(0.5f);
            isAllOpen = true;
        }

        public override void ClosePanel()
        {
            isOpen = false;
            panel.SetActive(false);
        }

    }
}