using DG.Tweening;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class HammerObstruction : MonoBehaviour, IObjectComponent
{
    [SerializeField] private SoundSo hammerSound;
    [SerializeField] private SoundSo hammerHitSound;
    [SerializeField] private Transform soundPoint;

    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private Transform axis;
    [SerializeField] private float rotationRange;
    [SerializeField] private float force;
    [SerializeField] private float point = 1;

    [Header("Check")]
    [SerializeField] private Renderer heade;
    [SerializeField] private Vector3 checkRange;
    [SerializeField] private Vector3 checkPoint;
    [SerializeField] private LayerMask playerMask;


    private Player _player;
    private Rigidbody _playerRigid;

    private float _currentTime;
    private float _goalsTime = 0.5f;

    public void Init(Player player)
    {
        _player = player;
        _playerRigid = _player.GetComponent<Rigidbody>();
        MoveHammer();
    }

    private async void MoveHammer()
    {
        SoundManager.sound.PlaySound(hammerSound, soundPoint.position);
        axis.DOLocalRotate(new Vector3(0, 0, rotationRange * point),2.5f).SetEase(Ease.InOutExpo);
        await Awaitable.WaitForSecondsAsync(2.5f, destroyCancellationToken);
        point *= -1;
        MoveHammer();
    }

    private void Update()
    {

        if (_currentTime >= _goalsTime)
            Push();
        else
            _currentTime += Time.deltaTime;


    }

    private void Push()
    {
        if (Physics.CheckBox(heade.bounds.center + checkPoint, checkRange * 0.5f
            , Quaternion.Euler(axis.eulerAngles), playerMask) && axis.eulerAngles.z < (point < 0 ? 50 : 300))
        {
            hitParticle.transform.position = _player.transform.position;
            hitParticle.transform.DOMoveY(hitParticle.transform.position.y + 1,0);
            hitParticle.Play();

            _currentTime = 0;
            SoundManager.sound.PlaySound(hammerHitSound);

            _playerRigid.linearVelocity = Vector3.zero;
            _playerRigid.AddForce(transform.TransformDirection(Vector3.right) * point * force, ForceMode.Impulse);
            _playerRigid.AddForce(transform.TransformDirection(Vector3.up) * (force / 2), ForceMode.Impulse);
            
            _player.GetCompo<PlayerMovement>().SetStern(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Matrix4x4 oldGizmosMatrix = Gizmos.matrix;
        Quaternion rot = Quaternion.Euler(axis.eulerAngles);
        Gizmos.matrix = Matrix4x4.TRS(heade.bounds.center + checkPoint, rot, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, checkRange);
        Gizmos.matrix = oldGizmosMatrix;
    }
}
