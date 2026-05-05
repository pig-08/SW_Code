using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeybindingPanel : MonoBehaviour
{

    [SerializeField] private MainInputKey[] mainInputKeys = new MainInputKey[3];
    [SerializeField] private KeyTextChangeKoreaSO keyTextChangeKorea;
    [SerializeField] private GameObject tagPanelPrefab;
    [SerializeField] private GameObject titlePrefab;
    [SerializeField] private Transform panelPos;

    private void Awake()
    {
        foreach (MainInputKey key in mainInputKeys)
        {
            Instantiate(titlePrefab, panelPos).GetComponent<TextMeshProUGUI>().SetText(key.tagName);
            foreach (InputAction action in key.mainKeyInput.GetActions())
            {
                if (action.bindings.Count <= 1)
                {
                    KeyPanel panel = Instantiate(tagPanelPrefab, panelPos).GetComponent<KeyPanel>();
                    panel.Init(keyTextChangeKorea.GetChangeKoreaText(action.name)
                        , action, key.mainKeyInput);
                }
                else
                    CreateActionsPanel(action, key.mainKeyInput);
            }
        }
    }

    public void CreateActionsPanel(InputAction action, KeybindingInputSO inputSO)
    {
        for (int i = 1; i < action.bindings.Count; ++i)
        {
            KeyPanel panel = Instantiate(tagPanelPrefab, panelPos).GetComponent<KeyPanel>();
            panel.InitIndex(i);
            panel.Init($"{keyTextChangeKorea.GetChangeKoreaText(action.bindings[i].action)}" +
                $"/{keyTextChangeKorea.GetChangeKoreaText(action.bindings[i].name)}",
                action, inputSO);
        }
    }
}

[Serializable]
public struct MainInputKey
{
    public string tagName;
    public KeybindingInputSO mainKeyInput;
}