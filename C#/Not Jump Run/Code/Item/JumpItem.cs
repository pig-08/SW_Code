using UnityEngine;

public class JumpItem : DisposableItem
{
    [SerializeField] private float jumpPower;

    private Rigidbody _playerRigid;
    private PlayerMovement _playerMovement;

    public override void Init(Player player)
    {
        base.Init(player);
    }
    private void Start()
    {
        _playerRigid = _player.GetComponent<Rigidbody>();
        _playerMovement = _player.GetCompo<PlayerMovement>();
    }

    public override async void PickUp()
    {
        base.PickUp();
        await Awaitable.WaitForSecondsAsync(0.1f, destroyCancellationToken);
        if (GetZeroCount())
        {
            _playerMovement.StopImmediately();
            _playerRigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            MinusCount(1);
        }
        _quickSlotItem.NotItem();
    }
}
