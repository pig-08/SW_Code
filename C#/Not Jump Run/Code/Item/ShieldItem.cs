using System;
using UnityEngine;

public class ShieldItem : ItemComponent
{
    private Player _player;
    private Animator _shieldAnimator;
    private BoxCollider _boxCollider;

    public override void Init(Player player)
    {
        _player = player;
        _shieldAnimator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
        IsDisposable = false;
    }
    public override void PickUp()
    {
        if (isPickup) return;


        base.PickUp();
        _shieldAnimator.Play("Create");
    }

    public override void PickDown()
    {
        base.PickDown();
        _shieldAnimator.Play("Delete");
    }
    public bool GetPick()
    {
        return isPickup;
    }

}
