using System;
using System.Collections.Generic;
using PSB.Code.BattleCode.Enums;
using PSB.Code.BattleCode.Events;
using PSB.Code.CoreSystem.Events;
using PSB.Code.CoreSystem.SaveSystem;
using PSW.Code.EventBus;
using UnityEngine;

namespace Work.PSB.Code.CoreSystem.Tests
{
    public class KillCounter : MonoBehaviour, ISaveable
    {
        public static KillCounter Instance { get; private set; }

        [field: SerializeField] public SaveId SaveId { get; private set; }
        
        private Dictionary<string, int> _killCounts = new Dictionary<string, int>();
        private Dictionary<EnemyGrade, int> _gradeKillCounts = new Dictionary<EnemyGrade, int>();
        
        private int _totalKillCount = 0;
        
        private string _battleStartSnapshot;

        [Serializable]
        public class KillEntry
        {
            public string enemyName;
            public int count;
        }
        
        [Serializable]
        public class GradeKillEntry
        {
            public EnemyGrade grade;
            public int count;
        }

        [Serializable]
        public class KillSaveCollection
        {
            public int totalCount;
            public List<KillEntry> entries = new List<KillEntry>();
            public List<GradeKillEntry> gradeEntries = new List<GradeKillEntry>();
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Bus<RollbackBattleLootEvent>.OnEvent += OnRollbackTriggered;
            Bus<VillageResetEvent>.OnEvent += HandleVillageReset;
        }

        private void HandleVillageReset(VillageResetEvent evt)
        {
            _killCounts.Clear();
            _gradeKillCounts.Clear();
            _totalKillCount = 0;
            _battleStartSnapshot = null;
            Debug.Log("<color=purple>마을 초기화 - 킬 카운터 리셋</color>");
        }

        private void OnDestroy()
        {
            Bus<RollbackBattleLootEvent>.OnEvent -= OnRollbackTriggered;
            Bus<VillageResetEvent>.OnEvent -= HandleVillageReset;
        }

        public void AddKill(string enemyName, EnemyGrade grade)
        {
            if (string.IsNullOrEmpty(enemyName)) return;

            if (!_killCounts.ContainsKey(enemyName)) _killCounts[enemyName] = 0;
            _killCounts[enemyName]++;
            
            if (!_gradeKillCounts.ContainsKey(grade)) _gradeKillCounts[grade] = 0;
            _gradeKillCounts[grade]++;
            
            _totalKillCount++;
            
            Debug.Log($"<color=navy>[Kill] {enemyName}, {grade} 처치 / 누적: {_killCounts[enemyName]}" +
                      $" - 전체 : {_totalKillCount}</color>");
        }

        public int GetTotalKillCount() => _totalKillCount;
        
        public int GetKillCountByName(string enemyName)
        {
            return _killCounts.TryGetValue(enemyName, out int count) ? count : 0;
        }

        public int GetKillCountByGrade(EnemyGrade grade)
        {
            return _gradeKillCounts.TryGetValue(grade, out int count) ? count : 0;
        }
        
        public void TakeSnapshot()
        {
            _battleStartSnapshot = GetSaveData();
        }

        // [핵심] 승리 후 패널에서 나갈 때 호출하여 백업본 제거
        public void CommitKills()
        {
            _battleStartSnapshot = null;
        }
        
        private void OnRollbackTriggered(RollbackBattleLootEvent e)
        {
            if (string.IsNullOrEmpty(_battleStartSnapshot)) return;

            RestoreSaveData(_battleStartSnapshot);
            _battleStartSnapshot = null; 
            Debug.Log("<color=red>[KillCounter] 전투 포기: 데이터가 전투 전으로 롤백되었습니다.</color>");
        }
        
        public string GetSaveData()
        {
            var collection = new KillSaveCollection();
            collection.totalCount = _totalKillCount;
            
            foreach (var kvp in _killCounts)
            {
                collection.entries.Add(new KillEntry { enemyName = kvp.Key, count = kvp.Value });
            }

            foreach (var kvp in _gradeKillCounts)
            {
                collection.gradeEntries.Add(new GradeKillEntry { grade = kvp.Key, count = kvp.Value });
            }
            
            return JsonUtility.ToJson(collection);
        }

        public void RestoreSaveData(string saveData)
        {
            _killCounts.Clear();
            _totalKillCount = 0;
            
            if (string.IsNullOrEmpty(saveData)) return;

            var loaded = JsonUtility.FromJson<KillSaveCollection>(saveData);
            
            if (loaded != null)
            {
                _totalKillCount = loaded.totalCount;
                
                foreach (var entry in loaded.entries)
                {
                    _killCounts[entry.enemyName] = entry.count;
                }
                
                foreach (var entry in loaded.gradeEntries)
                {
                    _gradeKillCounts[entry.grade] = entry.count;
                }
            }
        }
        
    }
}