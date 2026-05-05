using PSW.Code.Dial;
using UnityEngine;
using UnityEngine.UI;
using YIS.Code.Defines;

namespace PSW.Code.Dial.Slot
{
    public class SlotEffect : MonoBehaviour
    {
        [SerializeField] private SkillCircleDataListSO skillCircleDataList;
        [SerializeField] private Animator effectAnim;
        [SerializeField] private Image effectImage;

        public void PlayEffect(Grade grade)
        {
            transform.SetSiblingIndex(transform.parent.childCount - 1);
            effectImage.color = skillCircleDataList.GetOutLineColor(grade);
            effectAnim.Play("InstallationEffect");
        }
    }
}