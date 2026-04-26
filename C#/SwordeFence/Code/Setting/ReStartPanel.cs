using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace SW.Code.Setting
{
    public class ReStartPanel : SettingComponent
    {
        private Button _yesButton;
        private Button _noButton;

        public override void Init(VisualElement root)
        {
            base.Init(root);

            _yesButton = _panel.Q<Button>("YesButton");
            _yesButton.RegisterCallback<ClickEvent>((v) => SceneManager.LoadScene(SceneManager.GetActiveScene().name));

            _noButton = _panel.Q<Button>("NoButton");
            _noButton.RegisterCallback<ClickEvent>((v) => SetPanel());
        }

    }
}