using PSB.Code.CoreSystem.SaveSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyService : MonoBehaviour, ISaveable
{
    [field:SerializeField] public SaveId SaveId { get; private set; }

    [SerializeField] private KeybindingInputSO fieldInput;
    [SerializeField] private KeybindingInputSO battleInput;
    [SerializeField] private KeybindingInputSO uiInput;

    public string GetSaveData()
    {
        KeyListData collectionData = new KeyListData();

        collectionData.uiInputKeyList = GetInputDataList(uiInput);
        collectionData.fieldInputKeyList = GetInputDataList(fieldInput);
        collectionData.battleInputKeyList = GetInputDataList(battleInput);

        return JsonUtility.ToJson(collectionData);
    }

    public void RestoreSaveData(string saveData)
    {
        if (!string.IsNullOrEmpty(saveData))
        {
            KeyListData keyList = JsonUtility.FromJson<KeyListData>(saveData);
            fieldInput.SetKeySaveGet(keyList.fieldInputKeyList);
            battleInput.SetKeySaveGet(keyList.battleInputKeyList);
            uiInput.SetKeySaveGet(keyList.uiInputKeyList);
        }
    }

    private List<KeyData> GetInputDataList(KeybindingInputSO inputSO)
    {
        List <KeyData> tempList = new List<KeyData>();
        foreach (InputAction action in inputSO.GetActions())
        {
            if (action.bindings.Count > 1)
            {
                for (int i = 1; i < action.bindings.Count; i++)
                {
                    tempList.Add(new KeyData(action.name,i,action.bindings[i].effectivePath));
                    Debug.Log($"{action.bindings[i].effectivePath} 이거ㅓㅓ");
                }
            }
            else
            {
                tempList.Add(new KeyData(action.name,action.bindings[0].effectivePath));
                Debug.Log($"{action.bindings[0].effectivePath} 이거ㅓㅓ");
            }
        }
        return tempList;
    }
}

[Serializable]
public struct KeyListData
{
    public List<KeyData> fieldInputKeyList;
    public List<KeyData> battleInputKeyList;
    public List<KeyData> uiInputKeyList;
}

[Serializable]
public struct KeyData
{
    public KeyData(string keyNameInit, string keyValueInit)
    {
        keyName = keyNameInit;
        keyIndex = -1;
        keyValue = keyValueInit;
    }
    public KeyData(string keyNameInit,int keyIndexInit, string keyValueInit)
    {
        keyName = keyNameInit;
        keyIndex = keyIndexInit;
        keyValue = keyValueInit;
    }

    public string keyName;
    public int keyIndex;
    public string keyValue;
}

