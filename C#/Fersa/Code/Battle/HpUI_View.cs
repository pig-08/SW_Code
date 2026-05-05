using DG.Tweening;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
namespace PSW.Code.Battle
{
    public class HpUI_View : MonoBehaviour, IView<HpData>
    {
        [SerializeField] private Image hpBar;
        [SerializeField] private TextMeshProUGUI hpText;

        private RectTransform _textTrm;

        public void Init(HpData defaultData)
        {
            _textTrm = hpText.GetComponent<RectTransform>();
            hpBar.fillAmount = defaultData.currentHp / defaultData.maxHp;
            float hp = MathF.Round(defaultData.currentHp,1);
            hpText.SetText(hp.ToString());
            LayoutRebuilder.ForceRebuildLayoutImmediate(_textTrm);
        }

        public void SetData(HpData data)
        {
            hpBar.DOFillAmount(data.currentHp / data.maxHp, data.time);
            float hp = MathF.Round(data.currentHp, 1);
            hpText.SetText(hp.ToString());
            LayoutRebuilder.ForceRebuildLayoutImmediate(_textTrm);
        }

        public void SetTextPos(HpTextPosData data)
        {
            print(data.isLeft);
            RectTransform textRect = hpText.GetComponent<RectTransform>();
            textRect.anchorMin = data.anchorMin;
            textRect.anchorMax = data.anchorMax;
            textRect.pivot = data.pivot;
            textRect.anchoredPosition = data.pos;
        }
    }

    public struct HpData
    {
        public float currentHp;
        public float maxHp;
        public float time;
    }
}

