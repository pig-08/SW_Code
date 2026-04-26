using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ClearPanel : MonoBehaviour
{
    [SerializeField] private RectTransform textListPoint;

    [Header("Button")]
    [SerializeField] private RectTransform rePlayButtonPoint;
    [SerializeField] private RectTransform exitButtonPoint;


    private Image _panel;

    private void Awake()
    {
        _panel = GetComponent<Image>();
    }

    public async void GameClear()
    {
        _panel.DOFade(1,1.2f);
        await Awaitable.WaitForSecondsAsync(0.9f);
        textListPoint.DOLocalMoveY(0, 1.4f).SetEase(Ease.OutBack);
        await Awaitable.WaitForSecondsAsync(1.2f);

        rePlayButtonPoint.DOLocalMoveY(0, 0.3f);
        rePlayButtonPoint.DOScale(new Vector2(1, 1), 0.3f);
        exitButtonPoint.DOLocalMoveY(0,0.3f);
        exitButtonPoint.DOScale(new Vector2(1, 1), 0.3f);
    }
}
