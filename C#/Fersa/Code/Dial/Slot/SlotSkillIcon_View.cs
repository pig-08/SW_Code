using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace PSW.Code.Dial.Slot
{
    public class SlotSkillIcon_View : SlotIcon_View
    {
        [SerializeField] private Animator chainEffectAnimator;

        public void PlayChainEffect(string animName) => chainEffectAnimator.Play(animName, 0, 0);
        public void ShakeIcon(IconShakeValueData shakeValueData)
        {
            rectTransform.DOShakeAnchorPos(shakeValueData.duration,
                shakeValueData.strength,
                shakeValueData.vibrato,
                shakeValueData.randomness, 
                shakeValueData.snapping,
                shakeValueData.fadeOut);
        }

    }
}