using DG.Tweening;
using UnityEngine;

public class MoveObstruction : MonoBehaviour, IObjectComponent
{
    [SerializeField] private Vector3[] movePoints;
    [SerializeField] private float moveTime;

    public void Init(Player player)
    {
        Move(0);
    }

    private async void Move(int point)
    {
        transform.DOMove(movePoints[point],moveTime).SetEase(Ease.Unset);
        await Awaitable.WaitForSecondsAsync(moveTime, destroyCancellationToken);
        Move((++point) % movePoints.Length);
    }
}
