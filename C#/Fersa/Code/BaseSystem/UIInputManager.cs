using CIW.Code.Player.Field;
using PSB.Code.BattleCode.Players;
using PSW.Code.EventBus;
using PSW.Code.Input;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PSW.Code.BaseSystem
{
    public class UIInputManager : MonoBehaviour
    {
        [SerializeField] private UiInputSO uiInput;
        [SerializeField] private PlayerInputSO playerInput;
        [SerializeField] private PlayerFieldInputSO fieldInput;


        private Dictionary<UIType, BaseOnOffSystemUI> _onOffUiDic = new Dictionary<UIType, BaseOnOffSystemUI>();
        private void Awake()
        {
            _onOffUiDic = GetComponentsInChildren<BaseOnOffSystemUI>().ToDictionary(t => t.GetThisType());
            uiInput.OnUIPressrd += OpenPanel;
            playerInput.OnUIPressrd += OpenPanel;
            fieldInput.OnUIPressrd += OpenPanel;
            Bus<UIPressrdEvent>.OnEvent += OpenPanel;
            Bus<UIPopDownEvent>.OnEvent += PopDown;
        }

        private void OnDisable()
        {
            uiInput.OnUIPressrd -= OpenPanel;
            playerInput.OnUIPressrd -= OpenPanel;
            fieldInput.OnUIPressrd -= OpenPanel;
            Bus<UIPressrdEvent>.OnEvent -= OpenPanel;
            Bus<UIPopDownEvent>.OnEvent -= PopDown;
        }

        private void OpenPanel(UIType type)
        {
            if (_onOffUiDic.TryGetValue(type, out BaseOnOffSystemUI baseUI))
                baseUI.PopUp();
        }

        private void OpenPanel(UIPressrdEvent evt)
        {
            OpenPanel(evt.type);
        }

        private void PopDown(UIPopDownEvent evt)
        {
            if (_onOffUiDic.TryGetValue(evt.type, out BaseOnOffSystemUI baseUI))
                baseUI.PopDown();
        }
    }

    public struct UIPressrdEvent : IEvent
    {
        public UIPressrdEvent(UIType uIType)
        {
            type = uIType;
        }
        public UIType type;
    }

    public struct UIPopDownEvent : IEvent
    {
        public UIPopDownEvent(UIType uIType)
        {
            type = uIType;
        }
        public UIType type;
    }
}