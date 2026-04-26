using System;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Code.SO
{
    [CreateAssetMenu(fileName = "Struct", menuName = "SO/Struct/StructList")]
    public class StatDataListSO : ScriptableObject
    {
        public List<STAT> statDataList;
    }
}

[Serializable]
public struct STAT
{
    public string statName;
    public float minRange;
    public float maxRange;
    public CalculationType calculationType;
    public UpGradepType upGradepType;
}

public enum CalculationType
{
    Plus,
    Multiplication
}