using UnityEngine;

public class JumpPadObstruction : MonoBehaviour, IObjectComponent
{
    [SerializeField] private SoundSo jumpSound;
    [SerializeField] private float pushPower;
    [SerializeField] private Vector3 checkRange;
    [SerializeField] private Vector3 checkPoint;
    [SerializeField] private LayerMask playerMask;

    private Animator _jumpPadAnimator;
    private Player _player;
    private Rigidbody _playerRigidbody;
    private ShoesItem _playerShoesItem;

    private float _currentTime;
    private float _goalsTime = 0.5f;

    public void Init(Player player)
    {
        _player = player;
        _playerRigidbody = _player.GetComponent<Rigidbody>();
        _jumpPadAnimator = GetComponent<Animator>();
        _playerShoesItem = _player.GetCompo<Items>().GetComp<ShoesItem>();
    }

    private void Update()
    {
        if (_currentTime >= _goalsTime)
            Jump();
        else
            _currentTime += Time.deltaTime;
    }

    private void Jump()
    {
        if (Physics.CheckBox(checkPoint + transform.position, checkRange * 0.5f, Quaternion.identity, playerMask)
            && _playerShoesItem.GetPick() == false)
        {
            _playerRigidbody.linearVelocity = Vector3.zero;
            _playerRigidbody.AddForce(transform.TransformDirection(Vector3.up) * pushPower,ForceMode.Impulse);
            _currentTime = 0;
            SoundManager.sound.PlaySound(jumpSound);
            _jumpPadAnimator.PlayInFixedTime("JumpPad",0,0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireCube(checkPoint + transform.position, checkRange);
    }


}
