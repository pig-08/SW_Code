using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YIS.Code.Skills;

public class CircleIcon : SkillIconSettingCompo
{
    [Header("Icon(Children)")]
    [SerializeField] protected Image elementIcon;
    [SerializeField] protected TextMeshProUGUI countText;

    [Header("PopUp(Children)")]
    [SerializeField] private float resetDelayTime = 0.1f;
    public override void SetSkill(SkillDataSO skillData)
    {
        base.SetSkill(skillData);
        elementIcon.sprite = skillCircleDataList.GetElementImage(skillData.elemental);
    }

    public async override void PopUp()
    {
        transform.DOScale(sizeData.GetSize(true), popTime);
        await Awaitable.WaitForSecondsAsync(resetDelayTime, destroyCancellationToken);
        transform.DOScale(sizeData.GetSize(false), popTime);
        await Awaitable.WaitForSecondsAsync(resetDelayTime, destroyCancellationToken);
        transform.DOScale(Vector3.one, popTime);
    }
}
