using UnityEngine;

public class ShoesItem : ItemComponent
{
    [SerializeField] private Animator[] shoesAnimatorList;
    
    private PlayerMovement _playerMovement;
    private PlayerAnimator _playerAnimator;
    private Player _player;

    public override void Init(Player player)
    {
        _player = player;
        _playerMovement = player.GetCompo<PlayerMovement>();
        _playerAnimator = player.GetCompo<PlayerAnimator>();

    }

    public override void PickUp()
    {
        _playerAnimator.SetPlaySpeed(0.5f);
        _playerMovement.SetMoveSpeed(-4);
        base.PickUp();
        foreach (Animator ani in shoesAnimatorList)
            ani.Play("Create");
    }

    public override void PickDown()
    {
        _playerAnimator.SetPlaySpeed(1f);
        _playerMovement.SetExistingSpeed();
        base.PickDown();
        foreach (Animator ani in shoesAnimatorList)
            ani.Play("Delete");
    }

    public bool GetPick()
    {
        return isPickup;
    }

}
