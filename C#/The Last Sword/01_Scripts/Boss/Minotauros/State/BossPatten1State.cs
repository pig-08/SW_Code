using DG.Tweening;
using Gwamegi.Code.Entities;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace SW.Code.Boss
{
    public class BossPatten1State : BossState
    {
        [SerializeField] private GameObject fire;
        [SerializeField] private GameObject charging;
        private GameObject ch;
        private GameObject ch2;
        private bool isFilp;

        public override void Enter()
        {
            Attack(false);
            _bossBrain.BossAnimationCompo.OnCharging += ChargingStart;
        }

        private IEnumerator StartTime(bool filp)
        {
            yield return new WaitForSeconds(0.5f);
            if (filp)
                _bossBrain.BossMove(new Vector2(-14f, -2.2f));
            else
                _bossBrain.BossMove(new Vector2(11.5f, -2.2f));
            _bossBrain.BossAnimationCompo.Filp(filp);
            isFilp = filp;
            _bossBrain.BossAnimationCompo.ChangeAnimation(BossAnimationType.attackReady);
        }

        public override void Exit()
        {
            _bossBrain.BossAnimationCompo.OnCharging -= ChargingStart;
            Destroy(ch);
            Destroy(ch2);
            StopAllCoroutines();
        }

        public override void FixedUpdateState()
        {

        }

        public override BossStateType GetStateType() => BossStateType.Patten1;

        public override void UpdateState()
        {
            
        }

        private void Attack(bool filp) => StartCoroutine(StartTime(filp));


        public override IEnumerator AttatkTime(float time)
        {
            yield return new WaitForSeconds(time);

            _bossBrain.BossAnimationCompo.ChangeAnimation(BossAnimationType.attackEnd);
            yield return new WaitForSeconds(0.2f);
            GameObject fr = Instantiate(fire);
            fr.GetComponent<Fire>().Init(isFilp,true,ch2.transform.position, _bossBrain.GetCompo<EntityStat>().GetStat(_bossBrain.DamageStat).Value, _bossBrain);
            if (isFilp)
                fr.transform.rotation = Quaternion.Euler(0, -180, 35f);
            else
                fr.transform.rotation = Quaternion.Euler(0, 0, 35f);
            Destroy(ch);
            Destroy(ch2);
            Instantiate(fire).GetComponent<Fire>().Init(isFilp,false,_bossBrain.transform.position, _bossBrain.GetCompo<EntityStat>().GetStat(_bossBrain.DamageStat).Value, _bossBrain);
            if (isFilp)
                _bossBrain.ChangeState(_bossBrain.GetState(BossStateType.Patten2));
            else
                Attack(true);
        }

        private void ChargingStart()
        {
            ch = Instantiate(charging);
            ch2 = Instantiate(charging);
            if (isFilp)
            {
                ch.transform.position = new Vector3(-16.5f, 4.35f);
                ch.transform.eulerAngles = new Vector3(0, -180, 0);
                ch2.transform.position = new Vector3(-17f, 9f);
                ch2.transform.rotation = Quaternion.Euler(0, -180, 35f);
            }
            else
            {
                ch.transform.position = new Vector3(14f, 4.35f);
                ch.transform.eulerAngles = Vector3.zero;
                ch2.transform.position = new Vector3(12f, 9f);
                ch2.transform.rotation = Quaternion.Euler(0, 0, 35f);
            }
            
            
            ch.transform.DOScale(new Vector3(2,2), 0.5f);
            ch2.transform.DOScale(new Vector3(2, 2), 0.5f);

            
            StartCoroutine(AttatkTime(0.8f));
        }
    }
}