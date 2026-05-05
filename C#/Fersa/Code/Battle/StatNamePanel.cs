using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

namespace PSW.Code.Battle
{
    public class StatNamePanel : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private SizeData sizeData;
        [SerializeField] private float time = 0.1f;
        [SerializeField] private int upYValue = 25;

        [SerializeField] private TextMeshProUGUI text;
        private void Awake()
        {
            rectTransform.DOSizeDelta(sizeData.GetSize(false), 0);
        }

        public void PopUpPanel(Vector2 pos, string statName)
        {
            pos.y += upYValue;
            rectTransform.localPosition = pos;
            rectTransform.DOSizeDelta(sizeData.GetSize(true), time);
            text.SetText(statName);
        }

        public void PopDownPanel()
        {
            rectTransform.DOSizeDelta(sizeData.GetSize(false), time);
        }
    }
}