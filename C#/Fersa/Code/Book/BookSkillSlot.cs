using PSW.Code.Dial;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YIS.Code.Skills;

namespace PSW.Code.Book
{
    public class BookSkillSlot : BookSlotCompo
    {
        [SerializeField] private Image outLine;
        [SerializeField] private Image lockImage;
        [SerializeField] protected SkillCircleDataListSO circleDataList;

        public override void Init<T>(T data)
        {
            base.Init(data);
            SkillDataSO skillDataSO = data as SkillDataSO;

            if (skillDataSO == null)
                return;

            _description = skillDataSO.visualData.itemDescription;
            SetIcon(skillDataSO.visualData.icon);
            outLine.color = circleDataList.GetOutLineColor(skillDataSO.grade);
        }

        public override void SetLock(bool isLock)
        {
            base.SetLock(isLock);
            lockImage.gameObject.SetActive(isLock);
        }

    }
}