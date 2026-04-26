using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SW.Code.UpGrade
{
    public class UpGradeUiPlayer : MonoBehaviour
    {
        private int _buttonIndexCount = 0;
        private UIDocument _uiPanel;

        private VisualElement _root;
        private VisualElement _panel;

        private Button[] _statButtonList = new Button[3];
        private List<Label> _statTextList = new List<Label>();

        public void Init()
        {
            _uiPanel = GetComponent<UIDocument>();

            _root = _uiPanel.rootVisualElement;

            _panel = _root.Q<VisualElement>("RandomPanel");

            for (int i = 0; i < _statButtonList.Length; ++i)
                _statButtonList[i] = _root.Q<Button>("ButtonPanel" + i.ToString());

            for (int i = 0; i < 3; ++i)
            {
                Label tempTextList = _statButtonList[i].Q<Label>("StatText");
                _statTextList.Add(tempTextList);
            }
        }

        public async void Open(string[] statIncrease)
        {
            _panel.ToggleInClassList("Open");
            await Awaitable.WaitForSecondsAsync(0.5f);

            foreach (Label text in _statTextList)
            {
                ScrollText(text, statIncrease);
                await Awaitable.WaitForSecondsAsync(0.5f);
            }
        }

        public void Close()
        {
            _buttonIndexCount = 0;
            _panel.ToggleInClassList("Open");
            foreach (Label text in _statTextList)
                text.ToggleInClassList("Up");
            foreach (Button button in _statButtonList)
                button.ToggleInClassList("NotChoice");
        }


        private async void ScrollText(Label text, string[] statIncrease)
        {
            int index = 0;
            for (int i = 0; i < 30; ++i)
            {

                await Awaitable.WaitForSecondsAsync(0.03f);
                text.ToggleInClassList("Plus");

                await Awaitable.WaitForSecondsAsync(0.03f);
                text.ToggleInClassList("Plus");
                text.ToggleInClassList("Up");

                index = Random.Range(0, statIncrease.Length);
                text.text = statIncrease[index];
            }
            _statButtonList[_buttonIndexCount++].ToggleInClassList("NotChoice");
            text.ToggleInClassList("Up");

        }



    }
}