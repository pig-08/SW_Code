using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerMovement : MonoBehaviour, IPlayerComponent
{
    public UnityEvent<bool> OnChangeStern;

    [Header("Speed")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float rotaionSpeed = 1.5f;

    [Header("Ground")]
    [SerializeField] private float maxGroundDistance = 1f;
    [SerializeField] private Vector3 size;
    [SerializeField] private LayerMask groundLayer;

    [Header("Move")]
    [SerializeField] private Transform playerRotaion;
    [SerializeField] private Rigidbody playerRidid;

    [Header("Side")]
    [SerializeField] private Vector3 sideSize;
    [SerializeField] private Vector3 sidePoint;

    private float _currtMoveSpeed;

    private bool _isStern;
    private bool _isSideGround;
    
    public bool IsGround { get; private set; }
    private Player _player;

    public void SetStern(bool isStern)
    {
        _isStern = isStern;
        OnChangeStern?.Invoke(isStern);
    }

    public void Init(Player player)
    {
        _player = player;
        _currtMoveSpeed = moveSpeed;
    }

    public void SetMoveSpeed(float plus)
    {
        _currtMoveSpeed += plus;
        _currtMoveSpeed = Mathf.Clamp(_currtMoveSpeed, 0.5f, 50f);
    }

    public void SetExistingSpeed()
    {
        _currtMoveSpeed = moveSpeed;
    }

    public void SetMovementDirection(Vector2 movementInput)
    {
        if (IsGround == false && _isSideGround) return;

        if (_isStern == false)
        {
            Vector3 forward = _player.transform.TransformDirection(Vector3.forward) * movementInput.y;
            Vector3 right = _player.transform.TransformDirection(Vector3.right) * movementInput.x;

            Vector3 moveDir = (forward + right).normalized;

            playerRidid.linearVelocity = new Vector3(moveDir.x * _currtMoveSpeed,
                    playerRidid.linearVelocity.y,
                    moveDir.z * _currtMoveSpeed);
        }
    }

    private void Update()
    {
        GetIsGround();
        CheckSide();
    }

    public void StopImmediately()
    {
        playerRidid.linearVelocity = new Vector3(0, playerRidid.linearVelocity.y, 0);
    }

    private void GetIsGround()
    {
        IsGround = Physics.BoxCast(transform.position, size * 0.5f, Vector3.down
            ,Quaternion.identity, maxGroundDistance, groundLayer);
    }

    private void CheckSide()
    {
        if (IsGround) return;

        _isSideGround = Physics.CheckBox(transform.position + sidePoint, sideSize * 0.5f, Quaternion.identity, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.down * maxGroundDistance, size);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + sidePoint, sideSize);
    }

}
