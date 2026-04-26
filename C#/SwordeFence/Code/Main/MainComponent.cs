using UnityEngine;
using UnityEngine.UIElements;

namespace SW.Code.Main
{
    public abstract class MainComponent : MonoBehaviour
    {
        [SerializeField] protected string mainName;
        [SerializeField] private int IndexId;

        protected VisualElement _root;
        protected VisualElement _panel;

        public bool IsOpen { get; protected set; }

        public virtual void Init(VisualElement root)
        {
            _root = root;
            _panel = _root.Q<VisualElement>(mainName + "Panel");
        }

        public int GetIndex() => IndexId;

        public virtual void Open()
        {
            if (IsOpen)
                return;

            _panel.BringToFront();
            SetPanel();
            IsOpen = true;
        }

        public virtual void Close()
        {
            if (IsOpen == false)
                return;

            _panel.SendToBack();
            SetPanel();
            IsOpen = false;
        }

        public void SetPanel()
        {
            _panel.ToggleInClassList("Open");
        }
    }
}