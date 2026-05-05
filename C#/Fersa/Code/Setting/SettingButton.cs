using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PSW.Code.Setting
{
    public class SettingButton : MonoBehaviour
    {
        [SerializeField] private Image imageButton;
        [SerializeField] private TextMeshProUGUI text;

        [SerializeField] private SpriteData buttonSprite;

        private string _buttonValue;
        private Button _currentButton;

        private Action<string> _setCurrentSettingEvnet;

        public void ButtonInit(string buttonValue, Action<string> settingEvent)
        {
            _buttonValue = buttonValue;
            text.SetText(_buttonValue);

            _currentButton = GetComponent<Button>();
            _setCurrentSettingEvnet = settingEvent;
            _currentButton.onClick.AddListener(ClickButton);
        }

        private void OnDisable()
        {
            _currentButton.onClick.RemoveAllListeners();
        }

        private void ClickButton()
        {
            _setCurrentSettingEvnet.Invoke(_buttonValue);
        }

        public void SetSelectBox(bool isPopUp)
        {
            imageButton.sprite = buttonSprite.GetSprite(isPopUp);
        }

    }
}