using System.Collections.Generic;
using Code.Scripts.Entities;
using PSB_Lib.StatSystem;
using PSB.Code.BattleCode.Enemies;
using PSB.Code.BattleCode.Entities;
using PSB.Code.BattleCode.Events;
using PSW.Code.Battle;
using PSW.Code.EventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YIS.Code.Modules;
using YIS.Code.Skills;

namespace PSB.Code.BattleCode.UIs
{
    public class EnemyInfoPanelUI : MonoBehaviour
    {
        [Header("Root")]
        [SerializeField] private GameObject root;

        [Header("Enemy Basic Info")]
        [SerializeField] private Image enemyIcon;
        [SerializeField] private TextMeshProUGUI enemyName;

        [Header("HP UI")]
        [SerializeField] private HpUI_Controller hpController;

        [Header("Stats")]
        [SerializeField] private Transform statRoot;
        [SerializeField] private StatNamePanel statNamePanel;
        [SerializeField] private EnemyStatLineUI statLinePrefab;

        [Header("Skills")]
        [SerializeField] private Transform skillRoot;
        [SerializeField] private EnemySkillItemUI skillItemPrefab;
        [SerializeField] private RectTransform tooltipAnchor;

        [Header("Buffs")]
        [SerializeField] private Transform buffContent;
        [SerializeField] private BuffIconView buffIconPrefab;
        [SerializeField] private TextMeshProUGUI emptyText;

        private BattleEnemy _currentEnemy;
        private EntityHealth _currentHealth;
        private BuffModule _currentBuffModule;

        private Transform _currentBuffEventTarget;

        private readonly List<GameObject> _spawned = new();
        private readonly Dictionary<BuffVisualSO, BuffIconView> _buffViews = new();

        private void Awake()
        {
            if (root != null)
                root.SetActive(false);
            
            UpdateEmptyTextVisibility();
        }

        private void OnEnable()
        {
            Bus<EnemyHoverInfoEvent>.OnEvent += HandleEnemyInfoShow;
            Bus<EnemyInfoCloseEvent>.OnEvent += HandleClose;
            Bus<BuffUiEvent>.OnEvent += HandleBuffUiEvent;
        }

        private void OnDisable()
        {
            Bus<EnemyHoverInfoEvent>.OnEvent -= HandleEnemyInfoShow;
            Bus<EnemyInfoCloseEvent>.OnEvent -= HandleClose;
            Bus<BuffUiEvent>.OnEvent -= HandleBuffUiEvent;

            Close();
        }

        private void HandleEnemyInfoShow(EnemyHoverInfoEvent evt)
        {
            if (!evt.Show) return;
            if (evt.Enemy == null) return;

            _currentEnemy = evt.Enemy;
            Apply(_currentEnemy);

            if (root != null)
                root.SetActive(true);
        }

        private void HandleClose(EnemyInfoCloseEvent evt) => Close();

        private void Close()
        {
            UnbindHealth();

            _currentEnemy = null;
            _currentBuffModule = null;
            _currentBuffEventTarget = null;

            ClearSpawned();
            ClearBuffViews();

            if (root != null)
                root.SetActive(false);
        }

        private void Apply(BattleEnemy enemy)
        {
            ClearSpawned();
            ClearBuffViews();
            UnbindHealth();

            _currentBuffModule = null;
            _currentBuffEventTarget = null;

            if (enemy == null) return;

            EnemySO so = enemy.enemySO;
            if (so == null) return;

            if (enemyIcon != null) enemyIcon.sprite = so.icon;
            if (enemyName != null) enemyName.text = so.enemyName;

            _currentHealth = enemy.GetModule<EntityHealth>();
            if (_currentHealth != null && hpController != null)
            {
                _currentHealth.OnHealthChangeEvent += HandleHealthChanged;
                hpController.Init(_currentHealth.CurrentHealth, _currentHealth.MaxHealth, isLeft: false);
            }

            var statComp = enemy.GetModule<EntityStat>();
            if (statComp != null)
            {
                foreach (var stat in statComp.GetAllStats())
                {
                    if (stat == null) continue;
                    if (stat.statName == "HP") continue;

                    int value = Mathf.RoundToInt(stat.Value);
                    SpawnStatLine(stat, value);
                }
            }

            if (so.attackSkills != null && skillItemPrefab != null && skillRoot != null)
            {
                for (int i = 0; i < so.attackSkills.Length; i++)
                {
                    SkillDataSO skill = so.attackSkills[i];
                    if (skill == null) continue;

                    var item = Instantiate(skillItemPrefab, skillRoot);
                    _spawned.Add(item.gameObject);

                    item.Set(enemy, skill, tooltipAnchor);
                }
            }
            
            _currentBuffModule = enemy.GetComponentInChildren<BuffModule>(true);
            if (_currentBuffModule != null)
            {
                _currentBuffEventTarget = enemy.transform;
                
                var snapshot = _currentBuffModule.GetActiveBuffs();
                for (int i = 0; i < snapshot.Count; i++)
                {
                    var (buffSo, remain) = snapshot[i];
                    UpsertBuff(buffSo, remain, enemy, false);
                }
            }
        }

        private void HandleBuffUiEvent(BuffUiEvent evt)
        {
            if (_currentEnemy == null) return;
            if (_currentBuffModule == null) return;
            if (_currentBuffEventTarget == null) return;
            if (evt.Target == null || evt.BuffVisualData == null) return;
            
            if (!ReferenceEquals(evt.Target, _currentBuffEventTarget))
                return;
            switch (evt.Op)
            {
                case BuffUiOp.Applied:
                case BuffUiOp.Updated:
                    UpsertBuff(evt.BuffVisualData, evt.Duration, evt.Target, false);
                    break;

                case BuffUiOp.Removed:
                    RemoveBuff(evt.BuffVisualData);
                    break;
            }
        }

        private void UpsertBuff(BuffVisualSO dataSo, int duration, ModuleOwner owner, bool isLeft)
        {
            if (buffContent == null || buffIconPrefab == null || dataSo == null)
                return;

            if (!_buffViews.TryGetValue(dataSo, out var view) || view == null)
            {
                view = Instantiate(buffIconPrefab, buffContent);
                _buffViews[dataSo] = view;
            }

            view.Set(dataSo, duration, owner, isLeft);
            UpdateEmptyTextVisibility();
        }

        private void RemoveBuff(BuffVisualSO dataSo)
        {
            if (dataSo == null) return;
            if (!_buffViews.TryGetValue(dataSo, out var view) || view == null) return;

            Destroy(view.gameObject);
            _buffViews.Remove(dataSo);
            UpdateEmptyTextVisibility();
        }

        private void ClearBuffViews()
        {
            foreach (var kv in _buffViews)
                if (kv.Value != null) Destroy(kv.Value.gameObject);

            _buffViews.Clear();
            UpdateEmptyTextVisibility();
        }

        private void HandleHealthChanged(float current, float max)
        {
            if (hpController == null) return;
            hpController.ChangeMaxHp(max);
            hpController.ChangeCurrentHp(current);
        }

        private void UnbindHealth()
        {
            if (_currentHealth != null)
                _currentHealth.OnHealthChangeEvent -= HandleHealthChanged;
            _currentHealth = null;
        }

        private void SpawnStatLine(StatSO stat, int value)
        {
            if (statLinePrefab == null || statRoot == null) return;

            var line = Instantiate(statLinePrefab, statRoot);
            _spawned.Add(line.gameObject);

            line.Set(stat, value, statNamePanel);
        }

        private void ClearSpawned()
        {
            for (int i = 0; i < _spawned.Count; i++)
                if (_spawned[i] != null) Destroy(_spawned[i]);

            _spawned.Clear();
        }
        
        private void UpdateEmptyTextVisibility()
        {
            if (emptyText != null)
            {
                emptyText.gameObject.SetActive(_buffViews.Count == 0);
            }
        }
        
    }
}
