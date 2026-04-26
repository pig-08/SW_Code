using Gwamegi.Code.Combats;
using Gwamegi.Code.Entities;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace SW.Code.Boss
{
    public class Fire : MonoBehaviour
    {
        private bool _moveRight;
        private bool _moveDown;
        private float _moveSpeed = 20f;
        private Rigidbody2D _rd;
        private OverlapDamageCaster damageCaster;
        private float _damage;
        public void Init(bool right,bool isDown, Vector3 firePosition, float damage, Entity entity)
        {
            _moveRight = right;
            _moveDown = isDown;
            if (right)
            {
                transform.position = new Vector3(firePosition.x + 3, firePosition.y);
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else
            {
                transform.position = new Vector3(firePosition.x - 3,firePosition.y);
                transform.eulerAngles = Vector3.zero;
            }
            damageCaster = GetComponentInChildren<OverlapDamageCaster>();
            damageCaster.InitCaster(entity);
            _damage = damage;
            _rd = GetComponent<Rigidbody2D>();
            Destroy(gameObject,3f);
        }

        public void Hit() => damageCaster.CastDamage(_damage, Vector2.zero, true, false);

        private void Update()
        {
            if (_moveDown)
            {
                if (_moveRight)
                {
                    _rd.linearVelocityX = _moveSpeed * 1;
                    _rd.linearVelocityY = (_moveSpeed / 2) * -1;
                }
                else
                {
                    _rd.linearVelocityX = _moveSpeed * -1;
                    _rd.linearVelocityY = (_moveSpeed / 2) * -1;
                }
            }
            else
            {
                if (_moveRight)
                    _rd.linearVelocityX = _moveSpeed * 1;
                else
                    _rd.linearVelocityX = _moveSpeed * -1;
            }
        }
    }
}