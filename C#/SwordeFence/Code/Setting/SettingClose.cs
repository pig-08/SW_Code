using UnityEngine;
using UnityEngine.UIElements;

namespace SW.Code.Setting
{
    public class SettingClose : SettingComponent
    {
        public override void Init(VisualElement root)
        {
            base.Init(root);
            _panel = _root.Q<VisualElement>("ChoicePanel");
        }

        public override void SetPanel()
        {
        }
    }
}