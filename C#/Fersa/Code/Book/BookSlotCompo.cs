using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YIS.Code.Skills;

namespace PSW.Code.Book
{
    public abstract class BookSlotCompo : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
    {
        public Action<Vector2, string> OnShowDescription;
        public event Action OnShowDownDescription;

        [SerializeField] protected Image icon;

        protected BookLockType _lockType;
        protected bool _isSlotTypePopUp = true;
        protected bool _isLockTypePopUp = true;
        protected string _description;

        protected RectTransform _rectTransform;

        public virtual void Init<T>(T data)
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public virtual void SetLock(bool isLock)
        {
            _lockType = isLock ? BookLockType.Lock : BookLockType.NotLock;
        }

        public void SetIcon(Sprite iconImage) => icon.sprite = iconImage;

        public void SetPopUp(bool isPopUp)
        {
            if (_isLockTypePopUp == false)
                return;

            gameObject.SetActive(isPopUp);
            _isSlotTypePopUp = isPopUp;
        }

        public void SetLockSlotPop(BookLockType checkLockType, bool isPopUp)
        {
            if (_isSlotTypePopUp == false)
                return;

            if(_lockType == checkLockType)
            {
                gameObject.SetActive(isPopUp);
                _isLockTypePopUp = isPopUp;
            }
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (_lockType == BookLockType.Lock)
                return;
            OnShowDescription?.Invoke(_rectTransform.anchoredPosition, _description);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            OnShowDownDescription?.Invoke();
        }
    }

    public enum BookSlotType
    {
        Skill,
        Item,
    }

    public enum BookLockType
    {
        Lock,
        NotLock,
    }
}