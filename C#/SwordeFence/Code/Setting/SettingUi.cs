using Gwamegi.Code.Managers;
using SW.Code.SO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace SW.Code.Setting
{
    public class SettingUi : MonoBehaviour
    {
        [SerializeField] private UIDocument uIPanel;
        [SerializeField] private UiInputSO uiInput;
        [SerializeField] private string[] nameList;
        [SerializeField] private SoundSo clickSound;
        [SerializeField] private SoundManager soundManager;

        private VisualElement _root;
        private VisualElement _choicePanel;

        private Dictionary<string, SettingComponent> _componentList = new Dictionary<string, SettingComponent>();
        private Button[] buttonList;


        private void Awake()
        {
            buttonList = new Button[nameList.Length];
            _root = uIPanel.rootVisualElement;
            _choicePanel = _root.Q<VisualElement>("ChoicePanel");
            uiInput.OnOpenPressed += SetSettingUi;

            for (int i = 0; i < nameList.Length; i++)
            {
                int index = i;
                buttonList[i] = _choicePanel.Q<Button>(nameList[i] + "Button");
                buttonList[i].RegisterCallback<ClickEvent>((v) => Click(buttonList[index], index));
            }

            AddComponentList();
            ComponentListInit();
        }

        private async void Click(Button button, int index)
        {
            button.ToggleInClassList("Big");
            await Awaitable.WaitForSecondsAsync(0.1f);
            button.ToggleInClassList("Big");
            SetSettingUi();

            soundManager.PlaySound(clickSound);
            _componentList.GetValueOrDefault(nameList[index]).SetPanel();
        }

        private void OnDisable()
        {
            uiInput.OnOpenPressed -= SetSettingUi;
        }

        private void SetSettingUi()
        {
            _componentList.Values.ToList().ForEach((v) =>
            {
                if (v.GetIsOpen())
                    v.SetPanel();
            });
            _choicePanel.ToggleInClassList("Open");
        }

        private void AddComponentList()
        {
            GetComponentsInChildren<SettingComponent>().ToList().ForEach((v) => _componentList.Add(v.GetPanelName(), v));
        }

        private void ComponentListInit()
        {
            _componentList.Values.ToList().ForEach((v) => v.Init(_root));
        }


    }
}