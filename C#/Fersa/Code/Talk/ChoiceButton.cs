using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private Button currentButton;
    [SerializeField] private TextMeshProUGUI buttonText;

    private Action<int> OnButtonClickEvent;

    private int _currentIndex;

    public void Init(Action<int> clickEvent, UnityAction buttonOffEvent)
    {
        OnButtonClickEvent = clickEvent;
        currentButton.onClick.AddListener(ButtonClick);
        currentButton.onClick.AddListener(buttonOffEvent);
    }

    private void OnDestroy()
    {
        currentButton.onClick.RemoveAllListeners();
    }

    public void SetUpButton(string text, int index)
    {
        buttonText.SetText(text);
        _currentIndex = index;
    }

    public void ButtonClick()
    {
        OnButtonClickEvent?.Invoke(_currentIndex);
    }
}
