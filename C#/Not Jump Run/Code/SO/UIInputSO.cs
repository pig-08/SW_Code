using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "UiInput", menuName = "SO/Input/Ui")]
public class UIInputSO : ScriptableObject, UiControls.IUiActions
{
    public event Action OnTapPressed;
    public event Action OnESCPressed;

    private UiControls controls;

    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new UiControls();
            controls.Ui.SetCallbacks(this);
        }
        controls.Ui.Enable();
    }

    private void OnDisable()
    {
        controls.Ui.Disable();
    }

    public void SetUIInput(bool isEnable)
    {
        if (isEnable)
            controls.Ui.Enable();
        else
            controls.Ui.Disable();
    }

    public void OnTap(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnTapPressed?.Invoke();
    }

    public void OnESC(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnESCPressed?.Invoke();
    }
}
