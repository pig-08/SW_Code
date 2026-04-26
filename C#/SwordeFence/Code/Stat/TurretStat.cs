using Gwamegi.Code.Towers;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace SW.Code.Stat
{
    public class TurretStat : MonoBehaviour, ITurretCompoenent
    {
        private Tower _tower; //타워 컴포넌트
        [SerializeField] private StatOverride[] statOverrides;
        private StatSO[] _stats; //실제 사용되는 스탯들

        

        public void Initialize()
        {
            _stats = statOverrides.Select(stat => stat.CreateStat()).ToArray();
            foreach(StatSO stat in _stats)
            {
                stat.OnValueChange += HandleStatValue;
            }


        }

        //어떤 스탯인지를 주면 해당하는 타입의 실제사용중인 스탯을 넘겨주는 함수
        public StatSO GetStat(StatSO targetStat)
        {
            Debug.Assert(targetStat != null, "Stats:GetStat : target stat is null");
            return _stats.FirstOrDefault(stat => stat.statName == targetStat.statName);
        }

        public StatSO GetStat(string targetStatName)
        {
            return _stats.FirstOrDefault(stat => stat.statName == targetStatName);
        }

        public StatSO GetRandomStat()
        {
            return _stats[UnityEngine.Random.Range(0, _stats.Length)];
        }

        public bool TryGetStat(StatSO targetStat, out StatSO outStat)
        {
            Debug.Assert(targetStat != null, "Stats::GetStat : target stat is null");

            outStat = _stats.FirstOrDefault(stat => stat.statName == targetStat.statName);
            return outStat;
        }

        public void SetBaseValue(StatSO stat, float value) => GetStat(stat).BaseValue = value;
        public float GetBaseValue(StatSO stat) => GetStat(stat).BaseValue;
        public void IncreaseBaseValue(StatSO stat, float value) => GetStat(stat).BaseValue += value;
        public void AddModifier(StatSO stat,object key, float value, bool isPercent = false) => GetStat(stat).AddModifier(key,value, isPercent);
        public void RemoveModifier(StatSO stat,object key) => GetStat(stat).RemoveModifier(key);

        public void CleanAllModifier()
        {
            foreach (StatSO stat in _stats)
            {
                stat.ClearAllModifier();
            }
        }

        private void HandleStatValue(StatSO stat, float current, float previous)
        {
            Debug.Log(current);
            _tower.GetStats();
        }

        public void Initialize(Tower tower)
        {
            _tower = tower;
            Initialize();
        }
    }
}