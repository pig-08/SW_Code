using DG.Tweening;
using Gwamegi.Code.Combats;
using Gwamegi.Code.Entities;
using System.Collections;
using UnityEngine;

namespace SW.Code.Boss
{
    public class Razer : MonoBehaviour
    {
        private RazerDamageCaster damageCaster;
        private float _damage;
        private bool isStart;
        public bool IsStart { get { return isStart; } }
        public void Init(float damage, Entity entity,float rotation)
        {
            isStart = true;
            
            damageCaster = GetComponentInChildren<RazerDamageCaster>();
            damageCaster.InitCaster(entity);
            if (rotation != 0)
            {
                transform.eulerAngles = new Vector3(0, 0, rotation);
                if (rotation < 0)
                    damageCaster.SetAngle(90 - (-1 * rotation));
                else
                    damageCaster.SetAngle(90 - rotation);
            }
            _damage = damage;
            transform.DOScaleX(100, 0.5f);
            StartCoroutine(DamageCasterSizeSet());
        }

        public void Delete(GameObject deldet)
        {
            transform.DOScaleX(0, 0.5f);
            StartCoroutine(DeleteTime(deldet));
        }

        public IEnumerator DamageCasterSizeSet()
        {
            damageCaster.SetSize(new Vector2(2.2f, transform.localScale.x / 2));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(DamageCasterSizeSet());
        }

        private IEnumerator DeleteTime(GameObject deldet)
        {
            yield return new WaitForSeconds(0.5f);
            if (deldet.GetComponent<AttackIdle>() != null)
                deldet.GetComponent<AttackIdle>().DeleteAniStart();
            Destroy(gameObject);
        }
        public void Hit()
        {
            if (isStart)
                damageCaster.CastDamage(_damage, Vector2.zero, true, false);
        }
    }
}