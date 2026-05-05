using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TalkButtonPanel : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private float popTime;
    [SerializeField] private float upValue;

    [Header("FadeData")]
    [SerializeField] private List<SpriteRenderer> fadeSpriteList;
    [SerializeField] private TextMeshPro fadeText;

    private Vector2 _defaultPos;
    private Vector2 _upPos;

    private bool _isUp;

    public void Init()
    {
        _defaultPos = transform.localPosition;
        _upPos = _defaultPos;
        _upPos.y += upValue;
    }

    public void PopUpDown()
    {
        fadeText.DOKill();
        transform.DOKill();
        StopAllCoroutines();

        _isUp = !_isUp;
        PopPanel();
    }

    private void PopPanel()
    {
        foreach (SpriteRenderer sprit in fadeSpriteList) sprit.DOFade(_isUp ? 1 : 0, popTime);
        fadeText.DOFade(_isUp ? 1 : 0, popTime);
        transform.DOLocalMove(_isUp ? _upPos : _defaultPos, popTime);
    }
}
