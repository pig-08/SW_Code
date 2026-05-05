using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Work.PSB.Code.CoreSystem;

namespace PSW.Code.Setting
{
    public class ExitPanel : MonoBehaviour
    {
        [SerializeField] private TransitionController transition;
        [SerializeField] private List<ExitData> dataList;
        [SerializeField] private TMP_Dropdown dropdown;

        private int _currentIndex;
        private bool _isPopUp;

        private void Awake()
        {
            foreach (ExitData data in dataList)
                dropdown.options.Add(new TMP_Dropdown.OptionData(data.Text));

            _currentIndex = 0;
            SetOnOff(false);
        }

        private void Start()
        {
            dropdown.onValueChanged.AddListener((value) => _currentIndex = value);
        }

        private void OnDisable()
        {
            dropdown.onValueChanged.RemoveAllListeners();
        }

        public void SetOnOff(bool isOn)
        {
            _isPopUp = isOn;
            gameObject.SetActive(isOn);
        }

        public void SetOnOff()
        {
            _isPopUp = !_isPopUp;
            gameObject.SetActive(_isPopUp);
        }

        public void Exit()
        {
            if (dataList[_currentIndex].isGameExit)
                Application.Quit();
            else
                transition.Transition(dataList[_currentIndex].Value);
        }
    }

    [Serializable]
    public struct ExitData
    {
        public string Text;
        public string Value;
        public bool isGameExit;
    }
}