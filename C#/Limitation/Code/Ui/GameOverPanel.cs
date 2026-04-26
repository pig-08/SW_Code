using DG.Tweening;
using GMS.Code.Core.Events;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public void GameOver()
    {
        transform.DOLocalMove(Vector2.zero,0.3f);
        transform.DOScale(Vector3.one,0.3f);
    }
}
