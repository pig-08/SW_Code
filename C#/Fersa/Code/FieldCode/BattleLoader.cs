using System;
using CIW.Code;
using Code.Scripts.Entities;
using PSB_Lib.Dependencies;
using PSB_Lib.ObjectPool.RunTime;
using PSB.Code.BattleCode.BattleSystems;
using PSB.Code.BattleCode.Players;
using UnityEngine;
using Work.PSB.Code.CoreSystem.Tests;
using Work.PSB.Code.CoreSystem.UpgradeSystem;

namespace Work.PSB.Code.FieldCode
{
    public class BattleLoader : MonoBehaviour
    {
        [SerializeField] private EnemyFactory enemyFactory;
        [SerializeField] private SpriteRenderer background;
        [SerializeField] private MilestoneRewardApplier rewardApplier;

        [Inject] private PoolManagerMono _poolManager;
        [Inject] private PlayerManager _playerManager;

        private void Awake()
        {
            KillCounter.Instance?.TakeSnapshot();
            BattleLootSession.Instance?.Clear();

            LoadEnemies();
            ApplyPresentation();

            if (BattleRuntimeData.Enemies == null || BattleRuntimeData.Enemies.Length == 0)
            {
                Debug.LogWarning("[BattleLoader] BattleRuntimeData.Enemies is empty.");
            }
        }

        private void Start()
        {
            ApplyMilestone();
        }

        private void LoadEnemies()
        {
            if (BattleRuntimeData.Enemies == null || BattleRuntimeData.Enemies.Length == 0)
            {
                Debug.LogWarning("[BattleLoader] No enemies found in BattleRuntimeData.");
                return;
            }

            foreach (var enemyData in BattleRuntimeData.Enemies)
            {
                if (enemyData == null)
                {
                    Debug.LogWarning("[BattleLoader] Encountered null EnemySO in runtime data.");
                    continue;
                }

                enemyFactory.CreateEnemy(enemyData, _poolManager);
            }
        }

        private void ApplyPresentation()
        {
            if (background == null)
            {
                Debug.LogWarning("[BattleLoader] Background SpriteRenderer is not assigned.");
                return;
            }

            if (BattleRuntimeData.Presentation == null)
            {
                Debug.LogWarning("[BattleLoader] Presentation is null.");
                return;
            }

            background.sprite = BattleRuntimeData.Presentation.backSprite;
        }

        private void ApplyMilestone()
        {
            Entity playerEntity = _playerManager.BattlePlayer; 
    
            UpgradeService upgradeService = new UpgradeService(playerEntity.GetModule<EntityStat>());

            if (rewardApplier != null)
            {
                rewardApplier.ApplyAllMilestones(playerEntity, upgradeService);
            }
        }
        
    }
}