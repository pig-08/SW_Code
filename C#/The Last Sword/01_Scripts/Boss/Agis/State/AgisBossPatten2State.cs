using Gwamegi.Code.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Code.Boss
{
    public class AgisBossPatten2State : BossState
    {
        [SerializeField] private GameObject razer;
        [SerializeField] private GameObject attackIdle;

        private AgisBossBrain _aBossBrain;

        private List<Razer> razers = new List<Razer>();
        private AttackIdle _currentattackIdle;
        public override IEnumerator AttatkTime(float time)
        {
            yield return null;
        }

        public override void Enter()
        {
            if (_aBossBrain == null)
                _aBossBrain = (AgisBossBrain)_bossBrain;

            _aBossBrain.AgisBossAnimationCompo.ChangeTrueAnimation(AgisBossAnimationType.attack2);

            _aBossBrain.AgisBossAnimationCompo.OnAttack2 += PlayCurrentattackIdle;
            _aBossBrain.AgisBossAnimationCompo.OnAttack2Hit += PlayRazer;
            _aBossBrain.AgisBossAnimationCompo.OnAttack2End += EndAttack2;
        }


        public override void Exit()
        {
            _aBossBrain.AgisBossAnimationCompo.OnAttack2 -= PlayCurrentattackIdle;
            _aBossBrain.AgisBossAnimationCompo.OnAttack2Hit -= PlayRazer;
            _aBossBrain.AgisBossAnimationCompo.OnAttack2End -= EndAttack2;
        }

        public override void FixedUpdateState()
        {
        }

        public override BossStateType GetStateType() => BossStateType.Patten2;

        public override void UpdateState()
        {
        }
        private void PlayCurrentattackIdle()
        {
            _currentattackIdle = Instantiate(attackIdle).GetComponent<AttackIdle>();
            _currentattackIdle.Init(new Vector2(0, 6.12f));
        }
        private void PlayRazer()
        {
            for(int i = 0; i < 5; i++)
            {
                razers.Add(Instantiate(razer).GetComponent<Razer>());
                int razerrotation = 0;
                switch (i)
                {
                    case 0:
                        razerrotation = 0;
                        break;
                    case 1:
                        razerrotation = 35;
                        break;
                    case 2:
                        razerrotation = -35;
                        break;
                    case 3:
                        razerrotation = 65;
                        break;
                    case 4:
                        razerrotation = -65;
                        break;
                }

                razers[i].transform.position = _currentattackIdle.transform.position;
                razers[i].Init(_aBossBrain.GetCompo<EntityStat>().GetStat(_bossBrain.DamageStat).Value, _aBossBrain, razerrotation);
            }
        }

        private void EndAttack2()
        {
            StartCoroutine(EndTime());
        }

        private IEnumerator EndTime()
        {
            yield return new WaitForSeconds(0.8f);
            foreach (Razer razer in razers)
            {
                razer.Delete(_currentattackIdle.gameObject);
            }
        }

    }
}