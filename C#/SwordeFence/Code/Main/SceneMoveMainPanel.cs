using Gwamegi.Code.Managers;
using SW.Code.SO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace SW.Code.Main
{
    public class SceneMoveMainPanel : MainComponent
    {
        [SerializeField] private UiInputSO uiInputSO;
        [SerializeField] private string moveScene;
        private Button _yesButton;
        public override void Init(VisualElement root)
        {
            base.Init(root);
            _yesButton = _panel.Q<Button>("YesButton");
            _yesButton.RegisterCallback<ClickEvent>(MoveScene);
        }

        private void MoveScene(ClickEvent evt) => Manager.GetCompoManager<TransitionPlayer>().Open(moveScene);
        private void MoveScene() => SceneManager.LoadScene(moveScene);

        public override void Open()
        {
            base.Open();
            uiInputSO.OnChoicePressed += MoveScene;
        }

        public override void Close()
        {
            base.Close();
            uiInputSO.OnChoicePressed -= MoveScene;
        }

        private void OnDisable()
        {
            _yesButton.UnregisterCallback<ClickEvent>(MoveScene);
        }
    }
}