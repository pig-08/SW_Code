using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInput",menuName ="SO/Input/Player")]
public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
{
    public event Action OnJumpPressed;
    public event Action<int> OnIntKeyPressed;

    public Vector2 MovementKey { get; private set; }
    public Vector2 MouseKey { get; private set; }

    private Controls controls;

    private void OnEnable()
    {
        if(controls == null)
        {
            controls = new Controls();
            controls.Player.SetCallbacks(this);
        }
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    public void SetPlayerInput(bool isEnable)
    {
        if (isEnable)
            controls.Player.Enable();
        else
            controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnJumpPressed?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementKey = context.ReadValue<Vector2>();
    }

    public void OnPointer(InputAction.CallbackContext context)
    {
        MouseKey = context.ReadValue<Vector2>();
    }

    public void On_1Key(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnIntKeyPressed?.Invoke(0);
    }

    public void On_2Key(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnIntKeyPressed?.Invoke(1);
    }

    public void On_3Key(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnIntKeyPressed?.Invoke(2);
    }

    public void On_4Key(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnIntKeyPressed?.Invoke(3);
    }
}
