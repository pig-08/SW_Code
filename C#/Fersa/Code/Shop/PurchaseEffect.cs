using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PSW.Code.Shop
{
    public class PurchaseEffect : MonoBehaviour
    {
        [SerializeField] private RectTransform effectRectTrm;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private float effectPlayTime;
        [SerializeField] private float moveUpValue = 25;

        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image icon;

        private SizeData _movePosData;
        private WaitForSeconds _wait;

        private void Start()
        {
            _movePosData.Down = effectRectTrm.transform.localPosition;
            _movePosData.Up = _movePosData.Down;
            _movePosData.Up.y += moveUpValue;
            _wait = new WaitForSeconds(effectPlayTime / 2);
        }

        public void InitEffect(Sprite iconImage)
        {
            icon.sprite = iconImage;
        }

        public void PlayEffect(string price)
        {
            string newText = "-";
            newText += price;
            text.SetText(newText);

            StartCoroutine(PlayMoveUpEffect());
        }

        private IEnumerator PlayMoveUpEffect()
        {
            group.alpha = 1;
            effectRectTrm.DOLocalMove(_movePosData.GetSize(true), effectPlayTime);
            yield return _wait;
            group.DOFade(0, effectPlayTime / 2);
        }

        public void ResetEffect()
        {
            text.SetText("");
            group.alpha = 0;
            effectRectTrm.localPosition = _movePosData.GetSize(false);
        }
    }
}