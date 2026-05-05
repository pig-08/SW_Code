using CIW.Code.Player.Field;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using YIS.Code.Skills;

public class TestSetPanel : MonoBehaviour
{
    [SerializeField] private KeybindingInputSO moveAction;
    [SerializeField] private InputActionReference inputAction;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
            moveAction.StartRebinding(inputAction.action.name);
    }
}
