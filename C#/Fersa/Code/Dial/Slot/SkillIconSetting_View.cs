using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using YIS.Code.Defines;

namespace PSW.Code.Dial.Slot
{
    public class SkillIconSetting_View : MonoBehaviour, IView<SizeData>
    {
        [Header("Icon(Parents)")]
        [SerializeField] protected SkillCircleDataListSO skillCircleDataList;
        [SerializeField] protected Image outLine;
        [SerializeField] protected Image icon;

        public virtual void Init(SizeData defaultData) { }
        public virtual void SetData(SizeData data) { }
        public void SetIcon(Sprite sprite) => icon.sprite = sprite;
        public void SetOutLineColor(Grade grade) => outLine.color = skillCircleDataList.GetOutLineColor(grade);
        public virtual void PopUp(Vector3 size, Vector3 rotation, float popTime)
        {
            transform.DOLocalRotate(rotation, popTime);
            transform.DOScale(size, popTime);
        }
    }
}