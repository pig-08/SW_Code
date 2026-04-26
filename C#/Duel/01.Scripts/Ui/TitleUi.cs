using UnityEngine;
using UnityEngine.UIElements;

public class TitleUi : MonoBehaviour
{
    private UIDocument _titlePenel;
    private VisualElement _root;
    private VisualElement _bottom;
    private Button[] buttons = new Button[2];

    private void Awake()
    {
        _titlePenel = GetComponent<UIDocument>();
        _root = _titlePenel.rootVisualElement;
        _bottom = _root.Q<VisualElement>("Bottom");
        buttons[0] = _bottom.Q<Button>("PlayButton");
        buttons[1] = _bottom.Q<Button>("ExitButton");

        _bottom[0].RegisterCallback<ClickEvent>((v) => GameManager.Instance.OnFadeIn(1));
        _bottom[1].RegisterCallback<ClickEvent>((v) => Application.Quit());
    }
}
