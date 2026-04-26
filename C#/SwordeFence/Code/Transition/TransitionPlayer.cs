using Gwamegi.Code.Input;
using Gwamegi.Code.Managers;
using SW.Code.SO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TransitionPlayer : MonoBehaviour, IManager
{
    [SerializeField] private UIDocument uiPanel;
    [SerializeField] private GameInputSO inputSO;
    [SerializeField] private UiInputSO uiInputSO;

    private VisualElement _root;
    private VisualElement[] _transitionBoxList = new VisualElement[6];

    public void Initailze(Manager manager)
    {
        _root = uiPanel.rootVisualElement;

        for (int i = 0; i < _transitionBoxList.Length; ++i)
        {
            _transitionBoxList[i] = _root.Q<VisualElement>("TransitionBox" + i);
        }
        Close();
    }

    private async void Transition()
    {
        foreach (VisualElement box in _transitionBoxList)
        {
            box.ToggleInClassList("Up");
            await Awaitable.WaitForSecondsAsync(0.2f);
        }
    }

    public async void Close()
    {
        inputSO.SetPlayerInput(false);
        inputSO.SetUIInput(false);
        uiInputSO.SetUIInput(false);
        await Awaitable.WaitForSecondsAsync(0.2f);
        Transition();
        await Awaitable.WaitForSecondsAsync(0.2f * _transitionBoxList.Length);
        inputSO.SetPlayerInput(true);
        inputSO.SetUIInput(true);
        uiInputSO.SetUIInput(true);
    }

    public async void Open(string moveScene)
    {
        Transition();
        await Awaitable.WaitForSecondsAsync(0.3f * _transitionBoxList.Length);
        SceneManager.LoadScene(moveScene);
    }

    
    
}
