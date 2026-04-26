using Gwamegi.Code.Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace SW.Code.Setting
{
    public class TiltlePopUp : SettingComponent
    {
        [SerializeField] private string tiltleName;
        private Label _descriptionText;

        private Button yesButton;
        private Button noButton;

        public override void Init(VisualElement root)
        {
            base.Init(root);

            _descriptionText = _panel.Q<Label>("DescriptionText");
            _descriptionText.text = "타이틀으로 이동하시겠습니까?";

            yesButton = _panel.Q<Button>("YesButton");
            yesButton.RegisterCallback<ClickEvent>((v) => Manager.GetCompoManager<TransitionPlayer>().Open(tiltleName));

            noButton = _panel.Q<Button>("NoButton");
            noButton.RegisterCallback<ClickEvent>((v) => SetPanel());
        }

        public override void SetPanel()
        {
            base.SetPanel();
        }
    }
}