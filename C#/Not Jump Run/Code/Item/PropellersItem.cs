using DG.Tweening;
using UnityEngine;

public class PropellersItem : DisposableItem
{
    private Rigidbody _playerRigid;
    private PlayerMovement _playerMovement;

    private bool _isOnPropellers;

    public override void Init(Player player)
    {
        base.Init(player);
        _playerRigid = player.GetComponent<Rigidbody>();
        _playerMovement = player.GetCompo<PlayerMovement>();
    }

    private void Update()
    {
        if(_isOnPropellers)
        {
            transform.Rotate(Vector3.forward * 100f * Time.deltaTime);
        }
    }

    public async override void PickUp()
    {
        if (_isOnPropellers)
        {
            _quickSlotItem.NotItem();
            return;
        }

        base.PickUp();
        await Awaitable.WaitForSecondsAsync(0.1f, destroyCancellationToken);
        if (GetZeroCount() && _isOnPropellers == false)
        {
            _playerMovement.SetStern(false);
            _playerRigid.linearVelocity = Vector3.zero;
            _isOnPropellers = true;
            transform.DOScale(Vector3.one * 3,0.1f);
            _playerRigid.useGravity = false;
            MinusCount(1);
        }
        _quickSlotItem.NotItem();

        if(_isOnPropellers)
        {
            await Awaitable.WaitForSecondsAsync(0.1f, destroyCancellationToken);
            transform.DOScale(Vector3.zero, 4f);
            await Awaitable.WaitForSecondsAsync(4f, destroyCancellationToken);
            _playerRigid.useGravity = true;
            _isOnPropellers = false;
        }
    }
}
