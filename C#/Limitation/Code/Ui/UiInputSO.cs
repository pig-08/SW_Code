using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

[CreateAssetMenu( fileName = "UiInputSO",menuName ="SO/Input/UiInput")]
public class UiInputSO : ScriptableObject, UiActions.IUIActions
{
    private UiActions _controller;
    public Action OnESCEvent;

    public void OnEnable()
    {
        if (_controller == null)
        {
            _controller = new UiActions();
            _controller.UI.SetCallbacks(this);
        }
        _controller.UI.Enable();
    }

    private void OnDisable()
    {
        _controller.UI.Disable();
    }


    public void OnSetting(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnESCEvent?.Invoke();
    }
}
