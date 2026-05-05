using System;
using System.Collections;
using System.Collections.Generic;
using CIW.Code.System.Events;
using DG.Tweening;
using PSB.Code.BattleCode.Enums;
using PSB.Code.CoreSystem.Events;
using PSB_Lib.Dependencies;
using PSB.Code.BattleCode.Enemies.BTs.Events;
using PSW.Code.EventBus;
using UnityEngine;
using Work.CSH.Scripts.Managers;

namespace PSB.Code.BattleCode.Enemies
{
    [Serializable]
    public struct BatchSize
    {
        public Vector2 startSize;
        public Vector2 endSize;
    }

    [DefaultExecutionOrder(-5)]
    [Provide]
    public class BattleEnemyManager : MonoBehaviour, IDependencyProvider
    {
        [SerializeField] private List<BattleEnemy> enemies = new();
        [SerializeField] private float attackDelay = 0.8f;

        [Header("배치 범위 설정")]
        [SerializeField] private BatchSize batchSize;

        [Header("배치 설정")]
        [SerializeField] private Vector2 startPosition = new(0f, 0f);
        [SerializeField] private Vector2 cellSize = new(1.5f, 1.5f);
        [SerializeField] private int columns = 5;

        [Header("DoTween")]
        [SerializeField] private float arrangeDuration = 0.35f;
        [SerializeField] private Ease arrangeEase = Ease.OutCubic;
        [SerializeField] private bool killPreviousArrangeTween = true;
        [SerializeField] private float arrangeStagger = 0.03f;

        private readonly Dictionary<BattleEnemy, Tween> _arrangeTweens = new();
        private static object ArrangeTweenId(BattleEnemy e) => (e, "ARRANGE");

        private bool _initialArrangeDone;
        private bool _battleEndRaised;
        private bool _arrangeDirty;

        private bool _isAttackSequenceRunning;
        private TurnManagerSO _turnManagerCache;

        private BattleEnemy _waitingBattleEnemy;
        private bool _waitingDone;
        private Coroutine _seqCo;

        private void OnEnable()
        {
            Bus<EnemyTurnDoneEvent>.OnEvent += OnEnemyTurnDone;

            KillAllArrangeTweens();
            _initialArrangeDone = false;
            _battleEndRaised = false;
            _arrangeDirty = false;
            
            StopSequenceIfRunning();

            StartCoroutine(CoInitialArrange());
        }

        private void OnDisable()
        {
            Bus<EnemyTurnDoneEvent>.OnEvent -= OnEnemyTurnDone;
            StopSequenceIfRunning();
            KillAllArrangeTweens();
            if (_turnManagerCache != null)
                _turnManagerCache.OnBattleEndedCondition = null;
        }

        private void StopSequenceIfRunning()
        {
            if (_seqCo != null)
            {
                StopCoroutine(_seqCo);
                _seqCo = null;
            }

            _isAttackSequenceRunning = false;
            _waitingBattleEnemy = null;
            _waitingDone = false;
        }

        private IEnumerator CoInitialArrange()
        {
            SetEnemiesVisible(false);

            SortEnemies();
            ArrangeEnemies(false);

            yield return null;

            SetEnemiesVisible(true);
            _initialArrangeDone = true;
        }

        private void Update()
        {
            if (!_battleEndRaised && enemies.Count <= 0)
            {
                _battleEndRaised = true;
                Debug.Log("적 다 죽었다");
                Bus<BattleEnd>.Raise(new BattleEnd(true));
            }
        }

        public void Register(BattleEnemy battleEnemy)
        {
            if (battleEnemy == null) return;
            if (enemies.Contains(battleEnemy)) return;

            enemies.Add(battleEnemy);
            SortEnemies();
            
            if (!_initialArrangeDone)
            {
                ArrangeEnemies(false);
            }
            else
            {
                //_arrangeDirty = true;
                ArrangeEnemies(true);
            }

            CacheTurnManager(battleEnemy);
            Bus<EnemyListChanged>.Raise(new EnemyListChanged());
        }

        public void Unregister(BattleEnemy battleEnemy)
        {
            if (battleEnemy == null) return;
            if (!enemies.Contains(battleEnemy)) return;

            DOTween.Kill(ArrangeTweenId(battleEnemy));
            _arrangeTweens.Remove(battleEnemy);

            enemies.Remove(battleEnemy);
            SortEnemies();

            if (_waitingBattleEnemy == battleEnemy)
            {
                _waitingBattleEnemy = null;
                _waitingDone = true;
            }
            
            if (!_initialArrangeDone)
            {
                ArrangeEnemies(false);
            }
            else
            {
                //_arrangeDirty = true;
                ArrangeEnemies(true);
            }

            Bus<EnemyListChanged>.Raise(new EnemyListChanged());
        }

        public IReadOnlyList<BattleEnemy> GetEnemies() => enemies;

        private void CacheTurnManager(BattleEnemy battleEnemy)
        {
            if (_turnManagerCache == null && battleEnemy != null && battleEnemy.TurnManager != null)
            {
                _turnManagerCache = battleEnemy.TurnManager;

                _turnManagerCache.OnBattleEndedCondition = CheckAllEnemiesDead;
            }
        }

        private bool CheckAllEnemiesDead()
        {
            if (_battleEndRaised) return true;

            foreach (var e in enemies)
            {
                if (e != null && e.gameObject.activeInHierarchy && !e.IsDead)
                {
                    return false;
                }
            }

            return true;
        }

        private void ArrangeEnemies(bool tween)
        {
            List<Vector3> targets = ComputeEnemyPositions();
            if (targets == null) return;

            if (tween) TweenToPositions(targets);
            else ApplyPositionsInstant(targets);

            ApplySortingOrders();
        }

        private void ApplyPositionsInstant(List<Vector3> targetPositions)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null) continue;
                enemies[i].transform.position = targetPositions[i];
            }
        }

        private void ApplySortingOrders()
        {
            const int frontBase = 20;
            const int backBase = 10;

            for (int i = 0; i < enemies.Count; i++)
            {
                BattleEnemy enemy = enemies[i];
                if (enemy == null) continue;

                bool isFront = i % 2 == 0;
                int baseOrder = isFront ? frontBase : backBase;

                int order = baseOrder + i;

                SpriteRenderer[] renderers = enemy.GetComponentsInChildren<SpriteRenderer>(true);
                for (int r = 0; r < renderers.Length; r++)
                    renderers[r].sortingOrder = order;
            }
        }

        private List<Vector3> ComputeEnemyPositions()
        {
            if (enemies.Count <= 0) return null;

            List<Vector3> positions = new List<Vector3>(enemies.Count);
            for (int i = 0; i < enemies.Count; i++)
            {
                int row = i / columns;
                int col = i % columns;

                float yOffset = (i == 1 || i == 3) ? cellSize.y : 0f;

                Vector3 pos = new Vector3(startPosition.x + col * cellSize.x, 
                    startPosition.y - row * cellSize.y + yOffset, 0f);

                positions.Add(pos);
            }

            float minX = float.MaxValue, maxX = float.MinValue;
            float minY = float.MaxValue, maxY = float.MinValue;

            foreach (Vector3 p in positions)
            {
                minX = Mathf.Min(minX, p.x);
                maxX = Mathf.Max(maxX, p.x);
                minY = Mathf.Min(minY, p.y);
                maxY = Mathf.Max(maxY, p.y);
            }

            float width = maxX - minX;
            float height = maxY - minY;

            float targetMinX = batchSize.startSize.x;
            float targetMinY = batchSize.startSize.y;
            float targetMaxX = batchSize.endSize.x;
            float targetMaxY = batchSize.endSize.y;

            float targetWidth = targetMaxX - targetMinX;
            float targetHeight = targetMaxY - targetMinY;

            float scaleX = targetWidth / Mathf.Max(width, 0.01f);
            float scaleY = targetHeight / Mathf.Max(height, 0.01f);
            float scale = Mathf.Min(scaleX, scaleY, 1f);

            List<Vector3> result = new List<Vector3>(enemies.Count);
            for (int i = 0; i < enemies.Count; i++)
            {
                Vector3 p = positions[i];
                Vector3 scaled = startPosition + (Vector2)(p - (Vector3)startPosition) * scale;

                scaled.x = Mathf.Clamp(scaled.x, targetMinX, targetMaxX);
                scaled.y = Mathf.Clamp(scaled.y, targetMinY, targetMaxY);

                result.Add(scaled);
            }

            return result;
        }

        private void TweenToPositions(List<Vector3> targetPositions)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                BattleEnemy enemy = enemies[i];
                if (enemy == null) continue;

                if (killPreviousArrangeTween)
                    DOTween.Kill(ArrangeTweenId(enemy));
                
                enemy.transform.DOKill();

                Tween t = enemy.transform
                    .DOMove(targetPositions[i], arrangeDuration)
                    .SetEase(arrangeEase)
                    .SetDelay(arrangeStagger > 0f ? i * arrangeStagger : 0f)
                    .SetUpdate(false)
                    .SetId(ArrangeTweenId(enemy));

                _arrangeTweens[enemy] = t;
            }
        }

        private void SetEnemiesVisible(bool visible)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                BattleEnemy e = enemies[i];
                if (e == null) continue;

                Renderer[] renderers = e.GetComponentsInChildren<Renderer>(true);
                for (int r = 0; r < renderers.Length; r++)
                    renderers[r].enabled = visible;
            }
        }

        private void KillAllArrangeTweens()
        {
            foreach (KeyValuePair<BattleEnemy, Tween> kv in _arrangeTweens)
            {
                if (kv.Value != null && kv.Value.IsActive())
                    kv.Value.Kill();
            }
            _arrangeTweens.Clear();
        }

        private void SortEnemies()
        {
            Dictionary<BattleEnemy, int> originIndex = new();
            for (int i = 0; i < enemies.Count; i++)
                originIndex[enemies[i]] = i;

            enemies.Sort((a, b) =>
            {
                if (a == null && b == null) return 0;
                if (a == null) return 1;
                if (b == null) return -1;

                int priA = GetPlacementPriority(a.enemySO.grade);
                int priB = GetPlacementPriority(b.enemySO.grade);

                int compare = priA.CompareTo(priB);
                if (compare != 0) return compare;

                return originIndex[a].CompareTo(originIndex[b]);
            });

            int bossIndex = enemies.FindIndex(e => e != null && e.enemySO.grade == EnemyGrade.Boss);
            if (bossIndex >= 0)
            {
                BattleEnemy boss = enemies[bossIndex];
                enemies.RemoveAt(bossIndex);

                int cnt = enemies.Count + 1;
                int insertCnt = cnt / 2;

                enemies.Insert(insertCnt, boss);
            }
        }

        private int GetPlacementPriority(EnemyGrade grade)
        {
            return grade switch
            {
                EnemyGrade.Common => 0,
                EnemyGrade.MiniBoss => 1,
                EnemyGrade.Boss => 2,
                EnemyGrade.Elite => 3,
                _ => 1
            };
        }

        public void TryStartEnemyAttackSequence()
        {
            if (_isAttackSequenceRunning) return;
            
            if (_arrangeDirty && _initialArrangeDone)
            {
                _arrangeDirty = false;
                ArrangeEnemies(true);
            }

            _seqCo = StartCoroutine(EnemyAttackSequence());
        }

        private void OnEnemyTurnDone(EnemyTurnDoneEvent evt)
        {
            if (!_isAttackSequenceRunning) return;
            if (_waitingBattleEnemy == null) return;
            if (evt.BattleEnemy == null) return;

            if (ReferenceEquals(evt.BattleEnemy, _waitingBattleEnemy))
                _waitingDone = true;
        }

        private IEnumerator EnemyAttackSequence()
        {
            _isAttackSequenceRunning = true;
            
            if (_initialArrangeDone)
                yield return new WaitForSeconds(arrangeDuration);
            
            yield return new WaitForSeconds(attackDelay);

            List<BattleEnemy> activeEnemies = new(enemies);

            foreach (BattleEnemy enemy in activeEnemies)
            {
                if (enemy == null || !enemy.gameObject.activeInHierarchy 
                                  || enemy.IsDead || enemy.IsFreeze)
                    continue;
                
                if (enemy.enemySO.attackSkills.Length == 0)
                {
                    Debug.Log($"[Battle] {enemy.gameObject.name}은(는) 공격 스킬이 없어 턴을 스킵합니다.");
                    continue;
                }

                _waitingBattleEnemy = enemy;
                _waitingDone = false;
                
                enemy.SendBTState(BattleEnemyState.Move);
                
                yield return new WaitUntil(() => _waitingDone 
                                                 || _waitingBattleEnemy == null 
                                                 || !_waitingBattleEnemy.gameObject.activeInHierarchy 
                                                 || _waitingBattleEnemy.IsDead);

                _waitingBattleEnemy = null;

                if (enemy != null && !enemy.IsDead) 
                {
                    yield return new WaitForSeconds(attackDelay);
                }
            }
            
            TickEnemyBuffsOnce(activeEnemies);
            
            yield return new WaitForSeconds(0.3f);

            _isAttackSequenceRunning = false;
            _seqCo = null;
            if (_arrangeDirty)
            {
                _arrangeDirty = false;
                ArrangeEnemies(_initialArrangeDone);
            }

            bool hasLivingEnemy = false;
            foreach (var e in enemies)
            {
                // 리스트에 들어있더라도, 이미 죽었거나(IsDead) 비활성화된 놈은 무시
                if (e != null && !e.IsDead && e.gameObject.activeInHierarchy)
                {
                    hasLivingEnemy = true;
                    break;
                }
            }

            // 진짜로 숨이 붙어있는 놈이 하나도 없다면
            if (!hasLivingEnemy || _battleEndRaised)
            {
                Debug.Log("필드에 시체뿐임: 턴 전환 중단");
                yield break;
            }
            _turnManagerCache?.NextTurn();
        }

        public void ResetAttackSequenceFlag() => _isAttackSequenceRunning = false;
        
        private void TickEnemyBuffsOnce(List<BattleEnemy> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var enemy = list[i];
                if (enemy == null || !enemy.gameObject.activeInHierarchy)
                    continue;

                var buffModule = enemy.buffModule;
                buffModule?.UpdateTime();
            }
        }
        
    }
}
