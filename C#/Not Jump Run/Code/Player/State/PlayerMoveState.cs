using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player player, int animationHash) : base(player, animationHash)
    {
    }

    public override void Update()
    {
        if (_player.transform.position.y <= -10)
            _player.ChangeState("DIE");
        else if(_player.PlayerInput.MovementKey == Vector2.zero)
            _player.ChangeState("IDLE");
        else if (_movement.IsGround == false)
            _player.ChangeState("FALL");
    }

    public override void FixedUpdate()
    {
        _movement.SetMovementDirection(_player.PlayerInput.MovementKey);
    }
}
