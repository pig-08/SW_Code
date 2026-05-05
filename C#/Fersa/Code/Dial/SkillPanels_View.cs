using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace PSW.Code.Dial
{
    public class SkillPanels_View : MonoBehaviour, IView<Vector2>
    {
        [SerializeField] private RectTransform iconPanelRectTrm;

        public void Init(Vector2 defaultData)
        {
            iconPanelRectTrm.localPosition = defaultData;
        }

        public void SetMove(Vector2 data, float time, Ease ease)
        {
            print("이동중...");
            iconPanelRectTrm.DOLocalMove(data, time).SetEase(ease);
        }

        public void SetData(Vector2 data)
        {

        }

        public RectTransform GetPanelTrm() => iconPanelRectTrm;

        public void DOKill()
        {
            iconPanelRectTrm.DOKill();
            transform.DOKill();
        }
    }

    [Serializable]
    public struct PanelsPosData
    {
        public Ease ease;
        public Vector2 iconPos;
        public float time;
    }
}