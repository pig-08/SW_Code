using System;
using UnityEngine;

namespace SW.Code.Stat
{
    [Serializable]
    public class StatOverride
    {
        [SerializeField] private StatSO _stat;
        [SerializeField] private bool _isUseOverride;
        [SerializeField] private float _overrideValue;

        public StatOverride(StatSO stat) => _stat = stat;

        public StatSO CreateStat()
        {
            StatSO newStat = _stat.Clone() as StatSO;
            Debug.Assert(newStat != null, $"{_stat.statName} clone failed");

            if(_isUseOverride)
                newStat.BaseValue = _overrideValue;

            return newStat; 
        }
    }
}