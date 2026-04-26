using UnityEngine;

public class PlayerAnimator : MonoBehaviour, IPlayerComponent
{
    private Player _player;

    private Animator _playerAnimator;

    public void Init(Player player)
    {
        _player = player;
        _playerAnimator = GetComponent<Animator>();
    }
    public void SetParam(int hash, float value) => _playerAnimator.SetFloat(hash, value);
    public void SetParam(int hash, int value) => _playerAnimator.SetInteger(hash, value);
    public void SetParam(int hash, bool value) => _playerAnimator.SetBool(hash, value);
    public void SetParam(int hash) => _playerAnimator.SetTrigger(hash);

    public void SetPlaySpeed(float speed) => _playerAnimator.speed = speed;
}
