using System.Collections;
using UnityEngine;

namespace SW.Code.Boss
{
    public abstract class BossState : MonoBehaviour, BossInterface
    {
        protected BossBrain _bossBrain;
        public void Init(BossBrain brain)
        {
            _bossBrain = brain;
        }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void UpdateState();

        public abstract void FixedUpdateState();

        public abstract BossStateType GetStateType();

        public abstract IEnumerator AttatkTime(float time);
    }
}