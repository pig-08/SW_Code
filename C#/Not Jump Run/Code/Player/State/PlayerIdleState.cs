using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, int animationHash) : base(player, animationHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.GetCompo<PlayerMovement>().StopImmediately();
    }

    public override void Update()
    {
        base.Update();

    }
}
