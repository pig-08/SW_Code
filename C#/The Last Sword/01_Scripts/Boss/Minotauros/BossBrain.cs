using DG.Tweening;
using Gwamegi.Code.Core.StatSystem;
using Gwamegi.Code.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Code.Boss
{
    public class BossBrain : Entity
    {
        [SerializeField]private ParticleSystem _moveParticle;
        public Rigidbody2D BossRigid { get { return bossRigid; } }
        private Rigidbody2D bossRigid;
        public BossAnimation BossAnimationCompo { get { return bossAnimation; } }
        private BossAnimation bossAnimation;

        private BossState _currentbossState;
        private List<BossState> _bossStates = new List<BossState>();

        private bool isDie;
        public bool IsDie { get { return isDie; } }

        public StatSO DamageStat { get { return damageStat; } }
        [SerializeField] private StatSO damageStat;

        public void Init()
        {
            bossRigid = GetComponent<Rigidbody2D>();
            bossAnimation = GetComponentInChildren<BossAnimation>();
            GetComponentsInChildren(_bossStates);
            foreach (var item in GetComponentsInChildren<BossInterface>())
            {
                item.Init(this);
            }
            ChangeState(GetState(BossStateType.Patten2));
        }

        public BossState GetState(BossStateType bossType)
        {
            foreach (var item in _bossStates)
                if (item.GetStateType() == bossType) return item;

            return null;
        }

        public void ChangeState(BossState newState)
        {
            if (_currentbossState != null)
                _currentbossState.Exit();

            _currentbossState = newState;
            _currentbossState.Enter();
        }

        private void Update()
        {
            if (_currentbossState != null)
                _currentbossState.UpdateState();
        }

        private void FixedUpdate()
        {
            if (_currentbossState != null)
                _currentbossState.FixedUpdateState();
        }

        public void BossMove(Vector3 position) => StartCoroutine(MoveTime(position));

        private IEnumerator MoveTime(Vector3 position)
        {
            _moveParticle.Play();
            BossAnimationCompo.transform.DOScale(new Vector3(0, 0), 0.5f);
            yield return new WaitForSeconds(0.5f);
            transform.position = position;
            _moveParticle.Play();
            BossAnimationCompo.transform.DOScale(new Vector3(2f, 2f), 0.5f);
        }

        private void Die() => ChangeState(GetState(BossStateType.Death));

        protected override void HandleHit()
        {

        }

        protected override void HandleDead()
        {
            isDie = true;
            Die();
        }
    }
}