    using Code.Scripts.Enemies;
using Code.Scripts.Enemies.BT;
using PSB.Code.BattleCode.Enemies;
using PSB.Code.CoreSystem.SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Work.PSB.Code.CoreSystem;
using Work.PSB.Code.FieldCode.BTs;
using Work.PSB.Code.FieldCode.MapSaves;
using YIS.Code.Modules;

namespace Work.PSB.Code.FieldCode
{
    public class FieldEnemyData : MonoBehaviour, IModule
    {
        [SerializeField] private string enemyID = "";
        [SerializeField] private EnemySO[] setEnemies;
        [SerializeField] private BattlePresentationSO battlePresentation;

        [SerializeField] private BattleEnterContextSO enterContext;
        [SerializeField] private AnimParamSO idleAnimParam;
        [SerializeField] private AnimParamSO hitAnimParam;

        private FieldEnemy _owner;
        private AgentMovement _movement;
        private FieldEnemyAngle _angle;
        private FieldEnemySensor _sensor;
        private Collider2D _collider;

        public string EnemyID => enemyID;
        public bool IsAlive = true;

        public void Initialize(ModuleOwner owner)
        {
            _owner = owner as FieldEnemy;
            _movement = owner.GetModule<AgentMovement>();
            _angle = owner.GetModule<FieldEnemyAngle>();
            _sensor = owner.GetModule<FieldEnemySensor>();
        }

        private void Awake()
        {
            if (string.IsNullOrEmpty(enemyID))
            {
                Debug.LogError($"[FieldEnemyData] enemyID is empty on {name}");
            }

            SceneObjectRegistry.RegisterEnemy(this);
            _collider = GetComponent<Collider2D>();
        }

        private void OnDestroy()
        {
            SceneObjectRegistry.UnRegisterEnemy(this);
        }

        public void Hit()
        {
            if (!IsAlive) return;

            NormalFieldEnemy normalEnemy = _owner as NormalFieldEnemy;

            if (normalEnemy == null)
            {
                Debug.LogError("실패 NormalFieldEnemy가 아닙니다.");
                return;
            }

            if (enterContext != null && enterContext.HasRequest)
            {
                if (enterContext.EnterBy == BattleEnterBy.Player)
                {
                    normalEnemy.ChangeState(EnemyState.Hit);
                }
                else
                {
                    normalEnemy.ChangeState(EnemyState.Idle);
                }

                var player = SceneObjectRegistry.GetPlayer();
                if (player != null)
                { 
                    StartBattle(player);
                }
            }
            else
            {
                normalEnemy.ChangeState(EnemyState.Hit);
            }
        }

        public void StartBattle(PlayerStateHandler player)
        {
            float dirX = player.transform.position.x - transform.position.x;
            _owner.EntityRenderer.FlipController(dirX);

            BattleContext.Set(SceneManager.GetActiveScene().name, enemyID);
            BattleRuntimeData.Set(setEnemies, battlePresentation);

            _collider.enabled = false;
            _movement.CanManualMove = false;
            _movement.StopImmediately();

            _movement.RigidCompo.linearVelocity = Vector2.zero;
            _movement.RigidCompo.angularVelocity = 0f;

            //_owner.EntityRenderer.LockClip(true);

            if (_angle != null)
                _angle.gameObject.SetActive(false);

            if (_sensor != null)
                _sensor.gameObject.SetActive(false);
        }

        public void LoadEnemy(SceneState state)
        {
            if (state == null || state.enemies == null)
                return;

            int idx = state.enemies.FindIndex(e => e.id == enemyID);

            if (idx < 0)
            {
                SceneSaveSystem.SetEnemyAlive(
                    SceneManager.GetActiveScene().name,
                    enemyID,
                    IsAlive
                );
                return;
            }

            var saved = state.enemies[idx];
            IsAlive = saved.isAlive;

            if (!IsAlive)
            {
                gameObject.SetActive(false);
                return;
            }

            transform.position = saved.position;
            gameObject.SetActive(true);
        }

        public void RestoreActiveIfAlive()
        {
            if (!IsAlive) return;
            gameObject.SetActive(true);
        }

        public void ApplyDead()
        {
            IsAlive = false;
            gameObject.SetActive(false);
        }
        
    }
}