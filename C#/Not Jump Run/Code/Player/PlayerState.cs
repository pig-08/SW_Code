using UnityEngine;

public abstract class PlayerState
{
    protected Player _player;
    protected PlayerMovement _movement;
    protected int _animationHash;
    protected PlayerAnimator _playerAnimator;
    protected PlayerAnimatorTrigger _animatorTrigger;

    public PlayerState(Player player, int animationHash)
    {
        _player = player;
        _animationHash = animationHash;
        _movement = player.GetCompo<PlayerMovement>();
        _playerAnimator = player.GetCompo<PlayerAnimator>();
        _animatorTrigger = player.GetCompo<PlayerAnimatorTrigger>();
    }


    public virtual void Enter()
    {
        _playerAnimator.SetParam(_animationHash, true);
    }

    public virtual void Update() 
    {
        if (_player.transform.position.y <= -10)
            _player.ChangeState("DIE");
        else if (_player.PlayerInput.MovementKey != Vector2.zero)
            _player.ChangeState("MOVE");
        else if (_movement.IsGround == false)
            _player.ChangeState("FALL");
    }

    public virtual void FixedUpdate()
    {

    }


    public virtual void Exit() 
    {
        _playerAnimator.SetParam(_animationHash, false);
    }
}
