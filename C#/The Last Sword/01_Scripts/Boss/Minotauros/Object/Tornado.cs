using Gwamegi.Code.Combats;
using Gwamegi.Code.Entities;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    private float moveSpeed = 15f;
    private Rigidbody2D _rd;
    private bool IsMoveSpeed;
    private bool _moveRight;
    private OverlapDamageCaster damageCaster;
    private float _damage;
    public void Init(float damage, bool right, Entity entity)
    {
        _rd = GetComponent<Rigidbody2D>();
        _moveRight = right;
        IsMoveSpeed = true;
        damageCaster = GetComponentInChildren<OverlapDamageCaster>();
        damageCaster.InitCaster(entity);
        _damage = damage;

    }

    public void Hit()
    {
        if (IsMoveSpeed)
            damageCaster.CastDamage(_damage, Vector2.zero, true, false);
    }

    private void Update()
    {
        if(IsMoveSpeed)
        {
            if(_moveRight)
                _rd.linearVelocityX = moveSpeed * 1;
            else
                _rd.linearVelocityX = moveSpeed * -1;
        }
    }
}
