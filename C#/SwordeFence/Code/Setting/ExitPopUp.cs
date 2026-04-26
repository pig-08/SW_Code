using UnityEngine;
using UnityEngine.UIElements;

namespace SW.Code.Setting
{
    public class ExitPopUp : SettingComponent
    {
        private Label _descriptionText;

        private Button yesButton;
        private Button noButton;
        public override void Init(VisualElement root)
        {
            base.Init(root);

            _descriptionText = _panel.Q<Label>("DescriptionText");
            _descriptionText.text = "게임을 종료하시겠습니까?";

            yesButton = _panel.Q<Button>("YesButton");
            yesButton.RegisterCallback<ClickEvent>((v) => Application.Quit());

            noButton = _panel.Q<Button>("NoButton");
            noButton.RegisterCallback<ClickEvent>((v) => SetPanel());
        }

        public override void SetPanel()
        {
            base.SetPanel();
        }
    }
}