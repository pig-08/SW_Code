using PSW.Code.Dial.Slot;
using PSW.Code.Text;
using TMPro;
using UnityEngine;
using YIS.Code.Defines;

namespace PSW.Code.Deck
{
    public class DescriptionSkillIcon_View : SkillIconSetting_View
    {
        [SerializeField] private TextAnimator nameTextAnimator;
        [SerializeField] private TextAnimator descriptionTextAnimator;
        [SerializeField] private TextMeshProUGUI gradeText;

        public void SetUpText(string nameText, string descriptionText, Grade grade, bool isGrade)
        {
            nameTextAnimator.Appearance(nameText);
            descriptionTextAnimator.Appearance(descriptionText);
            gradeText.color = skillCircleDataList.GetOutLineColor(grade);
            if(isGrade)
                gradeText.SetText(grade.ToString());
            else
                gradeText.SetText("");

        }
    }
}