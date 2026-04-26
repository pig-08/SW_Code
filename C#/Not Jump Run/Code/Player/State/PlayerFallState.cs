using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(Player player, int animationHash) : base(player, animationHash)
    {
    }

    public override void Update()
    {
        if (_player.transform.position.y <= -10)
            _player.ChangeState("DIE");
        else if (_movement.IsGround)
            _player.ChangeState("IDLE");

        _movement.SetMovementDirection(_player.PlayerInput.MovementKey);
    }

    public override void Exit()
    {
        base.Exit();
        _movement.SetStern(false);
    }

}
