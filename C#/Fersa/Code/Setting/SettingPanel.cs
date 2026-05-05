using PSW.Code.BaseSystem;
using PSW.Code.Input;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PSW.Code.Setting
{
    public class SettingPanel : MonoBehaviour
    {
        [SerializeField] private ExitPanel exitPanel;
        [SerializeField] private List<SettingData> settingDataList;
        [SerializeField] private GameObject settingButtonPrefab;

        [SerializeField] private Transform buttonContent;
        [SerializeField] private Transform valuePanelContent;

        private Dictionary<string, (SettingButton, GameObject)> _buttonDic = new Dictionary<string, (SettingButton, GameObject)>();
        private (SettingButton, GameObject, string) _currentSettingData;

        private bool _isPopUp = false;

        private void Start()
        {
            foreach (SettingData setting in settingDataList)
            {
                SettingButton newButton = Instantiate(settingButtonPrefab, buttonContent)
                    .GetComponent<SettingButton>();
                GameObject panel = Instantiate(setting.SettingValuePrefab, valuePanelContent);

                panel.SetActive(false);
                newButton.ButtonInit(setting.Value, SetCurrentSetting);

                _buttonDic.Add(setting.Value, (newButton, panel));
            }

            SetCurrentSetting(settingDataList[0].Value);
        }

        public void SetCurrentSetting(string buttonValue)
        {
            if (_currentSettingData.Item3 == buttonValue)
                return;

            _currentSettingData.Item1?.SetSelectBox(false);
            _currentSettingData.Item2?.SetActive(false);

            _buttonDic.TryGetValue(buttonValue, out (SettingButton, GameObject) data);
            _currentSettingData = (data.Item1, data.Item2, buttonValue);

            data.Item1?.SetSelectBox(true);
            data.Item2.SetActive(true);
        }

    }

    [Serializable]
    public struct SettingData
    {
        public string Value;
        public GameObject SettingValuePrefab;
    }
}
