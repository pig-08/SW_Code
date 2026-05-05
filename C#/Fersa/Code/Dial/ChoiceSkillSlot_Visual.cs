using DG.Tweening;
using PSW.Code.Dial.Slot;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceSkillSlot_Visual : MonoBehaviour
{
    [Header("Image")]
    [SerializeField] private Image slotImage;
    [SerializeField] private Animator slotEffectAnimator;

    [Header("SpriteData")]
    [SerializeField] private SpriteData slotData;
    [SerializeField] private string slotEffectName;

    [SerializeField] private SlotEffect effect;
    [SerializeField] private List<Animator> chainAnimatorList;
    [SerializeField] private Image selectBox;

    [SerializeField] private float chainDelayTime = 1f;
    [SerializeField] private float chainMoveDelayTime = 0.2f;

    private string[] _animNmaes = { "ChainMaskUp", "ChainMaskDown" };

    public void SetSelectBox(bool isSelectBox)
    {
        Vector2 size = isSelectBox ? Vector2.one : new Vector2(1.2f, 1.2f);
        selectBox.transform.DOScale(size, chainMoveDelayTime);
        selectBox.DOFade(isSelectBox ? 1 : 0, chainMoveDelayTime);
    }

    public void ChainEffect(bool isBigCurrentIndex, Action callBack = null)
    {
        int animIndex = isBigCurrentIndex ? 1 : 0;

        for (int i = 0; i < chainAnimatorList.Count; i++)
            chainAnimatorList[i].Play(_animNmaes[animIndex], 0, 0);

        ChainEffectEnd(animIndex, callBack);
    }


    private async void ChainEffectEnd(int animIndex, Action callBack)
    {
        await Awaitable.WaitForSecondsAsync(chainMoveDelayTime + chainDelayTime);

        for (int i = 0; i < chainAnimatorList.Count; i++)
            chainAnimatorList[i].Play("Reverse" + _animNmaes[animIndex], 0, 0);
        
        await Awaitable.WaitForSecondsAsync(chainDelayTime);

        if (callBack != null)
            callBack?.Invoke();
    }

    public async void ChainEffect(SlotSkillIcon_Controller skillIcon, Action callBack = null)
    {
        await Awaitable.WaitForSecondsAsync(chainMoveDelayTime);
        skillIcon?.Chaining(true);

        ChainEffectEnd(skillIcon, callBack);
    }

    private async void ChainEffectEnd(SlotSkillIcon_Controller skillIcon, Action callBack)
    {
        await Awaitable.WaitForSecondsAsync(chainMoveDelayTime + chainDelayTime);

        skillIcon?.Chaining(false);
        await Awaitable.WaitForSecondsAsync(chainDelayTime);

        if (callBack != null)
            callBack?.Invoke();
    }

    public void SetSlotImage(bool isUp)
    {
        slotImage.sprite = slotData.GetSprite(isUp);
    }

    public void SetSlotEffect(bool isUp)
    {
        slotEffectAnimator.Play(isUp ? slotEffectName : "Re" + slotEffectName, 0, 0);
    }
}
