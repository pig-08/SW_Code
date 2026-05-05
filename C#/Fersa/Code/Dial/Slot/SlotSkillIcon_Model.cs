using System.Collections.Generic;
using UnityEngine;
using YIS.Code.Skills;

namespace PSW.Code.Dial.Slot
{
    public class SlotSkillIcon_Model : SlotIcon_Model
    {
        [SerializeField] private IconShakeValueData shakeData;
        [SerializeField] private List<string> chainAnimClipNameList;

        public IconShakeValueData GetShakeValueData() => shakeData;
        public string GetChainEffectName(bool isUp) => isUp ? chainAnimClipNameList[0] : chainAnimClipNameList[1];
    }
}