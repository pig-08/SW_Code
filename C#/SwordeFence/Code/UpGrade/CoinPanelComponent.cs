using SW.Code.SO;
using SW.Code.Stat;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

namespace SW.Code.UpGrade
{
    public abstract class CoinPanelComponent : UpGradeComponent
    {
        [SerializeField] protected StatDataListSO statDataList;
        [SerializeField] protected StatSOListSO statDataListSO;
        [SerializeField] private UiInputSO inputSO;

        protected List<STAT> _statList;
        protected TurretStat currTurretStat;
        protected Dictionary<string, UpGradepType> _upGradepTypeDtat = new Dictionary<string, UpGradepType>();

        protected bool isSkip;

        public override void Init(VisualElement root)
        {
            base.Init(root);
            _statList = statDataList.statDataList;
        }

        public virtual void Open(TurretStat turretStat)
        {
            inputSO.OnSkipPressed += HandeSkipEvent;
            isOpen = true;
            currTurretStat = turretStat;
            SetPanel();
        }

        public virtual void Close()
        {
            inputSO.OnSkipPressed -= HandeSkipEvent;
            isOpen = false;
            isSkip = false; //¹¹Áö
            SetPanel();
        }

        public bool GetIsOpen() => isOpen;

        public async virtual void Scroll(Label text)
        {
            await Awaitable.WaitForSecondsAsync(0.03f);
            text.ToggleInClassList("Plus");

            await Awaitable.WaitForSecondsAsync(0.03f);
            text.ToggleInClassList("Plus");
            text.ToggleInClassList("Up");
        }

        private void HandeSkipEvent()
        {
            isSkip = true;
        }

    }
}