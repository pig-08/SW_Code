using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private ParticleSystem boomParticle;
    [SerializeField] private SoundSo ballSound;
    
    private SoundManager _soundManager;
    
    private Player _player;
    
    private Rigidbody _playerRigid;
    private Rigidbody _rigidbody;

    private ShieldItem _shieldItem;
    private Vector3 _pushPoint;

    private bool _isBack;
    public void Init(Player player,float fireForce, float lifeTime)
    {
        _player = player;
        _shieldItem = _player.GetCompo<Items>().GetComp<ShieldItem>();
        _soundManager = SoundManager.sound;

        _playerRigid = _player.GetComponent<Rigidbody>();
        _rigidbody = GetComponent<Rigidbody>();
        _pushPoint = transform.TransformDirection(Vector3.up);


        _rigidbody.AddForce(_pushPoint * fireForce, ForceMode.Impulse);
        Destroy(gameObject, lifeTime);
    }

    private async void AddPlayerBack(GameObject hitObject, Vector3 normal)
    {
        _isBack = true;

        _soundManager.PlaySound(ballSound, transform.position);
        transform.localScale = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
        boomParticle.transform.position = normal;

        boomParticle.Play();
        
        if (hitObject != _shieldItem.gameObject)
        {
            _player.GetCompo<PlayerMovement>().StopImmediately();
            _playerRigid.AddForce(_pushPoint * 75, ForceMode.Impulse);
            _playerRigid.AddForce(_player.transform.up * 50, ForceMode.Impulse);
            _player.GetCompo<PlayerMovement>().SetStern(true);
        }
        await Awaitable.WaitForSecondsAsync(0.5f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _player?.gameObject.layer && _isBack == false)
        {
            AddPlayerBack(collision.gameObject , collision.contacts[0].normal);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool ishit = other.gameObject.layer == _player?.gameObject.layer || other.gameObject.layer == _shieldItem.gameObject.layer;

        if (ishit && _isBack == false && _shieldItem.GetPick())
        {
            Vector3 normal = (transform.position - other.ClosestPoint(transform.position)).normalized;
            AddPlayerBack(other.gameObject, normal);
        }
    }

}
