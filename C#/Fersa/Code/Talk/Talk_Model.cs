using System;
using System.Collections.Generic;
using UnityEngine;

namespace PSW.Code.Talk
{
    public class Talk_Model : ModelCompo<TalkData>
    {
        [Header("DefaultData")]
        [SerializeField] private Vector3 popUpMaskValue;
        [SerializeField] private float popTime = 0.3f;

        [field:SerializeField] public ChoicePanel_Controller Controller;

        [SerializeField] private float defaultWaitTime = 0.3f;
        [field:SerializeField] private float defaultspeedWaitTime = 0.03f;
        public ChoiceSaveData TempTalkData { get;private set; }

        private TalkDataListSO _currentTalkData;
        
        public event Action OnEndTextEvnet;

        private int _talkIndex; //대화 char 인덱스
        private int _talkValueIndex; //대화 리스트 인덱스

        private string _targetText;
        private string _talkEndActionValue;

        public bool IsChoice {  get; private set; }
        public bool IsStartChoice {  get; set; }
        private bool _isNotSpeedText = true;
        private bool _isNotStopTaTalkIndex;
        private bool _isStartTalk;
        private bool _isPopUpTalk;
        private bool _isActionValue;

        private WaitForSeconds _currentdWait;
        private WaitForSeconds _speedWait;
        private WaitForSeconds _defaultWait;

        public Vector3 GetPopMaskValue(bool isPopUp) => isPopUp ? popUpMaskValue : Vector3.zero;

        public float GetPopTime() => popTime;

        public CharacterData GetCharacterData(DirType type)
        {
            if (type == DirType.Left)
                return _currentTalkData.leftCharacter;
            else
                return _currentTalkData.rightCharacter;
        }

        public override TalkData InitModel()
        {
            if(TempTalkData == null)
            {
                TempTalkData = new ChoiceSaveData();
                TempTalkData.currentTalkData = new TalkDataListSO();
                TempTalkData.currentTalkData.talkDataList = new List<TalkValueData>();
            }

            _currentData.Name = "";
            _currentData.TaklValue = "";

            _defaultWait = new WaitForSeconds(defaultWaitTime);
            _speedWait = new WaitForSeconds(defaultspeedWaitTime);

            _talkIndex = -1;

            SetCurrentdWait(false);

            return _currentData;
        }
        public void SetTalkEndActionValue(string  actionValue) => _talkEndActionValue = actionValue;
        public void SetIsPopUpTalk(bool isPopUpTalk) => _isPopUpTalk = isPopUpTalk;
        public void SetIsActionValue(bool isActionValue) => _isActionValue = isActionValue;
        public string GetTalkEndActionValue() => _talkEndActionValue;
        public bool GetIsPopUpTalk() => _isPopUpTalk;
        public bool GetIsStartTalk() => _isStartTalk;
        public bool GetIsActionValue() => _isActionValue;
        public void Choice(bool isOn)
        {
            IsChoice = isOn;
            Controller.PopUpPanel(isOn);
        }
        public void SetCurrentTalkData(TalkDataListSO talkDataSO)
        {
            OnEndTextEvnet = null;
            _talkValueIndex = -1;
            _talkIndex = -1;
            _targetText = "";
            _isStartTalk = true;
            _isPopUpTalk = true;
            _currentTalkData = talkDataSO;
        }

        public bool TextTalk()
        {
            if (_talkValueIndex + 1 >= _currentTalkData.talkDataList.Count)
            {
                _isStartTalk = false;
                return false;
            }

            _talkValueIndex++;
            _talkIndex = -1;

            SetCurrentdWait(false);
            _isNotSpeedText = false;

            _currentData.type = _currentTalkData.talkDataList[_talkValueIndex].Type;
            _currentData.TaklValue = "";
            _currentData.Name = "<" + GetCharacterData(_currentData.type).Name + ">";
            _currentData.IsUpLeft = _currentData.type == DirType.Left;

            _targetText = _currentTalkData.talkDataList[_talkValueIndex].Text;
            return true;
        }

        public bool TextTalkIndexValue()
        {
            if (_talkIndex + 1 >= _targetText.Length)
            {
                _isNotStopTaTalkIndex = false;
                OnEndTextEvnet?.Invoke();
                if (_talkValueIndex > -1 &&_currentTalkData.talkDataList[_talkValueIndex].choiceData != null)
                    Choice(true);
            }
            else
            {
                _isNotStopTaTalkIndex = true;
                _currentData.TaklValue += _targetText[++_talkIndex];
            }
            return _isNotStopTaTalkIndex;
        }
        public bool Click()
        {
            if (_isStartTalk == false) return false;
            else if(_isNotSpeedText && _isNotStopTaTalkIndex == false)
                return TextTalk();
            else if (_isNotSpeedText == false)
            {
                _isNotSpeedText = true;
                SetCurrentdWait(true);
            }
            
            return true; 
        }
        public int GetTalkValueIndex() => _talkValueIndex;
        public TalkValueData GetCurrentTalkValue() => _currentTalkData.talkDataList[_talkValueIndex];
        public TalkDataListSO GetCurrentTalkData() => _currentTalkData;
        public WaitForSeconds GetCurrentdWait() => _currentdWait;
        public void SetCurrentdWait(bool isSpeedText) => _currentdWait = isSpeedText ? _speedWait : _defaultWait;
    }

    public class ChoiceSaveData
    {
        public TalkDataListSO currentTalkData;
        public TalkDataListSO originalTalkData;

        public void SetCurrentTalkData(TalkDataListSO dataList) => currentTalkData = dataList;
        public void SetOriginalTalkData(TalkDataListSO dataList) => originalTalkData = dataList;
    }
}