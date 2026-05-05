using DG.Tweening;
using PSW.Code.Dial;
using System;
using TMPro;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YIS.Code.Skills;

public abstract class SkillIconSettingCompo : MonoBehaviour
{
    [Header("Icon(Parents)")]
    [SerializeField] protected SkillCircleDataListSO skillCircleDataList;
    [SerializeField] protected Image outLine;
    [SerializeField] protected Image icon;

    [Header("PopUp(Parents)")]
    [SerializeField] protected SizeData sizeData;
    [SerializeField] protected float popTime = 0.3f;

    public virtual void SetSkill(SkillDataSO skillData)
    {
        if(skillData.visualData.icon != null)
            icon.sprite = skillData.visualData.icon;

        outLine.color = skillCircleDataList.GetOutLineColor(skillData.grade);
    }

    public float GetPopTime() => popTime;

    public abstract void PopUp();
}

[Serializable]
public struct SizeData
{
    public Vector3 Up;
    public Vector3 Down;

    public Vector3 GetSize(bool isOne)
    {
        if (isOne)
            return Up;
        else
            return Down;
    }
}