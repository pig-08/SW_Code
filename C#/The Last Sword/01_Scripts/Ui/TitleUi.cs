using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleUi : MonoBehaviour
{
    private UIDocument _panel;
    private VisualElement _root;
    private List<Button> _buttons;
    [SerializeField] private string moveSceneName;

    private void Awake()
    {
        _buttons = new List<Button>();
        _panel = GetComponent<UIDocument>();
        _root = _panel.rootVisualElement;
        _buttons.Add(_root.Q<Button>("PlayButton"));
        _buttons.Add(_root.Q<Button>("ExitButton"));
        _buttons.Add(_root.Q<Button>("TutorialButton"));
        _buttons.Add(_root.Q<Button>("InfiniteButton"));
        print(_buttons[0].name);
        foreach (var button in _buttons)
            button.RegisterCallback<ClickEvent>((v) => StartCoroutine(Click(button,button.name == _buttons[0].name)));
    }

    private IEnumerator Click(Button button,bool isPlay)
    {
        button.ToggleInClassList("Big");
        yield return new WaitForSeconds(0.15f);
        button.ToggleInClassList("Big");
        yield return new WaitForSeconds(0.2f);
        if (isPlay)
            SceneManager.LoadScene(moveSceneName);
        else if (button.text == "Tutorial")
            SceneManager.LoadScene("Tutorial");
        else if (button.text == "Infinite")
            SceneManager.LoadScene("Gwa");
        else
            Application.Quit();
    }
}
