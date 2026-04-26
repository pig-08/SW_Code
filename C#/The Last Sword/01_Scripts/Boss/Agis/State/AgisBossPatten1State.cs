using Gwamegi.Code.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Code.Boss
{
    public class AgisBossPatten1State : BossState
    {
        [SerializeField] private GameObject razer;
        [SerializeField] private GameObject attackIdle;
        private List<Razer> razers;
        private List<AttackIdle> attackIdles;
        private AgisBossBrain _aBossBrain;


        public override IEnumerator AttatkTime(float time)
        {
            yield return new WaitForSeconds(time);

            for (int i = 0; i < 7; i++)
            {
                razers[i].Init(_aBossBrain.GetCompo<EntityStat>().GetStat(_bossBrain.DamageStat).Value, _aBossBrain,0);
                yield return new WaitForSeconds(time);
            }
            for (int i = 0; i < 7; i++)
            {
                razers[i].Delete(attackIdles[i].gameObject);
                yield return new WaitForSeconds(time);
            }
            _aBossBrain.ChangeState(_aBossBrain.GetState(BossStateType.Patten2));
        }
        public override void Enter()
        {
            if (_aBossBrain == null)
                _aBossBrain = (AgisBossBrain)_bossBrain;

            razers = new List<Razer>();
            attackIdles = new List<AttackIdle>();

            for (int i = 0; i < 7; i++)
            {
                GameObject rz = Instantiate(razer);
                rz.transform.position = new Vector3(rz.transform.position.x + 5 * i, rz.transform.position.y);
                razers.Add(rz.GetComponent<Razer>());
                attackIdles.Add(Instantiate(attackIdle.GetComponent<AttackIdle>()));
                attackIdles[i].Init(rz.transform.position);
            }
            _aBossBrain.AgisBossAnimationCompo.ChangeTrueAnimation(AgisBossAnimationType.attack1);
            _aBossBrain.AgisBossAnimationCompo.OnAttack1 += Attatk;
        }

        private void Attatk() => StartCoroutine(AttatkTime(0.5f));

        public override void Exit()
        {

            for (int i = 0; i < 7; i++)
            {
                if (attackIdles[i] != null)
                    Destroy(attackIdles[i].gameObject);
                if (razers[i] != null)
                Destroy(razers[i].gameObject);
            }
        }

        public override void FixedUpdateState()
        {
        }

        public override BossStateType GetStateType() => BossStateType.Patten1;

        public override void UpdateState()
        {

        }
    }
}