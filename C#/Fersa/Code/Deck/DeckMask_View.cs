using DG.Tweening;
using UnityEngine;

public class DeckMask_View : MonoBehaviour, IView<Vector2>
{
    [SerializeField] public RectTransform maskTrm;
    public void Init(Vector2 defaultData)
    {
        maskTrm.localPosition = defaultData;
    }

    public void SetData(Vector2 data)
    {

    }

    public void SetMaskTrm(Vector2 pos, float time)
    {
        maskTrm.DOKill();
        maskTrm.DOLocalMove(pos, time);
    }
}
