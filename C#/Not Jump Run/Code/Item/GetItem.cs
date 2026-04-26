using DG.Tweening;
using UnityEngine;

public class GetItem : MonoBehaviour, IObjectComponent
{
    [SerializeField] private float movePoint;
    [SerializeField] private DisposableItem itemCompo;

    private Player _player;
    private Animator _animator;

    private bool _isInit;
    private bool _isEndGet;

    public void Init(Player player)
    {
        _player = player;
        _isInit = true;
        _animator = GetComponent<Animator>();
        UpDownMove();
    }

    private async void UpDownMove()
    {
        transform.DOMoveY(transform.position.y + movePoint, 1.5f)/*.SetEase(Ease.OutQuint)*/;
        await Awaitable.WaitForSecondsAsync(1.2f, destroyCancellationToken);
        movePoint *= -1;
        UpDownMove();
    }

    private void Update()
    {
        if(_isInit)
            transform.rotation = Quaternion.Euler(_player.transform.eulerAngles);
    }

    private void GameObjectSet() => Destroy(gameObject);

    private void OnTriggerEnter(Collider other)
    {
       
        if(other.gameObject.layer == _player.gameObject.layer && _isEndGet == false)
        {
            _isEndGet = true;
            _animator.Play("GetItem");
            itemCompo.PlusCount(1);
        }

    }
}
