using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PSW.Code.Book
{
    public abstract class TagToggleCompo<T> : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI text;
        [SerializeField] protected SpriteData popImageData;
        [SerializeField] protected Image panel;

        protected bool _isPopUpData = true;
        protected Action<T, bool> _currentEvent;
        protected T _currentType;

        public void Init(T type, Action<T, bool> InvokeEvent)
        {
            text.SetText(type.ToString());
            _currentType = type;
            _currentEvent = InvokeEvent;
        }
        public void SetPopUp()
        {
            _isPopUpData = !_isPopUpData;
            panel.sprite = popImageData.GetSprite(_isPopUpData);
            _currentEvent?.Invoke(_currentType, _isPopUpData);
        }
    }
}