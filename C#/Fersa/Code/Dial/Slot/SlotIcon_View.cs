using DG.Tweening;
using PSW.Code.Dial.Slot;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotIcon_View : SkillIconSetting_View
{
    [SerializeField] protected RectTransform rectTransform;
    [SerializeField] private RectTransform selectBoxRTrm;

    [SerializeField] private List<Animator> nextChainEffectAnimatorList;
    
    [SerializeField] private Image nextChainEffect;
    [SerializeField] private Image coolTimeImage;
    
    public void AddAnimatorList(Animator newAnimator)
    {
        nextChainEffectAnimatorList.Add(newAnimator);
    }
    public void DOTweenAllKill()
    {
        rectTransform.DOKill();
        transform.DOKill();
    }
    public void SetSelectBoxSize(Vector3 size, float time) => selectBoxRTrm.DOScale(size, time);
    public void SetCoolTime(float fill, float time)
    {
        if (coolTimeImage == null) return;

        coolTimeImage.DOFillAmount(fill, time);
    }
    public void PlayNextChainEffect(string animName)
    {
        foreach (Animator anim in nextChainEffectAnimatorList)
            anim.Play(animName, 0, 0);
    }
}
