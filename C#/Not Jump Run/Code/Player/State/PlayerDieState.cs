using UnityEngine;

public class PlayerDieState : PlayerState
{
    public PlayerDieState(Player player, int animationHash) : base(player, animationHash)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _player.PlayerInput.SetPlayerInput(false);
        _player.SetGameOver(true);
    }
    public override void Update()
    {

    }
}
