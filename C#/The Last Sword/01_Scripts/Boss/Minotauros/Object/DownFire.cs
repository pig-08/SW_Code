using Gwamegi.Code.Combats;
using Gwamegi.Code.Entities;
using UnityEngine;

public class DownFire : MonoBehaviour
{
    public bool IsMove { get {return isMove;} }
    private bool isMove;
    private float _moveSpeed = 15f;
    private OverlapDamageCaster damageCaster;
    private float _damage;

    private Rigidbody2D _rd;

    public void Init(float damage, Entity entity)
    {
        isMove = true;
        _rd = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 1f);
        damageCaster = GetComponentInChildren<OverlapDamageCaster>();
        damageCaster.InitCaster(entity);
        _damage = damage;
    }

    public void Hit()
    {
        if(isMove)
            damageCaster.CastDamage(_damage, Vector2.zero, true, false);
    }

    private void Update()
    {
        if (isMove)
            _rd.linearVelocityY = _moveSpeed * -1;

    }
}
