using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SW.Code.SO
{
    [CreateAssetMenu(fileName = "UiInputSO", menuName = "SO/Input/Ui")]
    public class UiInputSO : ScriptableObject, UiController.IUiActions
    {
        public event Action OnOpenPressed;
        public event Action OnSkipPressed;
        public event Action OnChoicePressed;
        public event Action OnMousePressed;

        public float UpDownValue { get; private set; }


        private UiController _controller;

        private void OnEnable()
        {
            if (_controller == null)
            {
                _controller = new UiController();
                _controller.Ui.SetCallbacks(this);
            }
            _controller.Ui.Enable();
        }

        private void OnDisable()
        {
            _controller.Ui.Disable();
        }

        public void SetUIInput(bool isEnable)
        {
            if (isEnable)
                _controller.Ui.Enable();
            else
                _controller.Ui.Disable();
        }

        public void OnOpen(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnOpenPressed?.Invoke();
        }

        public void OnSkip(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSkipPressed?.Invoke();
        }

        public void OnUpDown(InputAction.CallbackContext context)
        {
            UpDownValue = context.ReadValue<Vector2>().y;
        }

        public void OnChoice(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnChoicePressed?.Invoke();
        }

        public void OnMouseClick(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnMousePressed?.Invoke();
        }
    }
}