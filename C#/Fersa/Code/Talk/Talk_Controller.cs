using PSW.Code.BaseSystem;
using PSW.Code.Input;
using System;
using System.Collections;
using UnityEngine;

namespace PSW.Code.Talk
{
    public class Talk_Controller : BaseOnOffSystemUI
    {
        [SerializeField] private Talk_Model model;
        [SerializeField] private Talk_View view;

        [SerializeField] private UiInputSO uiInputSO;

        public event Action OnTalkClosed;
        public event Action<string> OnChangeAction;
        
        private void Awake()
        {
            view.Init(model.InitModel());
            uiInputSO.OnLeftButtonPressrd += TalkTextClick;
        }

        private void OnDestroy()
        {
            uiInputSO.OnLeftButtonPressrd -= TalkTextClick;
        }

        public void StartTalk(TalkDataListSO talkDataSO, bool isForce = false)
        {
            if (isForce == false)
            {
                if (IsOnPopUp() == false) return;
            }

            model.SetCurrentTalkData(talkDataSO);
            view.PopMask(model.GetPopMaskValue(true), model.GetPopTime());
            for (int i = (int)DirType.Left; i < (int)DirType.End; ++i)
            {
                DirType tempType = (DirType)i;
                view.SetCharacter(model.GetCharacterData(tempType), tempType);
            }

            TalkTextClick();
        }

        private void AddButton()
        {
            model.Controller.NewButton(model.GetCurrentTalkValue().
                choiceData.choiceTextList, ChoicButtonClick);
        }

        private void ChoicButtonClick(int index)
        {
            model.Choice(false);
            model.IsStartChoice = false;
            ChoiceData data = model.GetCurrentTalkValue().choiceData.choiceTextList[index];

            if (data.isChangeAction)
            {
                model.SetTalkEndActionValue(data.newActionValue);
                model.SetIsActionValue(true);
            }

            TalkValueData tempData = new TalkValueData();
            tempData.Type = model.GetCurrentTalkValue().choiceData.type;
            tempData.Text = data.choiceText;

            SetNextTalk(data.isRandom == false ? data.nextTalkData : data.GetRandom(), tempData);
        }

        private void SetNextTalk(TalkDataListSO data, TalkValueData talkValue)
        {
            ChoiceSaveData choiceSave = model.TempTalkData;
            
            choiceSave.SetOriginalTalkData(data);

            choiceSave.currentTalkData.rightCharacter = data.rightCharacter;
            choiceSave.currentTalkData.leftCharacter = data.leftCharacter;
            choiceSave.currentTalkData.talkDataList.Clear();

            foreach (TalkValueData talkValueData in data.talkDataList)
                choiceSave.currentTalkData.talkDataList.Add(talkValueData);

            choiceSave.currentTalkData.talkDataList.Insert(0, talkValue);

            StartTalk(choiceSave.currentTalkData, true);
        }
        private void TalkTextClick()
        {
            if (model.IsChoice)
            {
                if (model.IsStartChoice == false)
                {
                    AddButton();
                    model.IsStartChoice = true;
                }
                return;
            }
            else if (model.Click())
                Click();
            else if (model.GetIsStartTalk() == false && model.GetIsPopUpTalk())
                PopDown();
        }

        public override void PopDown()
        {
            base.PopDown();

            if (model.GetIsActionValue())
                OnChangeAction?.Invoke(model.GetTalkEndActionValue());

            model.SetIsActionValue(false);
            model.SetTalkEndActionValue("");
            model.SetIsPopUpTalk(false);
            view.PopMask(model.GetPopMaskValue(false), model.GetPopTime(), 0.5f);
            view.PopDownTalk();
            OnTalkClosed?.Invoke();
        }
        private void Click()
        {
            StopAllCoroutines();
            view.SetData(model.GetModelData());
            model.TextTalkIndexValue();
            StartCoroutine(TalkValueTextVisual());
        }
        private IEnumerator TalkValueTextVisual()
        {
            view.SetTaklValue(model.GetModelData().TaklValue);

            yield return model.GetCurrentdWait();

            if (model.TextTalkIndexValue())
                StartCoroutine(TalkValueTextVisual());
        }

    }
}