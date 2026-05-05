using System;
using System.Collections.Generic;
using UnityEngine;

namespace PSW.Code.Talk
{
    public class ChoicePanel_Controller : MonoBehaviour
    {
        [SerializeField] private ChoicePanel_Model model;
        [SerializeField] private ChoicePanel_View view;

        public void PopUpPanel(bool isOn)
        {
            view.OnOffPanel(isOn);
        }

        public void AllButtonOff()
        {
            for (int i = 0; i < model.choiceButtonList.Count; ++i)
                model.choiceButtonList[i].gameObject.SetActive(false);
        }

        public void NewButton(List<ChoiceData> dataList, Action<int> choiceEvent)
        {
            for (int i = 0; i < model.choiceButtonList.Count; ++i)
            {
                if(i < dataList.Count)
                {
                    model.choiceButtonList[i].SetUpButton(dataList[i].choiceText, i);
                    model.choiceButtonList[i].gameObject.SetActive(true);
                }
            }

            for (int i = model.choiceButtonList.Count; i < dataList.Count; ++i)
            {
                model.choiceButtonList.Add(
                    Instantiate(model.GetPrefab(),
                    model.GetButtonPanelTrm()).GetComponent<ChoiceButton>());
                model.choiceButtonList[i].Init(choiceEvent, AllButtonOff);
                model.choiceButtonList[i].SetUpButton(dataList[i].choiceText, i);
            }
        }
    }
}