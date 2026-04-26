using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private ParticleSystem popPartucle;
    [SerializeField] private SoundSo checkPointSound;
    private int _currCheckPoint;
    private Player _player;

    private bool _isInit;

    public void Init(Player player, int point)
    {
        _player = player;
        _isInit = true;
        _currCheckPoint = point;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == _player?.gameObject.layer && _isInit)
        {
            SoundManager.sound.PlaySound(checkPointSound);
            PlayerPrefs.SetInt("CheckPointIndex", _currCheckPoint);
            popPartucle.Play();
            _isInit = false;
        }
    }
}
