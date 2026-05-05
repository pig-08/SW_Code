using PSW.Code.Dial;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YIS.Code.Defines;

namespace PSW.Code.Battle
{
    [CreateAssetMenu(fileName = "DamageColorList", menuName = "SO/Battle/DamageColorList")]
    public class DamageColorListSO : ScriptableObject
    {
        public List<DamageColorData> damageColorDataList;

        public TMP_ColorGradient GetColor(Elemental type)
        {
            TMP_ColorGradient currentColor = null;
            foreach (DamageColorData data in damageColorDataList)
            {
                if (data.elementalType == type)
                    currentColor = data.color;
            }
            return currentColor;
        }
    }

    [Serializable]
    public struct DamageColorData
    {
        public Elemental elementalType;
        public TMP_ColorGradient color;
    }
}