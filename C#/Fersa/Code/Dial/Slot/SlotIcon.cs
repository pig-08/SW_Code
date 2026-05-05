using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Work.CSH.Scripts.Managers;
using YIS.Code.Skills;

namespace PSW.Code.Dial
{
    public class SlotIcon : SkillIconSettingCompo, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private TurnManagerSO turnManager;
        [SerializeField] private Animator chainEffectAnimator;

        [SerializeField] private Vector3 popUpRotationValue;
        [SerializeField] private Vector3 popDownRotationValue;

        [SerializeField] private LayerMask currentSkillLayer;
        [SerializeField] private float notAddSkillIconMoveTime = 0.3f;
        [SerializeField] private int installRange = 60;

        [SerializeField] private IconShakeValueData shakeData;

        public Action OnDragEvent;

        private Collider2D[] currentSlotColliders;
        private bool _isPopUp = false;
        private bool _donMoveIcon;
        private bool _isDrag;

        private Transform _parentTrm;
        private RectTransform _rectTransform;
        private Canvas _canvas;

        private Vector2 _mousePos;
        private Vector2 _startIconAnchoredPosition;
        private SkillDataSO _currentSkillData;

        private PointerEventData _eventData;

        public void Init(Transform parentTrm, Canvas moveIconCanvas)
        {
            _parentTrm = parentTrm;
            _canvas = moveIconCanvas;

            _rectTransform = GetComponent<RectTransform>();
            _startIconAnchoredPosition = _rectTransform.anchoredPosition;
            _eventData = new PointerEventData(EventSystem.current);
        }

        public void Chaining(bool isChaining)
        {
            if(isChaining)
            {
                _rectTransform.DOShakeAnchorPos(shakeData.duration,
                    shakeData.strength,
                    shakeData.vibrato,
                    shakeData.randomness,
                    shakeData.snapping,
                    shakeData.fadeOut);
            }
            chainEffectAnimator.Play(isChaining ? "Chaining" : "ReChaining", 0, 0);
        }

        public void SetDonMoveIcon(bool donMoveIcon) => _donMoveIcon = donMoveIcon;

        public override void SetSkill(SkillDataSO skillData)
        {
            base.SetSkill(skillData);
            _currentSkillData = skillData;
        }

        public SkillDataSO GetSkill() => _currentSkillData;

        public override void PopUp()
        {
            _isPopUp = !_isPopUp;
            transform.DOLocalRotate(GetRotationValue(), popTime);
            transform.DOScale(sizeData.GetSize(_isPopUp), popTime);
        }

        private Vector3 GetRotationValue()
        {
            if (_isPopUp)
                return popUpRotationValue;
            else
                return popDownRotationValue;
        }

        public void MovePosParentTrm()
        {
            transform.SetParent(_parentTrm);
            _rectTransform.DOAnchorPos(Vector2.zero, notAddSkillIconMoveTime);
        }

        private Transform GetParentTrm(bool isAddIcon = true)
        {
            if (currentSlotColliders.Length <= 0)
                return _parentTrm;
            else
            {
                int minSizeIndex = 0;
                float minSize = (currentSlotColliders[0].transform.position - transform.position).sqrMagnitude;

                for (int i = 1; i < currentSlotColliders.Length; ++i)
                {
                    if (minSize > (currentSlotColliders[i].transform.position - transform.position).sqrMagnitude)
                    {
                        minSizeIndex = i;
                        minSize = (currentSlotColliders[i].transform.position - transform.position).sqrMagnitude;
                    }
                }

                ChoiceSkillSlot choiceSkill = currentSlotColliders[minSizeIndex].GetComponent<ChoiceSkillSlot>();

                /*if (choiceSkill != null && isAddIcon)
                    choiceSkill.SetSkill(this, notAddSkillIconMoveTime);*/

                return currentSlotColliders[minSizeIndex].transform;
            }
        }
        private void Update()
        {
            if ((turnManager.Turn == false || _donMoveIcon) && _isDrag)
                ExecuteEvents.Execute(gameObject, _eventData, ExecuteEvents.endDragHandler);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (turnManager.Turn == false || _donMoveIcon) return;

            _rectTransform.DOKill();
            transform.DOKill();

            _isDrag = true;
            OnDragEvent?.Invoke();
            transform.SetParent(_canvas.transform);
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (turnManager.Turn == false || _donMoveIcon) return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform.parent as RectTransform, eventData.position,
            _canvas.worldCamera, out _mousePos);

            _rectTransform.anchoredPosition = _mousePos;
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            _isDrag = false;
            bool isAddIcon = true;

            _rectTransform.DOKill();

            if (turnManager.Turn == false || _donMoveIcon)
                isAddIcon = false;

            currentSlotColliders = Physics2D.OverlapCircleAll(transform.position, installRange, currentSkillLayer);
            transform.SetParent(GetParentTrm(isAddIcon));
            _rectTransform.DOAnchorPos(_startIconAnchoredPosition, notAddSkillIconMoveTime);
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, installRange);
        }
    }
}

[Serializable]
public struct IconShakeValueData
{
    public float duration;
    public float strength;
    public int vibrato ;
    public float randomness;
    public bool snapping;
    public bool fadeOut;
}