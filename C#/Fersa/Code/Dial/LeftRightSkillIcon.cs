using DG.Tweening;
using UnityEngine;

public class LeftRightSkillIcon : SkillIconSettingCompo
{
    [Header("PopUp(Children)")]
    [SerializeField] private float resetDelayTime = 0.1f;

    public async override void PopUp()
    {
        transform.DOLocalMoveY(sizeData.GetSize(true).y, popTime);
        await Awaitable.WaitForSecondsAsync(resetDelayTime + popTime);
        transform.DOLocalMoveY(sizeData.GetSize(false).y, popTime);
    }
}
