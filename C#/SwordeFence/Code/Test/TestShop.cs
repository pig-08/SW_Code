using DG.Tweening;
using SW.Code.SO;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Code.Test
{
    public class TestShop : MonoBehaviour
    {
        [SerializeField] private TestTurretDataListSO _testTurretDataListSO;
        [SerializeField] private GameObject weaponButton;

        private TestWeaponButton[] _weaponButtonList = new TestWeaponButton[3];
        private TestSetTurretDataText _setTurretDataText;


        private void Awake()
        {
            Opne();
        }

        public void Opne()
        {
            _setTurretDataText = GetComponentInChildren<TestSetTurretDataText>();
            _setTurretDataText.Init();
            transform.DOMoveY(540, 0.5f).SetEase(Ease.OutBounce);

            for (int i = 0; i < _weaponButtonList.Length; i++)
            {
                if (_testTurretDataListSO.testTurretDataList.Count <= 0) break;
                int j = i;
                GameObject button = Instantiate(weaponButton, transform);
                _weaponButtonList[j] = button.GetComponent<TestWeaponButton>();
                int rndValue = Random.Range(0, _testTurretDataListSO.testTurretDataList.Count);
                _weaponButtonList[j].Init(_testTurretDataListSO.testTurretDataList[rndValue], j, this);
                _testTurretDataListSO.testTurretDataList.Remove(_testTurretDataListSO.testTurretDataList[rndValue]);
            }
        }

        public void SetText(TestTurretSO turretData)
        {
            _setTurretDataText.SetTurretDataText(turretData);
        }

        public void AddTurretData(TestTurretSO turretData)
        {
            _testTurretDataListSO.testTurretDataList.Add(turretData);
        }

        public void SetButtonClick()
        {

        }
    }
}