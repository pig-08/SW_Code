using UnityEngine;

public class AttackIdle : MonoBehaviour
{
    private Animator _animator;
    public void Init(Vector2 dir)
    {
        transform.position = dir;
        _animator = GetComponent<Animator>();
        _animator.Play("AttackIdleSpawnEffect");
    }



    public void DeleteAniStart() => _animator.Play("AttackIdleEffectEnd");

    public void Delete() => Destroy(this.gameObject);
}

