using DG.Tweening;
using PSW.Code.Dial;
using PSW.Code.Dial.Slot;
using UnityEngine;
using UnityEngine.UI;
using YIS.Code.Defines;

public class SkillIcon_Circle_View : SkillIconSetting_View
{
    [SerializeField] private Image choiceEffectImage;

    [SerializeField] protected SkillCircleDataListSO effectDataList;
    [SerializeField] private Image mouseUpEffectImage;
    [SerializeField] private Animator mouseUpEffectAnimator;

    public override void Init(SizeData defaultData)
    {

    }

    public override void SetData(SizeData data)
    {

    }
    public void PlayMouseUpEffect(string animName) => mouseUpEffectAnimator.Play(animName, 0, 0);
    public void SetEffectColor(Grade grade) => mouseUpEffectImage.color = effectDataList.GetOutLineColor(grade);
    public void DOAllSkill() => transform.DOKill();
    public void SetSize(Vector3 size, float popTime) => transform.DOScale(size, popTime);
    public void SetChoiceImageFade(float fade, float time) => choiceEffectImage?.DOFade(fade, time);
}
