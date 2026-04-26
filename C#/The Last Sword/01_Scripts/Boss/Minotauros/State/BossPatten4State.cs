using DG.Tweening;
using DG.Tweening.Core;
using Gwamegi.Code.Combats;
using Gwamegi.Code.Entities;
using System.Collections;
using UnityEngine;

namespace SW.Code.Boss
{
    public class BossPatten4State : BossState
    {
        [SerializeField] private GameObject bigFire;
        [SerializeField] private GameObject firePosition;
        private GameObject fp;
        private GameObject bf;
        private bool isFilp;
        public override IEnumerator AttatkTime(float time)
        {
            yield return new WaitForSeconds(0.5f);
            bf = Instantiate(bigFire);
            bf.GetComponent<BigFier>().Init(_bossBrain.GetCompo<EntityStat>().GetStat(_bossBrain.DamageStat).Value - 10, _bossBrain);
            bf.transform.position = _bossBrain.transform.position;
            bf.transform.DOScale(new Vector3(7, 7), 5f);
            float value = 1;
            DOTween.To(() => value, x => value = x, 6f, 5f);
            while (value != 6)
            {
                if(bf != null)
                    bf.GetComponentInChildren<DamageCaster>().SetSize(new Vector2(value, 0));
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(time);
            bf.transform.DOScale(new Vector3(0, 0), 2.5f);
            DOTween.To(() => value, x => value = x, 0f, 2.5f);
            while (value != 0)
            {
                if (bf != null)
                    bf.GetComponentInChildren<DamageCaster>().SetSize(new Vector2(value, 0));
                yield return new WaitForSeconds(0.2f);
            }
            Destroy(bf);
            if (isFilp)
            {
                _bossBrain.ChangeState(_bossBrain.GetState(BossStateType.Patten3));
                fp.transform.DOScale(new Vector3(0, 0), 0.3f);
                Destroy(fp,0.3f);
            }
            else
                Attack(true);
        }

        public override void Enter()
        {
            StartCoroutine(PlusFire());
            Attack(false);
        }

        private IEnumerator PlusFire()
        {
            fp = Instantiate(firePosition);
            fp.transform.position = new Vector3(-0.35f, -3.4f);
            fp.transform.DOScale(new Vector3(1.5f, 1.5f), 0.5f);
            yield return new WaitForSeconds(0.5f);
            GameObject bf = Instantiate(bigFire, fp.transform);
            bf.transform.DOScale(new Vector3(3.3f,3.3f),3f);
            bf.GetComponent<BigFier>().Init(_bossBrain.GetCompo<EntityStat>().GetStat(_bossBrain.DamageStat).Value - 10, _bossBrain);
            float value = 1;
            DOTween.To(() => value, x => value = x, 4f, 5f);
            while (value != 4)
            {
                bf.GetComponentInChildren<DamageCaster>().SetSize(new Vector2(value, 0));
                yield return new WaitForSeconds(0.2f);
            }
        }

        public override void Exit()
        {
            Destroy(fp);
            Destroy(bf);
            StopAllCoroutines();
        }

        public override void FixedUpdateState()
        {
        }

        public override BossStateType GetStateType() => BossStateType.Patten4;
        public override void UpdateState()
        {

        }

        private void Attack(bool filp)
        {
            if (filp)
                _bossBrain.BossMove(new Vector2(-14f, -2.2f));
            else
                _bossBrain.BossMove(new Vector2(11.5f, -2.2f));
            isFilp = filp;
            _bossBrain.BossAnimationCompo.Filp(filp);
            StartCoroutine(AttatkTime(2f));
        }
    }
}