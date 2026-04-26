using AJ;
using SW.Code.Stat;
using UnityEngine;
using UnityEngine.UIElements;

namespace SW.Code.UpGrade
{
    public abstract class UpGradeComponent : MonoBehaviour
    {
        [SerializeField] protected string panelName;
        protected VisualElement _root;
        protected VisualElement _panel;

        protected bool isOpen;
        protected bool isAllOpen = true;

        public virtual void Init(VisualElement root)
        {
            _root = root;
            _panel = _root.Q<VisualElement>(panelName + "Panel");
        }

        public string GetPanelName() => panelName;

        public virtual void Open(TurretSO turretData, TurretStat turretStat) { SetPanel(); }

        public virtual void ClosePanel()
        {
            isOpen = false;
            SetPanel();
        }
        public virtual void SetPanel()
        {
            _panel.ToggleInClassList("Open");
        }
    }
}