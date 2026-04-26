using DG.Tweening;
using Gwamegi.Code.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Code.Boss
{
    public class BossPatten2State : BossState
    {
        [SerializeField] private GameObject Tornado;
        [SerializeField] private GameObject BackFire;
        private GameObject to;
        private GameObject to2;
        private GameObject bf;

        public override void Enter()
        {
            _bossBrain.BossAnimationCompo.ChangeAnimation(BossAnimationType.idle);
            _bossBrain.BossMove(new Vector2(0, 2f));
            TornadoTime();
        }

        public override void Exit()
        {
            Destroy(to);
            Destroy(to2);
            Destroy(bf);
            StopAllCoroutines();
        }

        public override void FixedUpdateState()
        {
        }

        public override BossStateType GetStateType() => BossStateType.Patten2;

        public override IEnumerator AttatkTime(float time)
        {
            yield return new WaitForSeconds(0.5f);
            CreateObject();
            yield return new WaitForSeconds(time);
            to.GetComponent<Tornado>().Init(_bossBrain.GetCompo<EntityStat>().GetStat(_bossBrain.DamageStat).Value,true,_bossBrain);
            yield return new WaitForSeconds(0.2f);
            to2.GetComponent<Tornado>().Init(_bossBrain.GetCompo<EntityStat>().GetStat(_bossBrain.DamageStat).Value,false,_bossBrain);
            Destroy(to, time);
            yield return new WaitForSeconds(time);
            bf.transform.DOScale(new Vector3(0, 0), time);
            yield return new WaitForSeconds(2.5f);
            _bossBrain.ChangeState(_bossBrain.GetState(BossStateType.Patten4));
        }
        public override void UpdateState()
        {

        }

        private void TornadoTime() => StartCoroutine(AttatkTime(3.5f));

        private void CreateObject()
        {
            bf = Instantiate(BackFire, _bossBrain.transform);
            bf.transform.DOScale(new Vector3(3, 3), 2.5f);
            to = Instantiate(Tornado);
            to2 = Instantiate(Tornado);
            to.transform.position = new Vector3(-17.25f, -0.7f);
            to.transform.DOScale(new Vector3(2f, 3.5f), 2.5f);
            to2.transform.position = new Vector3(14f, -0.7f);
            to2.transform.DOScale(new Vector3(2f, 3.5f), 2.5f);
        }


    }
}