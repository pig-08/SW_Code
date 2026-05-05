using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PSW.Code.Dial
{
    public class SkillPanels_Model : ModelCompo<PanelsPosData>
    {
        [field : SerializeField] public VerticalLayoutGroup VerticalLayoutGroup { get; private set; }
        [SerializeField] private float moveTime;
        [SerializeField] private Ease ease;
        [SerializeField] private LayoutElement panelLayout;

        private SizeData _movePosData;
        private bool _isPopMove = false;

        public override PanelsPosData InitModel()
        {
            return _currentData;
        }

        public void SetUpData(Vector2 upPos) => _movePosData.Up = upPos;
        public void SetDownData(Vector2 downPos) => _movePosData.Down = downPos;
        public LayoutElement GetPaenlLayout() => panelLayout;
        public Ease GetEase() => ease;
        public float GetTime() => moveTime;
        public Vector2 GetMovePos(bool isUp)
        {
            _isPopMove = isUp;
            return _movePosData.GetSize(isUp);
        }
        public bool GetIsPopMove() => _isPopMove;
    }

    [Serializable]
    public struct PopPosData
    {
        public bool isPopUp;
        public PanelsPosData data;
    }
}