using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PSW.Code.Input
{
    [CreateAssetMenu(fileName = "UiInput", menuName = "SO/Input/UI")]
    public class UiInputSO : KeybindingInputSO, UiInput.IUiActions
    {
        public bool IsArrow { get; private set; }
        public bool IsLeft { get; private set; }

        public event Action<UIType> OnUIPressrd;
        public event Action OnChoicePressrd;
        public event Action OnLeftButtonPressrd;

        private UiInput _controls;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new UiInput();
                _controls.Ui.SetCallbacks(this);
            }
            _controls.Ui.Enable();
        }
        public override InputActionAsset GetActionAsset() => _controls.asset;
        public override void StartRebinding(string actionName, int bindingIndex = -1) => StartRebinding(_controls.asset, actionName, bindingIndex);
        public override List<InputAction> GetActions()
        {
            return _controls.asset.actionMaps[0].actions.ToList();
        }
        private void OnDisable()
        {
            _controls.Ui.Disable();
        }
        public void OnNextText(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnLeftButtonPressrd?.Invoke();
        }
        public void OnChoice(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnChoicePressrd?.Invoke();
        }
        public void OnDeck(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnUIPressrd?.Invoke(UIType.Deck);
        }

        public void OnSetting(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnUIPressrd?.Invoke(UIType.Setting);
        }

        public void OnBook(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnUIPressrd?.Invoke(UIType.Book);
        }
    }
}