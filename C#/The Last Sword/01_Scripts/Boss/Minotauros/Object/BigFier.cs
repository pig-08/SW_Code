using Gwamegi.Code.Combats;
using Gwamegi.Code.Entities;
using UnityEngine;

namespace SW.Code.Boss
{
    public class BigFier : MonoBehaviour
    {
        private OverlapDamageCaster damageCaster;
        private float _damage;
        public void Init(float damage,Entity entity)
        {
            damageCaster = GetComponentInChildren<OverlapDamageCaster>();
            damageCaster.InitCaster(entity);
            _damage = damage;
        }

        public void Hit() => damageCaster.CastDamage(_damage,Vector2.zero,true,false);
    }
}