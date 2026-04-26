using SW.Code.SO;
using UnityEngine;
using UnityEngine.UIElements;

namespace SW.Code.Main
{
    public class ExitPanel : MainComponent
    {
        private Button _yesButton;
        [SerializeField] private UiInputSO uiInputSO;
        public override void Init(VisualElement root)
        {
            base.Init(root);
            _yesButton = _panel.Q<Button>("YesButton");
            _yesButton.RegisterCallback<ClickEvent>(GameEnd);
        }

        private void GameEnd(ClickEvent evt) => Application.Quit();
        private void GameEnd() => Application.Quit();

        public override void Open()
        {
            base.Open();
            uiInputSO.OnChoicePressed += GameEnd;
        }
        public override void Close()
        {
            base.Close();
            uiInputSO.OnChoicePressed -= GameEnd;
        }

        private void OnDisable()
        {
            _yesButton.UnregisterCallback<ClickEvent>(GameEnd);
        }
    }
}