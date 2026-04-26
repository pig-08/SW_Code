using Gwamegi.Code.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Code.Boss
{
    public class BossPatten3State : BossState
    {
        [SerializeField] private GameObject downFire;
        private List<GameObject> downFires = new List<GameObject>();
        public override IEnumerator AttatkTime(float time)
        {
            yield return new WaitForSeconds(time);
            AttackEnd();
        }

        public override void Enter()
        {
            _bossBrain.BossMove(new Vector2(0, 2f));
            _bossBrain.BossAnimationCompo.OnCharging += Attack;
            _bossBrain.BossAnimationCompo.OnAttackEnd += AttackEnd;
            for (int i = 0; i < 13; i++)
            {
                downFires.Add(Instantiate(downFire));
                downFires[i].transform.position = new Vector3(-15 + i * 2.5f, 5.5f);
            }
            StartCoroutine(AttatkTime(0.5f));
        }

        public override void Exit()
        {
            _bossBrain.BossAnimationCompo.OnCharging -= Attack;
            _bossBrain.BossAnimationCompo.OnAttackEnd -= AttackEnd;
            foreach (GameObject go in downFires)
                Destroy(go);

            StopAllCoroutines();
        }

        public override void FixedUpdateState()
        {

        }

        public override BossStateType GetStateType() => BossStateType.Patten3;

        public override void UpdateState()
        {
        }

        private void Attack()
        {
            _bossBrain.BossAnimationCompo.ChangeAnimation(BossAnimationType.attackEnd);
            for (int i = 0; i < downFires.Count; i++)
            {
                DownFire df = downFires[i].GetComponent<DownFire>();
                DownFire dfl = downFires[downFires.Count - 1].GetComponent<DownFire>();
                if (!(df.IsMove))
                {
                    df.Init(_bossBrain.GetCompo<EntityStat>().GetStat(_bossBrain.DamageStat).Value, _bossBrain);
                    dfl.Init(_bossBrain.GetCompo<EntityStat>().GetStat(_bossBrain.DamageStat).Value, _bossBrain);
                    
                    downFires.Remove(downFires[i]);
                    if(downFires.Count != 0)
                        downFires.Remove(downFires[downFires.Count - 1]);
                    return;
                }
            }
            
        }
        private void AttackEnd()
        {
            _bossBrain.BossAnimationCompo.ChangeAnimation(BossAnimationType.attackReady);
            for (int i = 0; i < downFires.Count; i++)
            {
                DownFire df = downFires[i].GetComponent<DownFire>();
                
                if (!(df.IsMove))
                    return;
            }
            _bossBrain.BossAnimationCompo.ChangeAnimation(BossAnimationType.idle);
            _bossBrain.ChangeState(_bossBrain.GetState(BossStateType.Patten1));

        }


    }
}