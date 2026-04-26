using DG.Tweening;
using TMPro;
using UnityEngine;

public class RoundText : MonoBehaviour
{
    private TextMeshProUGUI _roundText;

    private void Awake()
    {
        _roundText = GetComponent<TextMeshProUGUI>();
    }


    public async void SetText( float round, bool isBoss = false)
    {
        transform.DOLocalMoveY(-400 + 540, 0.4f).SetEase(Ease.InOutElastic);
        transform.DOScale(new Vector3(2,2,2), 0.4f).SetEase(Ease.InOutElastic);
        _roundText.text = isBoss == false ? $"[{round}라운드]" : _roundText.text = $"[보스라운드]";
        await Awaitable.WaitForSecondsAsync(0.8f);

        if (isBoss)
        {
            transform.DOScale(new Vector3(0,0),0.4f).SetEase(Ease.InOutElastic);
            return;
        }

        transform.DOLocalMoveY(0 + 540, 0.4f).SetEase(Ease.InOutElastic);
        transform.DOScale(new Vector3(1,1,1), 0.4f).SetEase(Ease.InOutElastic);
    }
}
