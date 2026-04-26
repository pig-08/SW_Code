using UnityEngine;
using UnityEngine.UIElements;

public class ModeChoiceUi : MonoBehaviour
{
    private UIDocument _modeChoicePenel;
    private VisualElement _root;
    private VisualElement[] _panels = new VisualElement[2];
    private Button[] buttons = new Button[2];

    private void Awake()
    {
        _modeChoicePenel = GetComponent<UIDocument>();
        _root = _modeChoicePenel.rootVisualElement;
        _panels[0] = _root.Q<VisualElement>("MultiPanel");
        _panels[1] = _root.Q<VisualElement>("LocalPanel");
        buttons[0] = _panels[0].Q<Button>("MultiButton");
        buttons[1] = _panels[1].Q<Button>("LocalButton");

        buttons[0].RegisterCallback<ClickEvent>((var) => print("Multi"));
        buttons[1].RegisterCallback<ClickEvent>((var) => print("Local"));
    }
}
