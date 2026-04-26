using Gwamegi.Code.Combats;
using Gwamegi.Code.Entities;
using UnityEngine;

namespace SW.Code.Boss
{
    public class RazerDamageCaster : DamageCaster
    {
        [SerializeField] private Vector2 _damageBoxSize;
        [SerializeField] private float _rotationAngle = 0;

        private Collider2D[] _hitResults;

        public override void InitCaster(Entity owner)
        {
            base.InitCaster(owner);
            _hitResults = new Collider2D[maxHitCount];
        }

        private void Update()
        {

        }

        public override bool CastDamage(float damage, Vector2 knockback, bool isPowerAttack, bool isHitCritical)
        {
            int cnt = Physics2D.OverlapBox(transform.position, _damageBoxSize, _rotationAngle, contactFilter, _hitResults);
            for (int i = 0; i < cnt; i++)
            {
                Vector2 direction = (_hitResults[i].transform.position - _owner.transform.position).normalized;

                knockback.x *= Mathf.Sign(direction.x);
                if (_hitResults[i].TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(damage, direction, knockback, isPowerAttack, isHitCritical, _owner);
                }
            }

            return cnt > 0;
        }


        public override void SetOffset(Vector2 position)
        {
            transform.localPosition = position;
        }

        public void SetAngle(float value)
        {
            _rotationAngle = value;
        }

        public override void SetSize(Vector2 value)
        {
            _damageBoxSize = value;
        }

        public override void ResetSize()
        {
            _damageBoxSize = new Vector2(1, 1);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.color = new Color(0.7f, 0.7f, 0, 1);
            Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.AngleAxis(_rotationAngle, new Vector3(0,0,1)), Vector2.one);
            Gizmos.DrawWireCube(Vector3.zero, _damageBoxSize);
            Gizmos.matrix = oldMatrix;
        }
#endif
    }
}
