using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class KeybindingInputSO : ScriptableObject 
{
    public KeybindingInputSO[] KeybindingInputs = new KeybindingInputSO[3];

    public event Action<string, int, string>OnChangeKey;
    public event Action<string, int> OnFail;

    public abstract List<InputAction> GetActions();
    public abstract InputActionAsset GetActionAsset();
    public abstract void StartRebinding(string actionName, int bindingIndex = -1);
    protected void StartRebinding(InputActionAsset asset, string actionName, int bindingIndex = -1)
    {
        InputAction action = asset.FindAction(actionName);
        if (action == null) return;

        action.Disable();

        action.PerformInteractiveRebinding(bindingIndex)
            .OnPotentialMatch(operation => 
            {
                string candidatePath = operation.selectedControl.path;
                if (CheckDuplicate(candidatePath))
                {
                    OnFail?.Invoke(actionName, bindingIndex);
                    operation.Cancel();
                    action.Enable();
                }
            })
            .OnComplete(op => 
            {
                op.Dispose();
                action.Enable();
                OnChangeKey?.Invoke(actionName, bindingIndex, GetKeyValue(action, bindingIndex));
            })
            .Start();
    }

    private string GetKeyValue(InputAction action, int index)
    {
        if (index >= 0)
            return action.GetBindingDisplayString(index);
        else
            return action.GetBindingDisplayString();
    }

    private bool CheckDuplicate(string newPath)
    {
        return KeybindingInputs
        .SelectMany(so => so.GetActions())
        .Any(action => action.bindings.Any(binding =>
        {
            string key = binding.effectivePath;

            string newKey = Regex.Replace(newPath, @"[<>/]", "");
            string listKey = Regex.Replace(key, @"[<>/]", "");

            Debug.Log($"{newKey} {binding.effectivePath} ¿Ã∞≈§√§√");
            return newKey == listKey;
        }));
    }

    public void SetKeySaveGet(List<KeyData> keyDataList)
    {
        for(int i = 0; i < keyDataList.Count; i++)
        {
            InputAction action = GetActionAsset().FindAction(keyDataList[i].keyName);
            if (action == null) continue;

            action.Disable();

            action.ApplyBindingOverride(keyDataList[i].keyIndex >= 0
                ? keyDataList[i].keyIndex : 0, keyDataList[i].keyValue);

            action.Enable();
        }
    }
}
