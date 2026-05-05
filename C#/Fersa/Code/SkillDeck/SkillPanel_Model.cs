using PSB.Code.FieldCode.BTs.Action;
using PSW.Code.Deck;
using PSW.Code.Dial.Slot;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using YIS.Code.Skills;

namespace PSW.Code.SkillDeck
{
    public class SkillPanel_Model : ModelCompo<object>
    {
        [field: SerializeField] public DecksListSO DeckDataList { private set; get; }

        [SerializeField] private AnimationClip downAnimation;
        [SerializeField] private AnimationClip upAnimation;
        [SerializeField] private int onePanelSkillNumber = 5;

        private SlotIcon_Controller _slotIcon;

        private bool _isStopMove = true;
        private float _animationTIme;
        private int _upIndex;

        public bool GetIsStopMove() => _isStopMove;
        public string GetAnimationName(bool isUp) => isUp ? upAnimation.name : downAnimation.name;
        public async void SetMovePanel()
        {
            _isStopMove = false;
            await Awaitable.WaitForSecondsAsync(_animationTIme);
            _isStopMove = true;
        }

        public override object InitModel()
        {
            _animationTIme = downAnimation.length;
            return "";
        }
        public bool IsUpIndex(int index) => _upIndex == index;

        public void NextIndex()
        {
            if (_upIndex == 1)
                _upIndex = 0;
            else
                _upIndex = 1;
        }
        public SlotIcon_Controller GetIcon() => _slotIcon;
        public void SetIcon(SlotIcon_Controller icon) => _slotIcon = icon;  

        public List<SkillDataSO> CuttingSkillDataList(int indexm, SkillDataSO[] dataList)
        {
            List<SkillDataSO> tempDataList = new List<SkillDataSO>();

            int dataSize = onePanelSkillNumber;
            int maxSize = ((indexm + 1) * onePanelSkillNumber) - 1;
            int magnification = (onePanelSkillNumber * indexm);

            if (dataList.Length < maxSize)
                dataSize = (dataList.Length - magnification);

            for (int i = 0; i < dataSize; ++i)
                tempDataList.Add(dataList[magnification + i]);

            return tempDataList;
        }
    }
}