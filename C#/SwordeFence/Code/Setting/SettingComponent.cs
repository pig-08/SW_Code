using UnityEngine;
using UnityEngine.UIElements;

namespace SW.Code.Setting
{
    public abstract class SettingComponent : MonoBehaviour
    {
        [SerializeField] protected string panelName;
        protected VisualElement _root;
        protected VisualElement _panel;

        protected bool _isOpen;

        public virtual void Init(VisualElement root)
        {
            _root = root;
            _panel = _root.Q<VisualElement>(panelName + "Panel");
        }

        public string GetPanelName() => panelName;

        public bool GetIsOpen() => _isOpen;

        public virtual void SetPanel()
        {
            _panel.ToggleInClassList("Open");
            _isOpen = !_isOpen;
        }
    }
}