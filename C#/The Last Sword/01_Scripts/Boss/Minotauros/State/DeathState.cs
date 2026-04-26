using System.Collections;
using UnityEngine;

namespace SW.Code.Boss
{
    public class DeathState : BossState
    {
        [SerializeField] private LayerMask dielayer;
        public override IEnumerator AttatkTime(float time)
        {
            throw new System.NotImplementedException();
        }

        public override void Enter()
        {
            _bossBrain.BossAnimationCompo.ChangeAnimation(BossAnimationType.death);
            _bossBrain.gameObject.layer = dielayer;
        }

        public override void Exit()
        {
        }

        public override void FixedUpdateState()
        {
        }

        public override BossStateType GetStateType() => BossStateType.Death;

        public override void UpdateState()
        {

        }

    }
}