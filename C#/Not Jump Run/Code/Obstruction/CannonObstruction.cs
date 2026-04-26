using UnityEngine;

public class CannonObstruction : MonoBehaviour, IObjectComponent
{
    [Header("시간")]
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;

    [Header("오브젝트")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float fireForce;
    [SerializeField] private float lifeTime;

    private Player _player;

    private float _currentTime;
    private float _goalsTime;

    private bool isInit;
    public void Init(Player player)
    {
        _player = player;
        isInit = true;
        _goalsTime = Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        if (isInit == false) return;

        if(_currentTime >= _goalsTime)
        {
            Instantiate(ballPrefab, firePoint).GetComponent<Ball>().Init(_player, fireForce, lifeTime);
            _currentTime = 0;
            _goalsTime = Random.Range(minTime, maxTime);
        }
        else
            _currentTime += Time.deltaTime;
    }
}
