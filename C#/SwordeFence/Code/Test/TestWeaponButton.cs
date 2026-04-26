using DG.Tweening;
using SW.Code.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SW.Code.Test
{
    public class TestWeaponButton : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Sprite specialImage;
        [SerializeField] private Animator look;

        private TestTurretSO _myTurretData;
        private TestShop _shop;

        private bool _isSpecial;
        private Button _myButton;
        public void Init(TestTurretSO turretData, int id,TestShop shop)
        {
            _myTurretData = turretData;
            _shop = shop;

            icon.sprite = _myTurretData.TurretImage;
            _isSpecial = turretData.IsSpecial;

            //icon.sprite = //원할걸로 변경;
            icon.color = Color.white;

            _myButton = GetComponent<Button>();
            _myButton.onClick.AddListener(() =>
            {
                _shop.SetText(_myTurretData);
            });

            if(_isSpecial)
                SpecialTurretSet();

            transform.position = new Vector2(transform.position.x + 600 * id, transform.position.y);

            if (id == 0)
                _shop.SetText(_myTurretData);
        }

        private void SpecialTurretSet()
        {
            Image buttonImage = GetComponent<Image>();
            buttonImage.sprite = specialImage;
            buttonImage.color = Color.yellow;
            look.gameObject.SetActive(true);
        }

        private async void OutLook()
        {
            look.Play("OutLook");
            await Awaitable.WaitForSecondsAsync(0.5f);
            look.gameObject.SetActive(false);
        }
        
    }
}