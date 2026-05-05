using DG.Tweening;
using PSW.Code.Dial;
using System.Threading.Tasks;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.UI;
using YIS.Code.Skills;

namespace PSW.Code.Dial.Slot
{
    public class SkillIconSetting_Controller: MonoBehaviour
    {
        [SerializeField] protected SkillIconSetting_Model model;
        [SerializeField] protected SkillIconSetting_View view;
        public virtual void SetSkill(SkillDataSO skillData)
        {
            if (skillData == null)
                return;

            if (skillData.visualData.icon != null)
                view.SetIcon(skillData.visualData.icon);
            view.SetOutLineColor(skillData.grade);
        }

        public float GetPopTime() => model.GetPopTime();

        public virtual async void PopUp()
        {
            if (this == null) return;

            model.SetPopMove(true);
            model.SetPopUp();
            view.PopUp(model.GetSize(), model.GetRotation(), GetPopTime());
            await Awaitable.WaitForSecondsAsync(GetPopTime());
            model.SetPopMove(false);
        }

        public virtual async void PopUp(bool isPopUp)
        {
            if (this == null) return;

            model.SetPopMove(true);
            model.SetPopUp(isPopUp);
            view.PopUp(model.GetSize(), model.GetRotation(), GetPopTime());
            await Awaitable.WaitForSecondsAsync(GetPopTime());
            model.SetPopMove(false);
        }
    }
}