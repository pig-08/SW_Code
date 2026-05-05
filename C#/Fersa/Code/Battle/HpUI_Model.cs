using PSW.Code.SkillDeck;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PSW.Code.Battle
{
    public class HpUI_Model : ModelCompo<HpData>
    {
        [SerializeField] private float time;
        [SerializeField] private List<HpTextPosData> hpTextPosDataList;
        public void ChangeMaxHp(float maxHp) => _currentData.maxHp = maxHp;
        public void ChangeCurrentHp(float currentHp) => _currentData.currentHp = currentHp;
        public HpTextPosData GetTextPosData(bool isLeft)
        {
            HpTextPosData data = hpTextPosDataList[0];

            foreach (var hp in hpTextPosDataList)
            {
                if (hp.isLeft == isLeft)
                    data = hp;
            }

            return data;
        }

        public override HpData InitModel()
        {
            return _currentData;
        }
    }
    [Serializable]
    public struct HpTextPosData
    {
        public bool isLeft;
        public Vector2 anchorMin;
        public Vector2 anchorMax;
        public Vector2 pivot;
        public Vector3 pos;
    }
}