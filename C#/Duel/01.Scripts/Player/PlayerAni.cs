using UnityEngine;

public class PlayerAni : MonoBehaviour, IPlayerComponents
{
    private Animator _playerAnimator;
    private ParticleSystem _smokePartivle;
    public void Initialize(Player player)
    {
        _playerAnimator = GetComponent<Animator>();
        _smokePartivle = GetComponentInChildren<ParticleSystem>();
    }

    public void SmokePlay() => _smokePartivle.Play();

    public void PlayAni(PlayerAniName name)
    {
        switch (name)
        {
            case PlayerAniName.Hit: _playerAnimator.SetTrigger("doHit"); break;
            case PlayerAniName.Die: _playerAnimator.SetTrigger("doDie"); break;
            //case PlayerAniName.Win: _playerAnimator.SetTrigger("doWin"); break;
        }
    }
}

public enum PlayerAniName
{
    Hit,
    Die,
    Win
}