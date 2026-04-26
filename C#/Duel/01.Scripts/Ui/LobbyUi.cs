using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class LobbyUi : MonoBehaviour
{
    private UIDocument _uIDocument;
    private VisualElement _root;
    private Button[] _buttons = new Button[2];

    private void Awake()
    {
        _uIDocument = GetComponent<UIDocument>();
        _root = _uIDocument.rootVisualElement;
        _buttons[0] = _root.Q<Button>("Play");
        _buttons[1] = _root.Q<Button>("Title");
        _buttons[0].RegisterCallback<ClickEvent>((v) =>
        {
            GameManager.Instance.ResetGame();
            SceneMove(2);
        });
        _buttons[1].RegisterCallback<ClickEvent>((v) =>
        {
            GameManager.Instance.ResetGame();
            SceneMove(0);
        });
        GameManager.Instance.OnSettingUi += SettingOn;
    }

    private void SceneMove(int value)
    {
        print(value);
        GameManager.Instance.OnFadeIn(value);
    }

    public void SettingOn(bool isOpen)
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            _uIDocument.sortingOrder = 1;
        }
        else
        {
            _uIDocument.sortingOrder = -1;
        }
        print(isOpen);
    }
}
