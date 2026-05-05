using DG.Tweening;
using PSW.Code.BaseSystem;
using UnityEngine;

public class BaseOnOffAnimationUI : BaseOnOffSystemUI
{
    [Header("Animation")]
    [SerializeField] protected float tweenDuration = 0.25f;
    [SerializeField] protected float downValue = -50f;
    [SerializeField] protected Ease ease = Ease.OutCubic;

    protected CanvasGroup _canvasGroup;
    protected RectTransform _rect;
    protected Tween _tween;

    public virtual void Awake()
    {
        _rect = transform as RectTransform;
        _canvasGroup = GetComponentInParent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
    public override void PopUp()
    {
        if (IsOnPopUp() == false) return;

        base.PopUp();
        _tween?.Kill();

        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;

        _rect.anchoredPosition = new Vector2(0f, downValue);
        _canvasGroup.alpha = 0f;

        _tween = DOTween.Sequence()
            .Append(_rect.DOAnchorPosY(0f, tweenDuration).SetEase(ease))
            .Join(_canvasGroup.DOFade(1f, tweenDuration));
    }
    public override void PopDown()
    {
        base.PopDown();
        _tween?.Kill();

        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        _tween = DOTween.Sequence()
            .Append(_rect.DOAnchorPosY(downValue, tweenDuration).SetEase(Ease.InCubic))
            .Join(_canvasGroup.DOFade(0f, tweenDuration))
            .OnComplete(() => _canvasGroup.alpha = 0);
    }
}
